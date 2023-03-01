using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate;
    private float _canFire = -1f;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private bool _enemyShielded = false;
    private int _enemyPowerupID;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyPowerup());
        _shieldVisualizer.SetActive(false);
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        EnemyFire();       
    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -8f)
        {
            float randomX = Random.Range(5f, -5f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    void EnemyFire()
    {
        if (Time.time > _canFire && transform.position.y <= 6f && transform.position.y >= -6f)
        {
            _fireRate = Random.Range(6f, 10f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);        //Debug.Break(); pause game when enemy laser is instantiated
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();

            }
        }
    }

    public void EnemyFire2()
    {
        GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);       
        Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].AssignEnemyLaser();

        }
    }

    IEnumerator EnemyPowerup()
    {
        while (true)
        {
            _enemyPowerupID = Random.Range(0, 5);
            if (_enemyPowerupID == 0)
            {
                _enemyShielded = true;
                if (_enemyShielded == true)
                {
                    _shieldVisualizer.SetActive(true);
                }
                else
                {
                    _shieldVisualizer.SetActive(false);
                }
            }
            yield return new WaitForSeconds(5);
        }        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collided");
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null) 
            {
                player.AddScore(10);
                player.Damage(1);
                //return;
            }
            if (_enemyShielded == true)
            {
                _shieldVisualizer.SetActive(false);
                _enemyShielded = false;
                return;
            }
            if (_enemyShielded == false)
            {
                _anim.SetTrigger("OnEnemyDeath");                            // can also pass OnEnemyDeath ID
                _speed = 0;
                _audioSource.Play();
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 2f);
            }      
        }
        
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null && _enemyShielded == false)
            {
                _player.AddScore(10);
                //return;
            }
            if (_enemyShielded == true)
            {
                _shieldVisualizer.SetActive(false);
                _enemyShielded = false;
                return;
            }
            if (_enemyShielded == false)
            {
                _anim.SetTrigger("OnEnemyDeath");                            // can also pass OnEnemyDeath ID
                _speed = 0;
                _audioSource.Play();
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 2f);
            }
        }
    }
}

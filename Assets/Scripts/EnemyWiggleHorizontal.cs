using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWiggleHorizontal : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    //private float _wiggleDistance = 0.7f;
    //private float _wiggleSpeed = 2.5f;

    private Player _player;
    private Transform _playerPosition;
    private Animator _anim;
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _laserPrefab;

    private float _canFire = -1f;
    private float _fireRate = 2f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _playerPosition = GameObject.Find("Player").transform;
        _audioSource = GetComponent<AudioSource>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
        if (_playerPosition == null)
        {
            Debug.LogError("The Player's Position is NULL.");
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
        HorizontalMovement();
    }

    void HorizontalMovement()
    {
        //float yPosition = Mathf.Sin(Time.time * _wiggleSpeed) * _wiggleDistance;             it only oscillates between x-axis
        //transform.localPosition = new Vector3(transform.position.x, yPosition, 0);
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
        if (transform.position.x >= 10f)
        {
            float randomY = Random.Range(6f, -6f);
            transform.position = new Vector3(-11, randomY, 0);      //why it doesn't go at random position?
        }
    }

    public void ShootUpward()
    {
        if (Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            GameObject enemyLaserUp = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaserUp.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaserUp();

            }
            //Debug.Break(); Pause game to see what's going on
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
            }
            _anim.SetTrigger("OnEnemyDeath");                            // can also pass OnEnemyDeath ID
            _speed = 0;
            //_wiggleSpeed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            //_wiggleSpeed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvoiding : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    private float _avoidSpeed = 6f;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;

    [SerializeField]
    private bool _avoid = false;

    // Start is called before the first frame update
    void Start()
    {
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
        if (_avoid == true)
        {
            transform.Translate(Vector3.left * _avoidSpeed * Time.deltaTime);
        }
        else
        {
            VerticalMovement();
        }       
    }

    void VerticalMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -8f)
        {
            float randomX = Random.Range(5f, -5f);
            transform.position = new Vector3(randomX, 7.5f, 0);      
        }
    }

    public void Avoiding()
    {
        _avoid = true;
        StartCoroutine(StopAvoiding());
    }

    IEnumerator StopAvoiding()
    {
        while (_avoid == true)
        {
            yield return new WaitForSeconds(0.5f);
            _avoid = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.AddScore(10);
                player.Damage(1);
            }
            _anim.SetTrigger("OnEnemyDeath");                           
            _speed = 0;
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
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWiggle : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    private float _wiggleDistance = 1;
    private float _wiggleSpeed = 3;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;

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
        WiggleMovement();
    }

    void WiggleMovement()
    {
        float xPosition = Mathf.Sin(Time.time * _wiggleSpeed) * _wiggleDistance;
        transform.localPosition = new Vector3(xPosition, transform.position.y, 0);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -9f)
        {
            float randomX = Random.Range(5f, -5f);
            transform.position = new Vector3(randomX, 10, 0);
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
            _wiggleSpeed = 0;
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
            _wiggleSpeed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }
    }
}

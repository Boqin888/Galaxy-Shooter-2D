using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFingers : MonoBehaviour
{
    private Player _player;
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _attackSpeed = 4f;
    [SerializeField] private float _canMove = 5f;
    [SerializeField] private float _moveRate;
    [SerializeField] private int _fingersLife = 3;
    private bool _retreatBool = false;
    [SerializeField] private bool _coroutineBool;
    //[SerializeField] private float _telegraphSpeed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
        if (_audioSource == null)
        {
            Debug.LogError("The Aduio is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        BossFingersMove();

        if (_coroutineBool == true)
        {
            StartCoroutine(FingersAttack());
        }
        if (_coroutineBool == false)
        {
            StartCoroutine(FingersRetreat());
        }
    }

    private void BossFingersMove()
    {
        if (Time.time > _canMove)
        {
            _moveRate = Random.Range(6f, 10f);
            _canMove = Time.time + _moveRate;
            _coroutineBool = true;
        }
    }

    IEnumerator FingersAttack()
    {
        
        while (_retreatBool == false)
        {       
            yield return new WaitForSeconds(1f);
            transform.Translate(Vector3.down * _attackSpeed * Time.deltaTime);
            if (transform.position.y <= -3f)
            {
                _attackSpeed = 0f;
                _retreatBool = true;
                _coroutineBool = false;
                _speed = 2f;
            }
        }
        
    }

    IEnumerator FingersRetreat()
    {
        while (_retreatBool == true)
        {
            yield return new WaitForSeconds(1f);
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
            if (transform.position.y >= -1f)
            {
                _speed = 0f;
                _retreatBool = false;
                _attackSpeed = 4f;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if ((player != null) && (_fingersLife != 1))
            {
                player.Damage(1);
                _fingersLife--;
            }
            if ((player != null) && (_fingersLife == 1))
            {
                player.AddScore(10);
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _speed = 0;
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject);
            }
            _audioSource.Play();
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if ((_player != null) && (_fingersLife != 1))
            {
                _fingersLife--;
            }
            if ((_player != null) && (_fingersLife == 1))
            {
                _player.AddScore(10);
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _speed = 0;
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject);
            }
            _audioSource.Play();
        }
    }
}

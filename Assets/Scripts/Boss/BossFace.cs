using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFace : MonoBehaviour
{
    private Player _player;
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _explosionPrefab;

    private float _speed = 2f;
    [SerializeField] private float _attackSpeed = 6f;
    [SerializeField] private int _faceLife = 10;
    private bool _movementBool = false;
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
        StartCoroutine(FaceAttack());
    }

    IEnumerator FaceAttack()
    {
        while (_movementBool == false)
        {
            yield return new WaitForSeconds(5f);
            transform.Translate(Vector3.up * _attackSpeed * Time.deltaTime);
            if (transform.position.y <= -2f)
            {
                _attackSpeed = 0f;
                _movementBool = true;                
                _speed = 2f;
                StartCoroutine(FaceRetreat());
            }
        }        
    }

    IEnumerator FaceRetreat()
    {
        while (_movementBool == true)
        {
            yield return new WaitForSeconds(1f);
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            if (transform.position.y >= 3f)
            {
                _speed = 0f;
                _movementBool = false;
                yield return new WaitForSeconds(5f);
                _attackSpeed = 6f;
                StartCoroutine(FaceAttack());
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if ((player != null) && (_faceLife != 1))
            {
                player.Damage(1);
                _faceLife--;
            }
            if ((player != null) && (_faceLife == 1))
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
            if ((_player != null) && (_faceLife != 1))
            {
                _faceLife--;
            }
            if ((_player != null) && (_faceLife == 1))
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

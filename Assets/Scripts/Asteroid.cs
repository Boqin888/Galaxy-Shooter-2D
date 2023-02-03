using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private Player _player;
    private SpawnManager _spawnManager;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log("Collided " + other.tag);  // tells what tag collided with
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 1.5f);
            _spawnManager.StartSpawning();
        }


        if (other.tag == "laser")                                                      // working if copied but not working if typed????
        {
            instantiate(_explosionprefab, transform.position, quaternion.identity);
            destroy(other.gameobject);
            _spawnmanager.startspawning();
            destroy(this.gameobject, 1.5f);
        }
    }
}


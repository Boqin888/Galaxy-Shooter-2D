using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArms : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("The Aduio is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(this.gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _audioSource.Play();
        }
        
    }

}

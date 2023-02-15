using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceBulletsLeft : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private float _rotateSpeed = -30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);

        if (transform.position.y >= 20f) 
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}


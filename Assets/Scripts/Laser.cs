using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]

    // Secondary Fire powerup attempt
    //private GameObject _enhancedBulletLeftLeft;
    //[SerializeField]
    //private GameObject _enhancedBulletLeftMiddle;
    //[SerializeField]
    //private GameObject _enhancedBulletLeftRight;
    //[SerializeField]
    //private GameObject _enhancedBulletRightLeft;
    //[SerializeField]
    //private GameObject _enhancedBulletRightMiddle;
    //[SerializeField]
    //private GameObject _enhancedBulletRightRight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 9f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    // Secondary Fire powerup attempt
    //public void EnhanceBulletsON()
    //{
    //    Instantiate(_enhancedBulletLeftLeft, transform.position, Quaternion.identity);
    //    Instantiate(_enhancedBulletLeftMiddle, transform.position, Quaternion.identity);
    //    Instantiate(_enhancedBulletLeftRight, transform.position, Quaternion.identity);
    //    Instantiate(_enhancedBulletRightLeft, transform.position, Quaternion.identity);
    //    Instantiate(_enhancedBulletRightMiddle, transform.position, Quaternion.identity);
    //    Instantiate(_enhancedBulletRightRight, transform.position, Quaternion.identity);
    //}
}

    

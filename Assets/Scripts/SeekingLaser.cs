using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingLaser : MonoBehaviour
{
    private float _speed = 3;
    private GameObject _closestEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
        TowardEnemy();
    }

    public void FindClosestEnemy()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float comparisonDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distanceCurrent = Vector3.Distance(this.gameObject.transform.position, enemy.transform.position);
            if (distanceCurrent < comparisonDistance)
            {
                _closestEnemy = enemy;
                comparisonDistance = distanceCurrent;
            }
        }
        //return _closestEnemy;
    }

    void TowardEnemy()
    {
        var step = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _closestEnemy.transform.position, step);
        Destroy(this.gameObject, 8f);
    }
}

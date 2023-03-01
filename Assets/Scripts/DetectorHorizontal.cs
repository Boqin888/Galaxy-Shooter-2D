using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorHorizontal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.LogError("Near Player");
            EnemyWiggleHorizontal _EnemyWiggleHorizontal = GetComponentInParent<EnemyWiggleHorizontal>();
            if (_EnemyWiggleHorizontal != null)
            {
                _EnemyWiggleHorizontal.ShootUpward();
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorHorizontal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EnemyWiggleHorizontal enemyWiggleHorizontal = GetComponentInParent<EnemyWiggleHorizontal>();
            if (enemyWiggleHorizontal != null)
            {
                enemyWiggleHorizontal.ShootUpward();
            }
        }
    }
}


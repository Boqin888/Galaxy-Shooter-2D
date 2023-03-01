using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorDiagonal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EnemyDiagonal _enemyDiagonal = GetComponentInParent<EnemyDiagonal>();
            if (_enemyDiagonal != null)
            {
                _enemyDiagonal.DetectedAction();
            }
        }
    }
}


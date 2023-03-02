using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            EnemyAvoiding enemyAvoiding = GetComponentInParent<EnemyAvoiding>();
            if (enemyAvoiding != null)
            {
                enemyAvoiding.Avoiding();
            }
        }
    }
}

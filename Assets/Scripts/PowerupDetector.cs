using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Powerup")
        {
            Enemy _enemy = GetComponentInParent<Enemy>();
            if (_enemy != null)
            {
                _enemy.EnemyFire2();
            }
        }
    }
}

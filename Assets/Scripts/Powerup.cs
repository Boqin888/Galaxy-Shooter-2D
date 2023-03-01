using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _powerUpSpeed = 3f;

    [SerializeField] //0=triple 1=spd 2=shiled
    private int powerupID;

    [SerializeField]
    private AudioClip _clip;

    private UIManager _uiManager;

    [SerializeField]

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            TowardPlayer();
        }
        else
        {
            PowerupMovement();
        }
        
    }

    void PowerupMovement()
    {
        transform.Translate(Vector3.down * _powerUpSpeed * Time.deltaTime);

        if (transform.position.y <= -9f)
        {
            Destroy(this.gameObject);
        }
    }

    void TowardPlayer()
    {
        var step = _powerUpSpeed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, step);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.MoreAmmoActive(1);
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.MoreSpeedActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    case 3:
                        player.MoreAmmoActive(3);
                        break;
                    case 4:
                        player.Damage(-1);
                        break;
                    case 5:
                        player.MoreAmmoActive(1);
                        player.EnhancedTripleShotActive();
                        break;
                    case 6:
                        player.Damage(2);
                        break;
                    default:
                        Debug.Log("Default");
                        break;
                }
                Destroy(this.gameObject);
            }
        }
        if (other.tag == "EnemyLaser")
        {
            Destroy(this.gameObject);
        }
    }
}





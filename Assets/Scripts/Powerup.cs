using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _powerUpSpeed =  3f;

    [SerializeField] //0=triple 1=spd 2=shiled
    private int powerupID;

    [SerializeField]
    private AudioClip _clip;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        TripleShotMovement();
    }

    void TripleShotMovement()
    {
        transform.Translate(Vector3.down * _powerUpSpeed * Time.deltaTime);

        if (transform.position.y <= -9f)
        {
            Destroy(this.gameObject);
        }
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
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.MoreSpeedActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    case 3:
                        player.MoreAmmoActive();
                        break;
                    case 4:
                        player.Damage(-1);
                        break;
                    case 5:
                        player.EnhancedTripleShotActive();
                        break;
                    default:
                        Debug.Log("Default");
                        break;
                }
                Destroy(this.gameObject);
            }
        }        
    }
}





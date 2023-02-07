using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   //public or private reference
    //date type (int, float, bool, string)
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private bool isTripleShotActive = false;
    [SerializeField]
    private bool moreSpeed = false;
    private int _speedMultiplier = 2;
    [SerializeField]
    private bool _shieldActive = false;
    [SerializeField]
    private bool _shieldActive1 = false;
    [SerializeField]
    private bool _shieldActive2 = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _shieldVisualizer1;
    [SerializeField]
    private GameObject _shieldVisualizer2;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;

    [SerializeField]
    private GameObject _rightEngine, _leftEngine;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource;
    private float _fasterSpeed = 10f;

    private int _ammoCount = 15;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,-4,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); //could also put in Spawn Manager tag
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("The player's audio source is NULL");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
        if (_spawnManager == null)
        {
            Debug.LogError("The spawn Manager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        FireLaser();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //move left, right with a, d
        float verticalInput = Input.GetAxis("Vertical"); //move up, down with w, s

        //new Vector3(1,0,0) * input * _speed * real time
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        // 2D Game Part III ---------------------------------------------------------------------------------------------------------
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Shift pressed");
            transform.Translate(direction * _fasterSpeed * Time.deltaTime);
        } 
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        // 2D Game Part III ---------------------------------------------------------------------------------------------------------


        if (transform.position.y >= 3)
        {
            transform.position = new Vector3(transform.position.x, 3, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        

        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x <= -10)
        {
            transform.position = new Vector3(10, transform.position.y, 0);
        }
        else if (transform.position.x >= 10)
        {
            transform.position = new Vector3(-10, transform.position.y, 0);
        }
    }

    void FireLaser()
    {

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && _ammoCount > 0)                                 //GetKey for continuous fire
        {
            _canFire = Time.time + _fireRate;
            if (isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                AmmoDisplay();
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
                AmmoDisplay();
            }       
        }

        _audioSource.Play();
    }

    public void Damage() 
    {
        if (_shieldActive == true)
        {
            _shieldActive = false;
            _shieldVisualizer.SetActive(false);
            ShieldActive1();                            // part III
            return;
        }
        if (_shieldActive1 == true)
        {
            _shieldActive1 = false;
            _shieldVisualizer1.SetActive(false);
            ShieldActive2();
            return;
        }
        if (_shieldActive2 == true)
        {
            _shieldActive2 = false;
            _shieldVisualizer2.SetActive(false);
            return;
        }

        _lives -= 1;
        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }
        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();

            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        while (isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5f);
            isTripleShotActive = false;
        }
    }

    public void MoreSpeedActive()
    {
        moreSpeed = true;
        _speed *= _speedMultiplier;
        StartCoroutine(MoreSpeedPowerDownRoutine());
    }

    IEnumerator MoreSpeedPowerDownRoutine()
    {
        while (moreSpeed == true)
        {
            yield return new WaitForSeconds(5f);
            moreSpeed = false;
            _speed /= _speedMultiplier;
        }
    }

    public void ShieldActive()
    {
        _shieldActive = true;
        _shieldVisualizer.SetActive(true);
    }
// 2D Game Part III -------------------------------------------------------------------------------
    public void ShieldActive1()
    {
        _shieldActive1 = true;
        _shieldVisualizer1.SetActive(true);
    }
    public void ShieldActive2()
    {
        _shieldActive2 = true;
        _shieldVisualizer2.SetActive(true);
    }
// 2D Game Part III -------------------------------------------------------------------------------

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public void AmmoDisplay()
    {
        _ammoCount -= 1;
        _uiManager.UpdateAmmo(_ammoCount);
    }
}



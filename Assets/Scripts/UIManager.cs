using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;
    [SerializeField]
    private Text _ammoCountText;



    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _ammoCountText.text = "Ammo: " + 15;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL.");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString(); // just playerScore works too
    }

    public void UpdateAmmo (int ammoCount)
    {
        _ammoCountText.text = "Ammo: " + ammoCount.ToString();

        if (ammoCount == 0)
        {
            StartCoroutine(AmmoCountFllickerRoutine());
        }
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }


    void GameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(.5f);
        }   
    }

    IEnumerator AmmoCountFllickerRoutine()
    {
        while (true)
        {
            _ammoCountText.text = "Ammo: " + 0;
            yield return new WaitForSeconds(.5f);
            _ammoCountText.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyDiagonalPrefab;
    [SerializeField]
    private GameObject _enemyWigglePrefab;
    [SerializeField]
    private GameObject _enemyHorizontalWigglePrefab;
    [SerializeField]
    private GameObject _enemyAvoidPrefab;
    [SerializeField]
    private GameObject _bossPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]  private bool _stopSpawning = false;
    [SerializeField]
    private float _enemySpawnTime = 4f;
    private int _score;
    private bool _normalSpawning = true;
    private bool _enemyDiagonalSpawning = false;
    private bool _enemyWiggleSpawning = false;
    private bool _enemyHorizontalWiggleSpawning = false;
    private bool _enemyAvoidSpawning = false;
    private bool _bossSpawning = false;




    public void StartSpawning()
    {
        StartCoroutine("SpawnEnemyRoutine");          //StartCoroutine(SpawnRoutine());
        StartCoroutine("SpawnPowerupRoutine");
    }

    // Update is called once per frame
    void Update()
    {
        // yield return null; wait one frame before next line
        // yeild return new WaitForSeconds(5.0f); wait 5s before next line
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1f);
        
        while (_stopSpawning == false)
        {           
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 9, 0);
            Vector3 posToSpawnHorizontal = new Vector3(-12, Random.Range(-5f, 5f), 0);

            if (_normalSpawning == true)
            {
                GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(_enemySpawnTime);
            }
            
            if(_enemyDiagonalSpawning == true)
            {
                GameObject newEnemyDiagonal = Instantiate(_enemyDiagonalPrefab, posToSpawn, Quaternion.identity);
                newEnemyDiagonal.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(_enemySpawnTime);
            }
            if (_enemyWiggleSpawning == true)
            {
                GameObject newEnemyWiggle = Instantiate(_enemyWigglePrefab, posToSpawn, Quaternion.identity);
                newEnemyWiggle.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(_enemySpawnTime);
            }
            if (_enemyHorizontalWiggleSpawning == true)
            {
                GameObject newEnemyWiggleHorizontal = Instantiate(_enemyHorizontalWigglePrefab, posToSpawnHorizontal, Quaternion.identity);
                newEnemyWiggleHorizontal.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(_enemySpawnTime);
            }
            if (_enemyAvoidSpawning == true)
            {
                GameObject newEnemyAvoid = Instantiate(_enemyAvoidPrefab, posToSpawn, Quaternion.identity);
                newEnemyAvoid.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(_enemySpawnTime);
            }
            if (_bossSpawning == true)
            {
                GameObject newBoss = Instantiate(_bossPrefab, new Vector3(-3.5f, 5, 0), Quaternion.identity);
                newBoss.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(10000);
            }
        }
    }

    public void SpawnController(int playerScore)
    {
        if (playerScore > 10)
        {
            _enemyDiagonalSpawning = true;
        }
        if (playerScore > 50)
        {
            _enemyWiggleSpawning = true;
        }
        if (playerScore > 100)
        {
            _enemyHorizontalWiggleSpawning = true;
        }
        if (playerScore > 150)
        {
            _enemyAvoidSpawning = true;
        }
        if (playerScore > 200)
        {
            _bossSpawning = true;
            _normalSpawning = false;
            _enemyDiagonalSpawning = false;
            _enemyWiggleSpawning = false;
            _enemyHorizontalWiggleSpawning = false;
            _enemyAvoidSpawning = false;           
        }
        if ((playerScore > 300) && (_enemyContainer.transform.childCount == 0))
        {
            _stopSpawning = true;
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(1f);
        while (_stopSpawning == false)
        {
            Vector3 postToSpawn = new Vector3(Random.Range(-8, 8f), 9, 0);
            int randomPowerUp = Random.Range(0, 10);
            if (randomPowerUp == 5 || randomPowerUp == 4 || randomPowerUp == 7) 
            {
                randomPowerUp = Random.Range(0, 8);
            }
            Instantiate(powerups[randomPowerUp], postToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}

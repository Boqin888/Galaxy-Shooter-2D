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
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    private bool _stopSpawning = false;
    [SerializeField]
    private float _enemySpawnTime = 4f;
    private int _score;
    private bool _enemyDiagonalSpawning = false;
    private bool _enemyWiggleSpawning = false;
    private bool _enemyHorizontalWiggleSpawning = false;




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
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnTime);

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
        }
    }

    public void SpawnController(int playerScore)
    {
        if (playerScore > 10)
        {
            //_enemySpawnTime = _enemySpawnTime - 0.5f;
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
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(1f);
        while (_stopSpawning == false)
        {
            Vector3 postToSpawn = new Vector3(Random.Range(-8, 8f), 9, 0);
            int randomPowerUp = Random.Range(0, 9);
            if (randomPowerUp == 5 || randomPowerUp == 4) 
            {
                randomPowerUp = Random.Range(0, 7);
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

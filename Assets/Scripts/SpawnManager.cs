using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    private bool _stopSpawning = false;


    public void StartSpawning()
    {
        StartCoroutine("SpawnEnemyRoutine");          //StartCoroutine(SpawnRoutine());
        StartCoroutine("SpawnPowerupRoutine");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1f);
        // yield return null; wait one frame before next line
        // yeild return new WaitForSeconds(5.0f); wait 5s before next line
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 11, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(1f);
        while (_stopSpawning == false)
        {
            Vector3 postToSpawn = new Vector3(Random.Range(-8, 8f), 9, 0);
            int randomPowerUp = Random.Range(0, 6);
            if (randomPowerUp == 5)
            {
                randomPowerUp = Random.Range(0, 6);
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

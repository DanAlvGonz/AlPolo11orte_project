using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject flyingEnemyPrefab;
    [SerializeField]
    private GameObject groundedEnemyPrefab;

    [SerializeField]
    private float flyingEnemyInterval = 5f;
    [SerializeField]
    private float groundedEnemyInterval = 3.5f;


    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(spawnEnemy(flyingEnemyInterval, flyingEnemyPrefab));
        StartCoroutine(spawnEnemy(groundedEnemyInterval, groundedEnemyPrefab));

    }

   private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));

    }

}

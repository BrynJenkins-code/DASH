using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] currentEnemies;
    public Vector3 spawnValues;
    public float spawnWait;
    public int startWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    private bool spawning = true;
    int randEnemy;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    public void StopSpawning(){
        spawning = false; 
        StopCoroutine(Spawner());
        Debug.Log("Corourine stopped");
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(startWait);

        while (spawning)
        {
            randEnemy = Random.Range(0, currentEnemies.Length);

            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), 1);

            Instantiate (currentEnemies[randEnemy], spawnPosition + transform.TransformPoint(0,0,0), gameObject.transform.rotation);

            yield return new WaitForSeconds(spawnWait);


        }
    }
}

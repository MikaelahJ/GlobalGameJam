using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region singleton
    public static EnemyManager Instance = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    [SerializeField] private GameObject molePrefab;

    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    private List<GameObject> activeEnemies = new List<GameObject>();
    private int maxAmount = 3;

    private float timer;
    private int timeBetween = 15;
    public int minTime = 20;
    public int maxTime = 40;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetween && activeEnemies.Count < maxAmount)
        {
            SpawnEnemy();
            timer = 0;
        }
    }

    private void SpawnEnemy()
    {
        Transform spawnPos = spawnPoints[Random.Range(0, spawnPoints.Count)];

        var enemy = Instantiate(molePrefab, spawnPos.position, spawnPos.rotation);
        activeEnemies.Add(enemy);

        timeBetween = Random.Range(minTime, maxTime);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        Destroy(enemy);
        activeEnemies.Remove(enemy);
    }
    public void EnemyKilled(GameObject enemy)
    {
        activeEnemies.Remove(enemy);

        //Göra näringsresurs
    }
}

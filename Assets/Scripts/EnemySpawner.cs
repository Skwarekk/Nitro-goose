using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySO> enemiesSOList = new List<EnemySO>();
    private EnemySO enemyToSpawn;
    private int unitsForPixel = 100;
    private float halfScreenSize = Screen.width / 2;
    private GameObject enemyToDestroy;

    private void Awake()
    {
        SelectNewEnemy();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeleteEnemy(enemyToDestroy);
            CreateEnemy(enemyToSpawn);
        }
    }

    private void CreateEnemy(EnemySO enemySO)
    {
        if (enemyToSpawn != null)
        {
            Transform enemyPrefab = Instantiate(enemySO.prefab);
            enemyToDestroy = enemyPrefab.gameObject;
            enemyPrefab.transform.position = GetEnemyStartPositionVector(enemySO.width);
            enemyPrefab.localPosition = Vector3.zero;
            SelectNewEnemy();
        }
    }

    private void SelectNewEnemy()
    {
        int whichEnemy = Random.Range(0, enemiesSOList.Count);
        enemyToSpawn = enemiesSOList[whichEnemy];
    }

    private void DeleteEnemy(GameObject enemy)
    {
        if (enemy != null)
        {
            Destroy(enemy);
        }
    }

    private Vector3 GetEnemyStartPositionVector(float enemyWidth)
    {
        Vector3 positionVector = new Vector3 ((halfScreenSize / unitsForPixel) + (enemyWidth / unitsForPixel), 0, 0);
        return positionVector;
    }
}

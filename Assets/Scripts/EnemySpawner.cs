using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySO> enemiesSOList = new List<EnemySO>();
    [SerializeField] [Range(-1, 1)] private int whitchLine;
    private EnemySO enemyToSpawn;
    private int unitsForPixel = 100;
    private float halfScreenWidth = Screen.width / 2;
    private int numberOfLines = 3;
    private float lineHeight;
    private GameObject enemyToDestroy;

    private void Awake()
    {
        lineHeight = Screen.height / numberOfLines;
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
        float x = (halfScreenWidth / unitsForPixel) + (enemyWidth / unitsForPixel);
        float y = SetEnemyYPosition(whitchLine);
        Vector3 positionVector = new Vector3 (0, y, 0);
        return positionVector;
    }

    private float SetEnemyYPosition(int line)
    {
        float position = lineHeight / unitsForPixel * line;
        return position;
    }
}

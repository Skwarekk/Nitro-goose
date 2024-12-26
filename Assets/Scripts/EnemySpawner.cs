using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySO> enemiesSOList = new List<EnemySO>();
    private List<GameObject> EnemiesInGame = new List<GameObject>();
    private int whitchLine;
    private EnemySO currentEnemySO;
    private int unitsForPixel = 100;
    private float halfScreenWidth = Screen.width / 2;
    private int numberOfLines = 3;
    private float lineHeight;

    private void Awake()
    {
        lineHeight = Screen.height / numberOfLines;
        SelectNewEnemy();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeleteAllEnemies();
            CreateGroupOfEnemies(currentEnemySO);
        }
    }

    private Transform CreateEnemy(EnemySO enemySO)
    {
        if (currentEnemySO != null)
        {
            Transform enemyPrefab = Instantiate(enemySO.prefab);
            enemyPrefab.transform.position = GetEnemyStartPositionVector(enemySO.width);
            EnemiesInGame.Add(enemyPrefab.gameObject);
            return enemyPrefab;
        }
        else
        {
            return null;
        }
    }

    private void SelectNewEnemy()
    {
        int whichEnemy = Random.Range(0, enemiesSOList.Count);
        currentEnemySO = enemiesSOList[whichEnemy];

    }

    private Vector3 GetEnemyStartPositionVector(float enemyWidth)
    {
        int line = Random.Range(-1, 1 + 1);
        float x = (halfScreenWidth / unitsForPixel) + (enemyWidth / unitsForPixel);
        float y = lineHeight / unitsForPixel * line;
        Vector3 positionVector = new Vector3(0, y, 0);
        return positionVector;
    }

    private float GetEnemyYPositionLine(GameObject enemy)
    {
        return (enemy.transform.position.y * unitsForPixel) / lineHeight;
    }

    private void DeleteAllEnemies()
    {
        foreach (GameObject enemy in EnemiesInGame)
        {
            Destroy(enemy);
        }
        EnemiesInGame.Clear();
    }

    private void CreateGroupOfEnemies(EnemySO enemySO)
    {
        GameObject firstEnemy, secondEnemy;
        while (true)
        {
            firstEnemy = CreateEnemy(enemySO).gameObject;
            secondEnemy = CreateEnemy(enemySO).gameObject;
            if (GetEnemyYPositionLine(firstEnemy) != GetEnemyYPositionLine(secondEnemy))
            {
                break;
            }
            else
            {
                Destroy(firstEnemy);
                Destroy(secondEnemy);
            }
        }
        SelectNewEnemy();
    }
}

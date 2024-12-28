using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySO> enemiesSOList = new List<EnemySO>();
    private List<GameObject> EnemiesInGame = new List<GameObject>();
    private int whitchLine;
    private EnemySO currentEnemySO;
    private float lineHeight;

    private void Awake()
    {
        int numberOfLines = 3;
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
        float x = (GameManager.Instance.halfScreenWidth / GameManager.Instance.unitsForPixel) + (enemyWidth / GameManager.Instance.unitsForPixel);
        float y = lineHeight / GameManager.Instance.unitsForPixel * line;
        Vector3 positionVector = new Vector3(0, y, 0);
        return positionVector;
    }

    private float GetEnemyYPositionLine(GameObject enemy)
    {
        return (enemy.transform.position.y * GameManager.Instance.unitsForPixel) / lineHeight;
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

using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemySO> enemiesSOList = new List<EnemySO>();
    private List<GameObject> EnemiesInGame = new List<GameObject>();
    private int whitchLine;
    private EnemySO currentEnemySO;
    private float lineHeight;

    private void Awake()
    {
        SelectNewEnemy();
        int numberOfLines = 3;
        lineHeight = Screen.height / numberOfLines;
    }

    private void Update()
    {
        List<GameObject> enemiesToDestroy = new List<GameObject>();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateGroupOfEnemies();
        }

        foreach (GameObject enemy in EnemiesInGame)
        {
            if (enemy != null) 
            {
                MoveEnemy(enemy);

                if (enemy.transform.position.x < -((GameManager.Instance.halfScreenWidth + currentEnemySO.width) / GameManager.Instance.unitsForPixel))
                {
                    enemiesToDestroy.Add(enemy);
                }
            }
        }

        foreach(GameObject enemy in enemiesToDestroy)
        {
            Destroy(enemy);
            EnemiesInGame.Remove(enemy);
        }
    }

    private void SelectNewEnemy()
    {
        int whichEnemy = Random.Range(0, enemiesSOList.Count);
        currentEnemySO = enemiesSOList[whichEnemy];

    }

    private Transform CreateEnemy()
    {
        if (currentEnemySO != null)
        {
            Transform enemyPrefab = Instantiate(currentEnemySO.prefab);
            enemyPrefab.transform.position = GetEnemyStartPositionVector();
            EnemiesInGame.Add(enemyPrefab.gameObject);
            return enemyPrefab;
        }
        else
        {
            return null;
        }
    }

    private Vector3 GetEnemyStartPositionVector()
    {
        int line = Random.Range(-1, 1 + 1);
        float x = (GameManager.Instance.halfScreenWidth / GameManager.Instance.unitsForPixel) + (currentEnemySO.width / GameManager.Instance.unitsForPixel);
        float y = lineHeight / GameManager.Instance.unitsForPixel * line;
        Vector3 positionVector = new Vector3(x, y, 0);
        return positionVector;
    }

    private float GetEnemyYPositionLine(GameObject enemy)
    {
        return (enemy.transform.position.y * GameManager.Instance.unitsForPixel) / lineHeight;
    }

    private void CreateGroupOfEnemies()
    {
        SelectNewEnemy();
        GameObject firstEnemy, secondEnemy;
        while (true)
        {
            firstEnemy = CreateEnemy().gameObject;
            secondEnemy = CreateEnemy().gameObject;
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
    }

    private void DeleteAllEnemies()
    {
        foreach (GameObject enemy in EnemiesInGame)
        {
            Destroy(enemy);
        }
        EnemiesInGame.Clear();
    }

    private void MoveEnemy(GameObject enemy)
    {
        enemy.transform.position -= new Vector3(currentEnemySO.speed * Time.deltaTime, 0, 0);
    }
}
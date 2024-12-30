using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Transform> enemyPrefabsList = new List<Transform>();
    [SerializeField] private float speed = 10;
    private List<GameObject> EnemiesInGame = new List<GameObject>();
    private int whitchLine;
    private Transform currentEnemyPrefab;
    private float lineHeight;

    private void Awake()
    {
        int numberOfLines = 3;
        lineHeight = Screen.height / numberOfLines;
    }

    private void Update()
    {
        List<GameObject> enemiesToDestroy = new List<GameObject>();
        foreach (GameObject enemy in EnemiesInGame)
        {
            if(enemy != null)
            {
                MoveEnemy(enemy);

                if (enemy.transform.position.x < -transform.position.x)
                {
                    enemiesToDestroy.Add(enemy);
                }
            }
        }

        foreach (GameObject enemyToDestroy in enemiesToDestroy)
        {
            Destroy(enemyToDestroy);
            EnemiesInGame.Remove(enemyToDestroy);
        }
    }

    private void SelectNewEnemy()
    {
        int whichEnemy = Random.Range(0, enemyPrefabsList.Count);
        currentEnemyPrefab = enemyPrefabsList[whichEnemy];

    }

    private Transform CreateEnemy()
    {
        SelectNewEnemy();
        if (currentEnemyPrefab != null)
        {
            Transform enemyPrefab = Instantiate(currentEnemyPrefab);
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
        float x = transform.position.x;
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
        GameObject firstEnemy, secondEnemy;
        firstEnemy = CreateEnemy().gameObject;
        while (true)
        {
            secondEnemy = CreateEnemy().gameObject;
            if (GetEnemyYPositionLine(firstEnemy) != GetEnemyYPositionLine(secondEnemy))
            {
                break;
            }
            else
            {
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
        if (enemy != null)
        {
            enemy.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }
}
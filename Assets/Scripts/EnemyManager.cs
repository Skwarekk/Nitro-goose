using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform[] enemyPrefabsArray;
    [SerializeField] private float speed = 10;
    [SerializeField][Range(1.0f, 2.0f)] private float enemySpawnInterval = 1.5f;
    [SerializeField][Range(1, 4)] private int howMuchEnemies;
    [SerializeField][Range(0.5f, 1f)] private float enemySize = 0.7f;
    private List<GameObject> enemiesInGame = new List<GameObject>();
    private int whitchLine;
    private Transform currentEnemyPrefab;
    private float lineHeight;

    private void Awake()
    {
        enemySize = 0.7f;

        int numberOfLines = 5;
        lineHeight = Screen.height / numberOfLines;
    }

    private void Update()
    {
        List<GameObject> enemiesToDestroy = new List<GameObject>();
        foreach (GameObject enemy in enemiesInGame)
        {
            if (enemy != null)
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
            enemiesInGame.Remove(enemyToDestroy);
        }
    }

    public void CreateGroupOfEnemies()
    {
        List<float> takenLines = new List<float>();
        for (int i = 1; i <= howMuchEnemies; i++)
        {
            SelectNewEnemy();
            while (true)
            {    
                GameObject enemy = CreateEnemy().gameObject;
                if (!takenLines.Contains(GetEnemyYPositionLine(enemy)))
                {
                    takenLines.Add(GetEnemyYPositionLine(enemy));
                    break;
                }
                else
                {
                    Destroy(enemy);
                }
            }
        }
    }

    public float GetEnemySpawnInterval()
    {
        return enemySpawnInterval;
    }

    private void SelectNewEnemy()
    {
        int whichEnemy = Random.Range(0, enemyPrefabsArray.Length);
        currentEnemyPrefab = enemyPrefabsArray[whichEnemy];

    }

    private Transform CreateEnemy()
    {
        if (currentEnemyPrefab != null)
        {
            Transform enemyPrefab = Instantiate(currentEnemyPrefab);
            enemyPrefab.transform.position = GetEnemyStartPositionVector();
            AddColider(enemyPrefab.gameObject);
            SetSize(enemyPrefab.gameObject);
            enemiesInGame.Add(enemyPrefab.gameObject);
            return enemyPrefab;
        }
        else
        {
            return null;
        }
    }

    private Vector3 GetEnemyStartPositionVector()
    {
        int line = Random.Range(-2, 2 + 1);
        float x = transform.position.x;
        float y = lineHeight / GameManager.Instance.unitsForPixel * line;
        Vector3 positionVector = new Vector3(x, y, 0);
        return positionVector;
    }

    private float GetEnemyYPositionLine(GameObject enemy)
    {
        return (enemy.transform.position.y * GameManager.Instance.unitsForPixel) / lineHeight;
    }

    private void DeleteAllEnemies()
    {
        foreach (GameObject enemy in enemiesInGame)
        {
            Destroy(enemy);
        }
        enemiesInGame.Clear();
    }

    private void MoveEnemy(GameObject enemy)
    {
        if (enemy != null)
        {
            enemy.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }

    private void AddColider(GameObject enemy)
    {
        float enemySize = 4;
        BoxCollider2D collider = enemy.AddComponent<BoxCollider2D>(); 
        collider.size = new Vector2(enemySize, lineHeight / GameManager.Instance.unitsForPixel);
        collider.isTrigger = true;
    }

    private void SetSize(GameObject enemy)
    {
        enemy.transform.localScale = new Vector3(enemySize, enemySize, enemySize);
    }
}
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySO> enemiesSOList = new List<EnemySO>();
    [SerializeField] private int whichEnemy;
    private EnemySO currentEnemy;
    private int unitsForPixel = 100;
    private float halfScreenSize = Screen.width / 2;

    private void Start()
    {
        if(whichEnemy >= 0 && whichEnemy < enemiesSOList.Count)
        {
            currentEnemy = enemiesSOList[whichEnemy];
        }
        CreateEnemy(currentEnemy);
    }

    private void CreateEnemy(EnemySO enemySO)
    {
        if(currentEnemy != null)
        {
            Transform enemyPrefab = Instantiate(enemySO.prefab);
            enemyPrefab.transform.position = GetEnemyStartPositionVector(enemySO.width);
        }
    }

    private Vector3 GetEnemyStartPositionVector(float enemyWidth)
    {
        Vector3 positionVector = new Vector3 ((halfScreenSize / unitsForPixel) + (enemyWidth / unitsForPixel), 0, 0);
        return positionVector;
    }
}

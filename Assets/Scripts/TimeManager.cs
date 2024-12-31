using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;

    private void Start()
    {
        StartCoroutine(EnemyCycle());
    }

    private IEnumerator EnemyCycle()
    {
        yield return new WaitForSeconds(enemyManager.GetEnemySpawnInterval());
        enemyManager.CreateGroupOfEnemies();
        StartCoroutine(EnemyCycle());
    }
}

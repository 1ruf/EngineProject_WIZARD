using Blade.Enemies;
using System.Collections.Generic;
using Unity.AppUI.Core;
using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyList;
    [SerializeField] private List<Transform> spawnLocations;

    [SerializeField] private GameObject returnPortal;

    private int _currentRound;
    private int _spawnCnt = 0;

    private int _enemyCnt;

    private void OnEnable()
    {
        RoundStart();
    }

    private void RoundStart()
    {
        if (_currentRound < 3)
        {
            //ClearEnemys();
            SpawnEnemy();
        }
    }

    private void ClearEnemys()
    {
        if(transform.childCount == 0) return;
        foreach (GameObject enemy in transform)
        {
            Destroy(enemy);
        }
    }

    private void SpawnEnemy()
    {
        _spawnCnt++;
        for (int i = 0; i < _currentRound + Random.Range(3, 5); ++i)
        {
            Spawn(Random.Range(0, enemyList.Count));
            _enemyCnt++;
        }
    }

    private void Spawn(int idx)
    {
        GameObject enemyObj = Instantiate(enemyList[idx].gameObject, spawnLocations[Random.Range(0, spawnLocations.Count)].position, Quaternion.identity, transform);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.OnDeathEvent.AddListener(HandleEnemyCounter);
        //if (enemy != null)
        //{
        //    UnityEngine.Events.UnityAction handler = null;
        //    handler = () =>
        //    {
        //        HandleEnemyCounter();
        //        enemy.OnDeathEvent.RemoveListener(handler);
        //    };
        //    
        //}
    }

    private void HandleEnemyCounter()
    {
        _enemyCnt--;
        if(_enemyCnt <= 0)
        {
            _currentRound++;
            if (_currentRound < 3)
            {
                RoundStart();
            }
            else
            {
                returnPortal.SetActive(true);
            }
        }
    }
}

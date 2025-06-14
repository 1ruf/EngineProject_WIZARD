using Blade.Enemies;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyList;
    [SerializeField] private List<Transform> spawnLocations;
    [SerializeField] private UnityEngine.Canvas canvas;

    [SerializeField] private TextMeshProUGUI titleText;

    [SerializeField] private GameObject spawnIndicatorPrefab;
    [SerializeField] private GameObject spawnEffectPrefab;

    [SerializeField] private GameObject returnPortal;

    private string _SAVED_ROUND_KEY = "SavedRound";

    private int _savedRound;

    private int _currentRound;
    private int _spawnCnt = 0;

    private int _enemyCnt;

    private List<int> _spawned = new List<int>();

    private void OnEnable()
    {
        RoundStart();
        StartCoroutine(SetText(titleText, $"시작\n레벨:{_savedRound}"));
    }

    private void RoundStart()  
    {
        _spawned.Clear();
        _savedRound = PlayerPrefs.GetInt(_SAVED_ROUND_KEY, 0);
        if (_currentRound < 3)
        {
            //ClearEnemys();
            SpawnEnemy();
        }
        else
        {
            StartCoroutine(SetText(titleText, "종료"));
            returnPortal.SetActive(true);
            PlayerPrefs.SetInt(_SAVED_ROUND_KEY, ++_savedRound);
        }
    }

    private void ClearEnemys()
    {
        if (transform.childCount == 0) return;
        foreach (GameObject enemy in transform)
        {
            Destroy(enemy);
        }
    }

    private void SpawnEnemy()
    {
        int enemyCount = _savedRound * 2 / 3 + Random.Range(4, 6);
        _spawnCnt++;
        for (int i = 1; i < enemyCount; ++i)
        {
            StartCoroutine(Spawn(Random.Range(0, enemyList.Count)));
            _enemyCnt++;
        }
    }

    private IEnumerator Spawn(int idx)
    {
        int locationNum = 0;
        while (true)
        {
            if (_spawned.Count == spawnLocations.Count) spawnLocations.Clear();
            locationNum = Random.Range(0, spawnLocations.Count);
            if (_spawned.Contains(locationNum) == false)
                break;
            yield return null;
        }
        _spawned.Add(locationNum);
        Vector3 sP = spawnLocations[locationNum].position;

        GameObject indicator = Instantiate(spawnIndicatorPrefab, canvas.transform);
        GameObject effect = Instantiate(spawnEffectPrefab, null);
        effect.transform.position = sP;
        RectTransform indicatorRect = indicator.GetComponent<RectTransform>();

        float elapsed = 0f;
        float duration = 3f;

        while (elapsed < duration)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(sP);
            indicatorRect.position = screenPos;

            elapsed += Time.deltaTime;
            yield return null;
        }

        GameObject enemyObj = Instantiate(enemyList[idx].gameObject, sP, Quaternion.identity, transform);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.OnDeathEvent.AddListener(HandleEnemyCounter);

        Destroy(indicator);
        yield return new WaitForSeconds(3f);
        Destroy(effect);
    }


    private void HandleEnemyCounter()
    {
        _enemyCnt--;
        if (_enemyCnt <= 0)
        {
            _currentRound++;
            if (_currentRound < 3)
            {
                RoundStart();
            }
            else
            {
                StartCoroutine(SetText(titleText, "종료"));
                returnPortal.SetActive(true);
                PlayerPrefs.SetInt(_SAVED_ROUND_KEY,++_savedRound);
            }
        }
    }

    private IEnumerator SetText(TextMeshProUGUI tmp,string text)
    {
        tmp.text = text;
        tmp.rectTransform.DOAnchorPosY(420, 0.5f);
        yield return new WaitForSeconds(1.5f);
        tmp.rectTransform.DOAnchorPosY(1000, 0.5f);
    }
}

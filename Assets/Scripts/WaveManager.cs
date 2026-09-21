using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

[System.Serializable]

public class WaveData
{
    public float duration = 10f;
    public int easyEnemies = 5;
    public int hardenemies = 2;
}
public class WaveManager : MonoBehaviour
{
    public WaveData[] waves;
    public Button startWaveButton;

    public GameObject easyEnemyprefab;
    public GameObject hardEnemyprefab;

    public Transform[] wayPoints;

    public TextMeshProUGUI waveTxt;

    private int currentWaveIndex = 0;
    private bool waveRunning = false;

    void Start()
    {
        startWaveButton.onClick.AddListener(StartWave);
    }

    public void StartWave()
    {
        if (waveRunning) return;
        if (currentWaveIndex >= waves.Length) return;

        StartCoroutine(RunWave());


    }

    IEnumerator RunWave()
    {
        waveRunning = true;
        startWaveButton.interactable = false;

        WaveData wave = waves[currentWaveIndex];
        for (int i = 0; i < wave.easyEnemies; i++)
        {
            SpawnEnemy(easyEnemyprefab);
            yield return new WaitForSeconds((wave.duration / 3) / wave.easyEnemies);
        }
        for (int i = 0; i < wave.hardenemies; i++)
        {

            SpawnEnemy(hardEnemyprefab);
            yield return new WaitForSeconds((wave.duration / 3) / wave.hardenemies);
        }

        yield return new WaitForSeconds(wave.duration / 3);

        waveRunning = false;
        startWaveButton.interactable = true;
        currentWaveIndex++;
        waveTxt.text = (currentWaveIndex + 1).ToString();

    }

    void SpawnEnemy(GameObject prefab)
    {
        GameObject e = Instantiate(prefab, wayPoints[0].position, Quaternion.identity);
        Enemy enemy = e.GetComponent<Enemy>();
        enemy.wayPoints = wayPoints;
    }
}

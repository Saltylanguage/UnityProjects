using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startIndex = 0;
    [SerializeField] bool loopWaves = false;

    // Use this for initialization
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (loopWaves);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int index = startIndex; index < waveConfigs.Count; index++)
        {
            var currentWave = waveConfigs[index];
            yield return StartCoroutine(SpawnEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnEnemiesInWave(WaveConfig wave)
    {

        for (int i = 0; i < wave.GetNumberOfEnemies(); i++)
        {
            var newEnemy = Instantiate(wave.GetEnemyPrefab(), wave.GetPathWaypoints()[0].transform.position, wave.GetPathWaypoints()[0].rotation);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(wave);
            yield return new WaitForSeconds(wave.GetSpawnDelay());
        }
    }
}

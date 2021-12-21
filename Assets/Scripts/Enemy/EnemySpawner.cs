using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    ///////////Serialized variables///////////////////////////
    [SerializeField] private List<WaveConfig> _waveConfigs;
    [SerializeField] private bool _isLooping = false;
    
    private int _startingWave = 0;
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }while(_isLooping);
        
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = _startingWave; waveIndex < _waveConfigs.Count; waveIndex++)
        {
            var currentWave = _waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int i = 0; i < waveConfig.GetNumOfEnemies(); i++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                                       waveConfig.GetWaypoints()[0].transform.position,
                                       Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
    
}

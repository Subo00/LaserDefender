using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    ///////////Serialized variables///////////////////////////
    [SerializeField] private List<WaveConfig> _waveConfigs;
    [SerializeField] private bool _isLooping = false;


    ////////////Private//////////////////////////////
    [SerializeField] private ObjectPool[] _enemyPools;
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
            
            GameObject enemy = _enemyPools[waveConfig.GetEnemyPoolIndex()].GetPooledObject(); //get enemy out of pool
            
            if(enemy != null)
            {
                enemy.transform.position = waveConfig.GetWaypoints()[0].transform.position; //set it's position
                enemy.transform.rotation = Quaternion.identity;                             //and rotation
                enemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);               //and it's path
                enemy.SetActive(true);
            }
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
    
}

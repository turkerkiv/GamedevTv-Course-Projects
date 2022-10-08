using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> _waveConfigs;
    [SerializeField] float _timeBetweenWaves = 0;
    [SerializeField] bool _isLooping;

    WaveConfigSO _currentWave;

    void Start()
    {
        StartCoroutine(StartEnemyWaves());
        _isLooping = true;
    }

    public WaveConfigSO GetCurrentWave()
    {
        return _currentWave;
    }

    IEnumerator StartEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in _waveConfigs)
            {
                _currentWave = wave;

                for (int i = 0; i < _currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(_currentWave.GetEnemyPrefab(i), _currentWave.GetStartingWaypoint().position, Quaternion.Euler(0,0,180), transform);
                    yield return new WaitForSeconds(_currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(_timeBetweenWaves);
            }
        }
        while (_isLooping);
    }
}

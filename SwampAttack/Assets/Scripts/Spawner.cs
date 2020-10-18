using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    public event UnityAction AllEnemySpawned;
    public event UnityAction<int, int> OnSpawnEnemy;

    private void Start()
    {
        SetWave(_currentWaveNumber);    
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if(_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
            OnSpawnEnemy?.Invoke(_spawned, _currentWave.Count);
        }

        if(_currentWave.Count <= _spawned)
        {
            if (_waves.Count > _currentWaveNumber + 1)
                AllEnemySpawned?.Invoke();

            _currentWave = null;
        }
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Diyng += OnEnemyDiyng;
    }

    public void OnEnemyDiyng(Enemy enemy)
    {
        enemy.Diyng -= OnEnemyDiyng;

        _player.AddMoney(enemy.Reward);
    }

    private void SetWave(int index)
    {
        if (index < _waves.Count)
        {
            _currentWave = _waves[index];
            _currentWaveNumber = index;
        }
    }

    public void NextWave()
    {
        SetWave(++_currentWaveNumber);
        _spawned = 0;
        OnSpawnEnemy?.Invoke(_spawned, _currentWave.Count);
    }
}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
}
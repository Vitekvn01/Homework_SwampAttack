using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _wavs;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentVave;
    private int _currentWaveNumber = 0; 
    private float _timerAfterLastSpawn; 
    private int _spawned;

    private List<Enemy> _enemies = new List<Enemy>();

    public event Action AllEnemySpawned;
    public event Action<int, int> EnemyCountChanged;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentVave == null)
        {
            return;
        }

        _timerAfterLastSpawn += Time.deltaTime;

        if (_timerAfterLastSpawn >= _currentVave.Delay)  
        {
            InstantiateEnemy();
            _spawned++;
            _timerAfterLastSpawn = 0;
            EnemyCountChanged?.Invoke(_spawned, _currentVave.Count);
        }

        if (_currentVave.Count <= _spawned)  
        {
            if (_wavs.Count > _currentWaveNumber + 1)
            {
                AllEnemySpawned?.Invoke();   
            }
            _currentVave = null;
        }
    }

    private void OnDestroy()
    {
        foreach (var enemy in _enemies)
        {
            if (enemy != null)
            {
                enemy.OnDieEvent -= OnEnemyDieng;
            }
        }
    }

    public void NextWave() 
    {
        SetWave(++_currentWaveNumber);
        _spawned = 0;
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentVave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.OnDieEvent += OnEnemyDieng;
        _enemies.Add(enemy);
    }

    private void SetWave(int index)
    {
        _currentVave = _wavs[index];
        EnemyCountChanged?.Invoke(0, 1);
    }

    private void OnEnemyDieng(Enemy enemy)
    {
        enemy.OnDieEvent -= OnEnemyDieng;
        _player.AddMoney(enemy.Reward);
        _enemies.Remove(enemy);
    }
}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public int Count;
    public int Delay;
}
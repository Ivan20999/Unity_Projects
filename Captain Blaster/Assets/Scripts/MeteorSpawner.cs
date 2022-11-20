using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    [SerializeField] GameObject _meteorPrefab;
    [SerializeField] float _minSpawnDelay = 1f;
    [SerializeField] float _maxSpawnDelay = 3f;
    [SerializeField] float _spawnXLimit = 6f;

    float _timeRespawn;

    public void TimeRespawn()
    {
        _timeRespawn += Time.deltaTime;
        //Invoke("Spawn", Time.deltaTime * Random.Range(_minSpawnDelay, _maxSpawnDelay));

        if ((Random.Range(_minSpawnDelay, _maxSpawnDelay) <= _timeRespawn))
        {
            Spawn();
            _timeRespawn = 0f;
            _minSpawnDelay -= 0.01f;
            _maxSpawnDelay -= 0.01f;

        }
    }

    void Spawn()
    {
        float random = Random.Range(-_spawnXLimit, _spawnXLimit);
        Vector3 spawnPos = transform.position + new Vector3(random, 0f, 0f);
        Instantiate(_meteorPrefab, spawnPos, Quaternion.identity);
    }
}

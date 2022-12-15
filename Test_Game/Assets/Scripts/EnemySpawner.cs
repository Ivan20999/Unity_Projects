using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject _enemyPrefab;
    public float _spawnCycle = 0.5f;

    private float _timer = 0;

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnCycle)
        {

            GameObject temp = Instantiate(_enemyPrefab) as GameObject;
            Vector3 position = temp.transform.position;
            position.z = Random.Range(-7f, 7f);
            temp.transform.position = position;

            _timer = 0;
        }
    }
}

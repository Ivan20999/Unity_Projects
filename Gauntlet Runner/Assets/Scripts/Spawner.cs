using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject _powerupPrefab;
    public GameObject _obstaclePrefab;
    public float _spawnCycle = 0.5f;

    GameManager _manager;
    float _elapsedTime;
    bool _spawnPowerup = true;

    // Start is called before the first frame update
    void Start()
    {
        _manager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _spawnCycle)
        {
            GameObject temp;
            if (_spawnPowerup)
                temp = Instantiate(_powerupPrefab) as GameObject;
            else
                temp = Instantiate(_obstaclePrefab) as GameObject;

            Vector3 position = temp.transform.position;
            position.x = Random.Range(-3f, 3f);
            temp.transform.position = position;

            //Collidable col = temp.GetComponent<Collidable>();
            //col._manager = _manager;

            _elapsedTime = 0;
            _spawnPowerup = !_spawnPowerup;
        }
    }
}

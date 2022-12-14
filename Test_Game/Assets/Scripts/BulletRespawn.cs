using UnityEngine;

public class BulletRespawn : MonoBehaviour
{
    public GameObject _bulletPrefab;

    float _timer = 0;

    public float _spawnCycle = 2f;

    private void Start()
    {
        //_spawnCycle = gameObject.GetComponent<Damage>()._attackSpeed;
        //_spawnCycle = _spawnCycle / 10;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnCycle)
        {
            Instantiate(_bulletPrefab, transform.position, transform.rotation);
            _timer = 0;
        }
    }
}

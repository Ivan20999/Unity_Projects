using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{

    [SerializeField] GameManager _gameManager;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _speed = 10f;
    [SerializeField] float _xLimit = 7f;
    [SerializeField] float _yLimit = 7f;
    [SerializeField] float _reloadTime = 0.5f;
    [SerializeField] Joystick _joystick;

    float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        //float xInput = Input.GetAxis("Horizontal");
        float xInput = _joystick.Horizontal;
        //float yInput = Input.GetAxis("Vertical");
        float yInput = _joystick.Vertical;
        transform.Translate(xInput * _speed * Time.deltaTime, yInput * _speed * Time.deltaTime, 0f);

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -_xLimit, _xLimit);
        position.y = Mathf.Clamp(position.y, -_yLimit, _yLimit);
        transform.position = position;

    }

    public void Fire()
    {
        if (elapsedTime > _reloadTime)
        {
            Vector3 spawnPos = transform.position;
            spawnPos += new Vector3(0, 1.2f, 0);
            Instantiate(_bulletPrefab, spawnPos, Quaternion.identity);
            elapsedTime = 0f; // Reset bullet ? ring timer
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _gameManager.PlayerDied();
    }
}

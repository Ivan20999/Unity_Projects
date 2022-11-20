using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{

    GameManager _manager;
    public float _moveSpeed = 20f;
    public float _timeAmount = 1.5f;

    // Start is called before the first frame update
    void Update()
    {
        transform.Translate(0, 0, -_moveSpeed * Time.deltaTime);
        _manager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
{
            _manager.AdjustTime(_timeAmount);
            Destroy(gameObject);
        }
    }
}

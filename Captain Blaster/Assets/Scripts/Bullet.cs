using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float speed = 10f;

    GameManager _gameManager; // Note this is private this time

    void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0f, speed);
    }

    private void Update()
    {
        Destroy(gameObject,2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject); 
        _gameManager.AddScore();
        Destroy(gameObject);
    }
}

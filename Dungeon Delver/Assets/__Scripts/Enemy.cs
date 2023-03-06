using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected static Vector3[] directions = new Vector3[]
    {
       Vector3.right, Vector3.up,Vector3.left, Vector3.down 
    };

    [Header("Set in Inspector : Enemy")]
    public float maxHealth = 1;

    [Header("Set Dynamically: Enemy")]
    public float health;

    protected Animator _anim;
    protected Rigidbody _rigid;
    protected SpriteRenderer _sRend;

    protected virtual void Awake()
    {
        health = maxHealth;
        _anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody>();
        _sRend = GetComponent<SpriteRenderer>();
    }
}


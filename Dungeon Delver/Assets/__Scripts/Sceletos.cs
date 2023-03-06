using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sceletos : Enemy
{
    [Header("Set in Inspector: Skeletos")]
    public int speed = 2;
    public float timeThinkMin = 1f;
    public float timeThinkMax = 4f;

    [Header("Set Dynamically: Skeletos")]
    public int facing = 0;
    public float timeNextDecision = 0;

    private void Update()
    {
        if(Time.time >= timeNextDecision)
        {
            DecideDirection();
        }
        //Поле _rigid уначледовано от класса Enemy и инициализируется
        //в Enemy.Awake
        _rigid.velocity = directions[facing] * speed;
    }

    void DecideDirection()
    {
        facing = Random.Range(0, 4);
        timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax);
    }
}

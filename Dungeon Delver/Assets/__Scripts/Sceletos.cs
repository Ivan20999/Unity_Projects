using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sceletos : Enemy, IFacingMover
{
    [Header("Set in Inspector: Skeletos")]
    public int speed = 2;
    public float timeThinkMin = 1f;
    public float timeThinkMax = 4f;

    [Header("Set Dynamically: Skeletos")]
    public int facing = 0;
    public float timeNextDecision = 0;

    private InRoom _inRm;

    protected override void Awake()
    {
        base.Awake();
        _inRm = GetComponent<InRoom>();
    }

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

    // Реализаия интерфейса IFacingMover
    public int GetFacing()
    {
        return facing;
    }

    public bool moving
    {
        get { return true; }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float gridMult
    {
        get { return _inRm.gridMult; }
    }

    public Vector2 roomPos
    {
        get { return _inRm.roomPos; }
        set { _inRm.roomPos = value; }
    }

    public Vector2 roomNum
    {
        get { return _inRm.roomNum; }
        set { _inRm.roomNum = value; }
    }

    public Vector2 GetRoomPosOnGrid(float mult = -1)
    {
        return _inRm.GetRoomPosOnGrid(mult);
    }
}

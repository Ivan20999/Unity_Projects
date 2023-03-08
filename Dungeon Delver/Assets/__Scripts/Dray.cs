using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dray : MonoBehaviour, IFacingMover
{
    public enum eMode { idle, move, attack, transition }

    [Header("Set in Inspector")]
    public float speed = 5;
    public float attackDuration = 0.25f; //Продолжительность атаки в секундах
    public float attackDelay = 0.5f;     //Задержка между атаками

    [Header("Set Dynamically")]
    public int dirHeld = -1;//Направление, соотвествующее удерживаемой клавише
    public int facing = 1;  //Направление движения Дрея
    public eMode mode = eMode.idle;

    private float timeAtkDone = 0;
    private float timeAtkNext = 0;

    private Rigidbody _rigid;
    private Animator _anim;
    private InRoom _inRm;

    private Vector3[] _directions = new Vector3[] {
        Vector3.right, Vector3.up, Vector3.left, Vector3.down};

    private KeyCode[] _keys = new KeyCode[] { KeyCode.RightArrow,
    KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow};

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _inRm = GetComponent<InRoom>();
    }

    private void Update()
    {
        //----Обработка ввода с кливаатуры и управление режимами eMode----
        dirHeld = -1;

        for (int i = 0; i < _keys.Length; i++)
        {
            if (Input.GetKey(_keys[i])) dirHeld = i;
        }

        // Нажата клавиша атаки
        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= timeAtkNext)
        {
            mode = eMode.attack;
            timeAtkDone = Time.time + attackDuration;
            timeAtkNext = Time.time + attackDelay;
        }

        // Завершить атаку, если время истекло
        if (Time.time >= timeAtkDone)
        {
            mode = eMode.idle;
        }

        // Выбрать правильный режим, если Дрей не атакует
        if (mode != eMode.attack)
        {
            if (dirHeld == -1)
            {
                mode = eMode.idle;
            }
            else
            {
                facing = dirHeld;
                mode = eMode.move;
            }
        }

        //----Действия в текущем режиме----
        Vector3 vel = Vector3.zero;
        switch (mode)
        {
            case eMode.attack:
                _anim.CrossFade("Dray_Attack_" + facing, 0);
                _anim.speed = 0;
                break;

            case eMode.idle:
                _anim.CrossFade("Dray_Walk_" + facing, 0);
                _anim.speed = 0;
                break;

            case eMode.move:
                vel = _directions[dirHeld];
                _anim.CrossFade("Dray_Walk_" + facing, 0);
                _anim.speed = 1;
                break;

        }

        _rigid.velocity = vel * speed;
    }

    // Реализаия интерфейса IFacingMover
    public int GetFacing()
    {
        return facing;
    }

    public bool moving
    {
        get { return (mode == eMode.move); }
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

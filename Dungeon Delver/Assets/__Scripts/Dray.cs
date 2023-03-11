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
    public float transitionDelay = 0.5f; //Задержка перехода между комнатами

    [Header("Set Dynamically")]
    public int dirHeld = -1;//Направление, соотвествующее удерживаемой клавише
    public int facing = 1;  //Направление движения Дрея
    public eMode mode = eMode.idle;

    private float timeAtkDone = 0;
    private float timeAtkNext = 0;

    private float transitionDone = 0;
    private Vector2 transitionPos;

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
        if(mode == eMode.transition)
        {
            _rigid.velocity = Vector3.zero;
            _anim.speed = 0;
            roomPos = transitionPos; //Оставить Дрея на месте
            if (Time.time < transitionDone) return;
            // Следующая строка выполняется, только если Time.time >= transitionDone
            mode = eMode.idle;
        }

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

    void LateUpdate()
    {
        // Получить координаты узла сетки, с размером ячейки
        // в половину единицы, ближайшего к данному персонажу
        Vector2 rPos = GetRoomPosOnGrid(0.5f); //Размер ячейки в пол-единицы

        // Персонаж находится на плитке с дверью?
        int doorNum;
        for (doorNum = 0; doorNum < 4; doorNum++)
        {
            if (rPos == InRoom.DOORS[doorNum])
                break;
        }

        if (doorNum > 3 || doorNum != facing) return;

        // Перейти в следующую комнату
        Vector2 rm = roomNum;
        switch (doorNum)
        {
            case 0:
                rm.x += 1;
                break;
            case 1:
                rm.y += 1;
                break;
            case 2:
                rm.x -= 1;
                break;
            case 3:
                rm.y -= 1;
                break;
        }

        // Проверить, можно ли выполнить переход в комнату rm
        if(rm.x >=0 && rm.x <= InRoom.MAX_RM_X)
        {
            if(rm.y >=0 && rm.y <= InRoom.MAX_RM_Y)
            {
                roomNum = rm;
                transitionPos = InRoom.DOORS[(doorNum + 2) % 4];
                roomPos = transitionPos;
                mode = eMode.transition;
                transitionDone = Time.time + transitionDelay;
            }
        }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S; //Одиночка

    [Header("Set in Inspector")]
    //Поля, управляющие движением коробля
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;

    //Эта переменная хранит ссылку на последний столкнувшийся игровой обьект
    private GameObject lastTriggerGo = null;

    private void Awake()
    {
        if (S == null)
        {
            S = this; //Сохранить ссылку на одиночку
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attemted to assign second Hero.S!");
        }
    }

    void Update()
    {
        //Извлечение информации из класса Input
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        //Изменить transform.position, опираясь на информацию по осям
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        //Повернуть корабль, чтобы придать ощущение динамизма
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        //Позволить кораблю выстрелить
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }
    }

    void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position; //Присвоение координат
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.up * projectileSpeed; //Присвеоние скорости
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //print("Triggered: " + go.name);

        //Гарантировать невозможность повторного столкновения с тем же обьектом
        if (go == lastTriggerGo)
        {
            return;
        }
        lastTriggerGo = go;

        if (go.tag == "Enemy") //Если защитное поле столкнулось с вражеским кораблем
        {
            shieldLevel--; //Уменьшить уровень защиты
            Destroy(go); //Уничтожить врага
        }
        else
        {
            print("Triggered by non-Enemy: " + go.name);
        }
    }

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            //Если уровень поля упал до нуля или ниже
            if(value < 0)
            {
                Destroy(this.gameObject);
                //Сообщить обьекту Main.S о необходимости перезагрузить игру
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }
}

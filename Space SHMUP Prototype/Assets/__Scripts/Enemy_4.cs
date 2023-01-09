using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy_4 создается за верхней границей, выбирает случайную точку на экране
/// и перемещается к ней. Добравшись до места, выбирает другую случайную точку
/// и продолжает двигаться к ней, пока игрок не уничтожит его.
/// </summary>

public class Enemy_4 : Enemy
{
    public Vector3 p0, p1;//Две точки интерполяции
    private float timeStart; //Время создания коробля
    private float duration = 4;//Продолжительность перемещения

    private void Start()
    {
        //Начальная позиция уже выбрана в Main.SpawnEnemy(),
        //поэтому запишем ее как начальные значения в p0,p1
        p0 = p1 = pos;
        InitMovement();
    }

    void InitMovement()
    {
        p0 = p1;//Переписать p1 в p0
        //выбрать новую точку p1 на экране
        float widMinRad = bndCheck.camWidth - bndCheck.radius;
        float hgtMinRad = bndCheck.camHeight - bndCheck.radius;
        p1.x = Random.Range(-widMinRad, widMinRad);
        p1.y = Random.Range(-hgtMinRad, hgtMinRad);

        //Сбросить время
        timeStart = Time.time;
    }

    public override void Move()
    {
        //Этот метод переопределяет Enemy.Move() и реализует
        //линейную интерполяцию
        float u = (Time.time - timeStart) / duration;

        if (u > 1)
        {
            InitMovement();
            u = 0;
        }

        u = 1 - Mathf.Pow(1 - u, 2);//Применить плавное замедление
        pos = (1 - u) * p0 + u * p1;//Простая линейна интерполяция
        
    }
}


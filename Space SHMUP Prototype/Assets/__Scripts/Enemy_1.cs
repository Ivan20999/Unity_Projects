using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy_1 Расширяем класс Enemy
public class Enemy_1 : Enemy
{
    [Header("Set in Inspector: Enemy_1")]
    //Число секунд полного цикла синусоиды
    public float waveFrequency = 2;
    //Ширина синусойды в метрах
    public float waveWidth = 4;
    public float waveRotY = 45;

    private float xO; //Начальное значение координаты Х
    private float birthTime;

    //Метод Start хорошо подходит для наших целей
    //потому что не используется суперклассом Enemy
    void Start()
    {
        //Установить начальную координату X обьекта Enemy_1
        xO = pos.x;

        birthTime = Time.time;
    }

    //Переопределить функцию Move суперкласса Enemy
    public override void Move()
    {
        //Так как pos - это свойство, нельзя напрямую изменить pos.x
        //поэтому получим pos в виде вектора Vector3, доустпного для изменения
        Vector3 tempPos = pos;
        //Значение theta изменяется с течением времени
        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = xO + waveWidth * sin;
        pos = tempPos;

        //повернуть немного относительно оси Y
        Vector3 rot = new Vector3(0, sin * waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        //Обрабатывать движение вниз, вдоль осиY
        base.Move();

        //print(bndCheck.isOnScreen);

    }

}

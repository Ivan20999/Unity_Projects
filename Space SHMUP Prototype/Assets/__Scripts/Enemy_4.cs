using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Part - еще один сериализуемый класс подобно WeaponDefinition
/// предназначен для хранения данных
/// </summary>
[System.Serializable]
public class Part
{
    //Значение этих трех полей должны определяться в инспекторе
    public string name; //Имя этой части
    public float heath; //Степень стойкости этой части
    public string[] protectedBy; //Другие части, защищающие эту

    //Эти два поля инициализируются автоматически в Start()
    //Кэширование, как здесь, ускорят получение необходимых данных
    [HideInInspector] //Не позволяет следующему полю появиться в инспекторе
    public GameObject go;//Игровой обьект этой части
    [HideInInspector]
    public Material mat;//Материал для отображения повреждений

}

/// <summary>
/// Enemy_4 создается за верхней границей, выбирает случайную точку на экране
/// и перемещается к ней. Добравшись до места, выбирает другую случайную точку
/// и продолжает двигаться к ней, пока игрок не уничтожит его.
/// </summary>

public class Enemy_4 : Enemy
{
    [Header("Set in Inspector: Enemy_4")]
    public Part[] parts; //Массив частей, состовляющих корабль

    public Vector3 p0, p1;//Две точки интерполяции
    private float timeStart; //Время создания коробля
    private float duration = 4;//Продолжительность перемещения

    private void Start()
    {
        //Начальная позиция уже выбрана в Main.SpawnEnemy(),
        //поэтому запишем ее как начальные значения в p0,p1
        p0 = p1 = pos;

        InitMovement();

        //Запись в кэш игровой обьект и материал каждой части в parts
        Transform t;
        foreach(Part prt in parts)
        {
            t = transform.Find(prt.name);
            if(t!= null)
            {
                prt.go = t.gameObject;
                prt.mat = prt.go.GetComponent<Renderer>().material;
            }
        }
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

    //Эти две функции выполняют поиск части в массиве parts n 
    //по имени или по ссылке на игрвой обьект
    Part FindPart(string n)
    {
        foreach(Part prt in parts)
        {
            if(prt.name == n)
            {
                return (prt);
            }
        }
        return (null);
    }

    Part FindPart(GameObject go)
    {
        foreach(Part prt in parts)
        {
            if(prt.go == go)
            {
                return (prt);
            }
        }
        return (null);
    }

    //Эти функции возвращают true, если данная часть уничтожена
    bool Destroyed(GameObject go)
    {
        return (Destroyed(FindPart(go)));
    }

    bool Destroyed(string n)
    {
        return (Destroyed(FindPart(n)));
    }

    bool Destroyed(Part prt)
    {
        if(prt == null) //Если ссылка на часть не была передана
        {
            return (true);
        }
        //Вернуть результат сравнения: prt.health <= 0
        //Если prt.health <= 0, вернуть true (да, была уничтожена)
        return (prt.heath <= 0);
    }

    //Окрашивает в красный только одну часть, а не весь корабль.
    void ShowLocalizedDamage(Material m)
    {
        m.color = Color.red;
        damageDoneTime = Time.time + showDamageDuration;
        showingDamage = true;
    }

    //Переопределить метод OnCollisionEnter из сценария Enemy.cs
    void OnCollisionEnter(Collision coll)
    {
        GameObject other = coll.gameObject;
        switch (other.tag)
        {
            case "ProjectileHero":
                Projectile p = other.GetComponent<Projectile>();
                //Если корабль за границе экрана, не повреждать его.
                if (!bndCheck.isOnScreen)
                {
                    Destroy(other);
                    break;
                }

                //поразить вражеский корабль
                GameObject goHit = coll.contacts[0].thisCollider.gameObject;
                Part prtHit = FindPart(goHit);
                if(prtHit == null)
                {
                    goHit = coll.contacts[0].otherCollider.gameObject;
                    prtHit = FindPart(goHit);
                }

                //Проверить, защищена ли еще эта часть коробля
                if(prtHit.protectedBy != null)
                {
                    foreach(string s in prtHit.protectedBy)
                    {
                        //Если хотя бы одна из защищающих ччастей еще
                        //не разрушена...
                        if (!Destroyed(s))
                        {
                            // не наносить повреждений этой части
                            Destroy(other);//Уничтожить снаряд ProjectileHero
                            return;//выйти, не повреждая Enemy_4
                        }
                    }
                }

                //Эта часть не защищена, нанести ей поврждение
                //Получить разрушающую силу из Projectile.type и Main.WEAP_DICT
                prtHit.heath -= Main.GetWeaponDefinition(p.type).damageOnHit;
                //Показать эффект попадания в часть
                ShowLocalizedDamage(prtHit.mat);
                if (prtHit.heath <= 0)
                {
                    //Вместо разрушения всего коробля
                    //деактивировать уничтоженную часть
                    prtHit.go.SetActive(false);
                }

                //Проверить, был ли корабль полностью разрушен
                bool allDestroyed = true;//Предположить, что разрушен
                foreach(Part prt in parts)
                {
                    if (!Destroyed(prt)) //Если какая-то часть еще существует
                    {
                        allDestroyed = false; // записать false в allDeatroyed
                        break;//и прервать цикл foreach
                    }
                }

                if (allDestroyed) //Если корабль разрушен полность..
                {
                    //уведомить обьект-одиночку Main, что этот корабль ращрушен
                    Main.S.ShipDestroyed(this);
                    //Уничтожить этот обьект Enemy
                    Destroy(this.gameObject);
                }
                Destroy(other);//Уничтожить снаряд ProjectileHero
                break;
        }
        
    }
}


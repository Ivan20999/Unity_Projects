using System.Collections; //Необходим для доступа к массивам и др. коллекциям
using System.Collections.Generic; //Необходим для доступа к спискам и словарям
using UnityEngine; //Необходим для доустпа к Unity

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f; //Скорость в м/с
    public float fireRate = 0.3f; //Секунд между выстрелами (не используется)
    public float health = 10;
    public int score = 100; //Очки за уничтожения этого коробля

    private BoundsCheck bndCheck;

    //Это свойство: метод, действует как поле
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    void Update()
    {
        Move();

        if (bndCheck != null && bndCheck.offDown)
        {
            //Корабль за нижней границей, поэтому его нужно уничтожить
            Destroy(gameObject);

        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if (otherGO.tag == "ProjectileHero")
        {
            Destroy(otherGO); //Уничтожить снаряд
            Destroy(gameObject); //Уничтожить игровой обьект Enemy
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }
}

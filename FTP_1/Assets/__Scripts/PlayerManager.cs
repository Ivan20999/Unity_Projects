using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public int health { get; private set; }
    public int maxHealth { get; private set; }

    public void Startup()
    {
        Debug.Log("Player manager starting...");

        health = 50;       //Эти значения могут быть инициализированы
        maxHealth = 100;   //сохраненными данными.

        status = ManagerStatus.Started;
    }

    public void ChangeHealth(int value) //Другие сценарии не могут напрямую задавать переменную
                                        //health, но могут вызвать эту функцию.
    {
        health += value;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
        }

        Debug.Log("Health: " + health + "/" + maxHealth);


    }
}

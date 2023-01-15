using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 1;

    private void Update()
    {
        transform.Translate(0,0,speed*Time.deltaTime);
    }

    void OnTriggerEnter (Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        //Проверяем, является ли этот другой обьект обьектом PlayerCharacter
        if(player != null)
        {
            //Debug.Log("Player hit");
            player.Hurt(damage);
        }
        Destroy(this.gameObject);
    }

}

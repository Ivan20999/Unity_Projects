using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    GameObject _enemy;

    private void Update()
    {
        if (_enemy != null)
        {
            GetComponent<BulletRespawn>().enabled = true;
        }
        else
        {
            _enemy = GameObject.FindGameObjectWithTag("Enemy");
            GetComponent<BulletRespawn>().enabled = false;
        }
    }
}

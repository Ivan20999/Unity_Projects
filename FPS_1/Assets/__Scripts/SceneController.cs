using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    //Сериализованный переменная для свзяи с обьектом-шаблоном.
    [SerializeField] private GameObject enemyPrefab;
    //Закрытая переменная для слежения за экземпляром врага в сцене.
    private GameObject _enemy;

    private void Update()
    {
        //Порождаем нового врага, только если враги в сцене отсуствуют.
        if(_enemy == null)
        {
            //Метод, копирующий обьект-шаблон.
            _enemy = Instantiate(enemyPrefab) as GameObject;
            _enemy.transform.position = transform.position;
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0,angle, 0);
        }
        
    }
}

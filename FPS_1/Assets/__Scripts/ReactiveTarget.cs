using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    //Метод, вызывающий сценарий стрельбы.
    public void ReactToHit()
    {
        WanderingAI behavior = GetComponent<WanderingAI>();
        //Проверяем, присоединен ли к персонажу сценарий WanderingAI,
        //он может и отсутсвоват.
        if(behavior != null)
        {
            behavior.SetAlive(false);
        }

        StartCoroutine(Die());
    }

    //Опрокидываем врага, ждем 1.5 секунды и уничтожаем его.
    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);

    }

}

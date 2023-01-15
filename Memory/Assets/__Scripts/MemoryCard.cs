using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    //Эта функция вызывается после щелка на обьекте
    public void OnMouseDown()
    {
        //Запускаем код деактивации, если обьект в данный момент 
        //является активным/видимым.
        if (cardBack.activeSelf)
        {
            //Делаем обьект неактивным/невидимым
            cardBack.SetActive(false);
        }
    }

}

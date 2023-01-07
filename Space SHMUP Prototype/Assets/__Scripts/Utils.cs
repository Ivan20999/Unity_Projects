using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{

    //=================Функции для работы с цветами==================//

    //Возвращает список всех материало в данном игровом обьекте
    //и его дочерних обьектах
    static public Material[] GetAllMaterials(GameObject go)
    {
        Renderer[] rends = go.GetComponentsInChildren<Renderer>();

        List<Material> mats = new List<Material>();
        foreach(Renderer rend in rends)
        {
            mats.Add(rend.material);
        }

        return (mats.ToArray());
    }

}

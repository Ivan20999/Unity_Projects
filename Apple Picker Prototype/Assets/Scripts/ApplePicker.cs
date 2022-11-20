using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Set In Inspector")]
    public GameObject basketPrefab;
    public int numBasket = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;

    private void Start()
    {
        basketList = new List<GameObject>();
        for (int i = 0; i < numBasket; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (i * basketSpacingY);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    public void AppleDestroyed()
    {
        //Удалить все упавшие яблоки
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tGO in tAppleArray)
        {
            Destroy(tGO);
        }

        //Удалить одну корзину
        //Получить индекс последней корзины в basketList
        int basketIndex = basketList.Count - 1;
        //Получить ссылку на этот игровой обьект Basket
        GameObject tBasketGO = basketList[basketIndex];
        //Исключить корзину из списка и удалить сам игровой обьект
        basketList.RemoveAt(basketIndex);
        Destroy(tBasketGO);

        //Если корзин не осталось, перезапустить игру
        if(basketList.Count == 0)
        {
            SceneManager.LoadScene("_Scene_0");
        }
    }
}

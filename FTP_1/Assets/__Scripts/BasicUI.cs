using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    private void OnGUI()
    {
        int posX = 10;
        int posY = 10;
        int width = 100;
        int height = 30;
        int buffer = 10;

        List<string> itemList = Manager.Inventory.GetItemList();
        if (itemList.Count == 0) //Отображает сообщение, информирующее об отсутствии инвентаря.
        {
            GUI.Box(new Rect(posX, posY, width, height), "No Items");
        }
        foreach (string item in itemList)
        {
            int count = Manager.Inventory.GetItemCount(item);
            Texture2D image = Resources.Load<Texture2D>("Icons/" + item);//Метод, загружающий ресурсы из папки Resourse/
            GUI.Box(new Rect(posX, posY, width, height),
                new GUIContent("(" + count + ")", image));
            posX += width + buffer;//При каждом прохождении цикла сдвигаемся в сторону.
        }

        string equipped = Manager.Inventory.equippedItem;
        if (equipped != null) //Отображение подготовленного элемента
        {
            posX = Screen.width - (width + buffer);
            Texture2D image = Resources.Load("Icons/" + equipped) as Texture2D;
            GUI.Box(new Rect(posX, posY, width, height),
                new GUIContent("Equipped", image));
        }

        posX = 10;
        posY += height + buffer;

        foreach (string item in itemList) //Просмотр всех элементов в цмкле для создания кнопок.
        {
            if (GUI.Button(new Rect(posX, posY, width, height),
                "Equip " + item))
            {
                Manager.Inventory.EguipItem(item);
            }

            if(item == "health")
            {
                if(GUI.Button(new Rect(posX,posY + height + buffer, width, height),
                    "Use Health"))
                {
                    Manager.Inventory.ConsumeItem("health");
                    Manager.Player.ChangeHealth(25);
                }
            }

            posX += width + buffer;
        }


    }

}

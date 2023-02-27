using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; } //Свойство читается откуда угоджно,
                                                      //но задается только в этом сценарии.
    private Dictionary<string, int> _items;

    public string equippedItem { get; private set; }
    
    public void Startup(NetworkService service)
    {
        Debug.Log("Invertory manager starting...");//Сюда идут все задачи запуска с долгим
                                                   //временем выполнения.
        _items = new Dictionary<string, int>();

        status = ManagerStatus.Started;
    }

    private void DisplayItems()//Вывод на консоль сообщения о текущем инвентаре.
    {
        string itemDisplay = "Items: ";

        foreach (KeyValuePair<string, int> item in _items)
        {
            itemDisplay += item.Key + "(" + item.Value + ")";
        }

        Debug.Log(itemDisplay);

    }

    public void AddItem(string name)//Другие сценарии не могут напрямую управлять списком
                                    //элементов, но могут вызвать этот метод.
    {
        if (_items.ContainsKey(name))//Проверка существующих записей перед вводом новых данных.
        {
            _items[name] += 1;
        }
        else
        {
            _items[name] = 1;
        }

        DisplayItems();
    }

    public List<string> GetItemList() //Возвращаем список всех ключей словаря.
    {
        List<string> list = new List<string>(_items.Keys); 
        return list;
    }

    public int GetItemCount(string name) //Возвращаем колличество указанных элементов в инвентаре.
    {
        if (_items.ContainsKey(name))
        {
            return _items[name];
        }
        return 0;
    }

    /// <summary>
    /// Проверяем наличие в инвентаре
    /// указанного элемента и тот факт, что он
    /// еще не подготовлен к использованию.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool EguipItem(string name) 
    {
        if(_items.ContainsKey(name) && equippedItem != name)
        {
            equippedItem = name;
            Debug.Log("Equipped " + name);
            return true;
        }

        equippedItem = null;
        Debug.Log("Unequipped");
        return false;
    }

    public bool ConsumeItem(string name)
    {
        if (_items.ContainsKey(name)) //Проверка наличия элемента среди инвентаря.
        {
            _items[name]--;
            if (_items[name] == 0) //Удаление записи, если колво становится равным нулю.
            {
                _items.Remove(name);
            }
            else //Реакция в случае отсутствия в инвентаре нужного элемента.
            {
                Debug.Log("cannot consume " + name);
                return false;
            }

        }

        DisplayItems();
        return true;
    }
}

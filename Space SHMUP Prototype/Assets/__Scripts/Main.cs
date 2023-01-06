using System.Collections; //Необходим для доступа к массивам и др. коллекциям
using System.Collections.Generic; //Необходим для доступа к спискам и словорям
using UnityEngine; //Необходим для доступа к Unity
using UnityEngine.SceneManagement; //Для загрузки и перезагрузки сцен

public class Main : MonoBehaviour
{
    static public Main S; //Обьект-одиночка Main
    static Dictionary<WeaponType,WeaponDefinition> WEAP_DICT;

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies; //Массив шаблонов Enemy
    public float enemySpawnPerSecond = 0.5f; //Вражеских кораблей в секунуду
    public float enemyDefaultPadding = 1.5f;//Отступ для позиционирования
    public WeaponDefinition[] weaponDefinitions;

    private BoundsCheck bndCheck;

    private void Awake()
    {
        S = this;
        //Запись в bndCheck ссылку на компонент BoundsCheck этого игрового лбьекта
        bndCheck = GetComponent<BoundsCheck>();
        //вызывать SpawnEnemy() один раз (в 2 секунды при значениях по умолчанию)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);

        //Словарь с ключами типа WeaponType
        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach(WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }
    }

    public void SpawnEnemy()
    {
        //Выбрать случайны шаблон Enemy для создания
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        //Разместить вражеский кораблю над экраном в случайной позиции x
        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>()!= null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        //Установить начальные координаты созданного вражеского корабля
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        //Снова вызвать SpawnEnemy()
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void DelayedRestart(float delay)
    {
        //Вызвать метод Restart() через delay секунд
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        //Перезагрузить _Scene_0, чтобы перезагрузить игру
        SceneManager.LoadScene("_Scene_0");
    }


    /// <summary>
    /// Статическая функция, возвращающая WeaponDefinition из статического
    /// защищенного поля WEAP_DICT класса Main
    /// </summary>
    /// <returns>Экземпляр WeaponDefinition или, если нет такого определения
    /// для указанного WeaponType, возвращает новый экземпляр WeaponDefinition
    /// с типом none
    /// </returns>
    /// <param name="wt">Тип оружия WeaponType, для которого требуется получить
    /// WeaponDefinition</param>
    static public WeaponDefinition GetWeaponDefinition(WeaponType wt)
    {
        //Проверить наличие указанного ключа в словаре
        //Попытка извлечь значение по отсутсвующему ключу вызовет ошибку,
        //поэтому слудующая инструкция играет важную роль.
        if (WEAP_DICT.ContainsKey(wt))
        {
            return(WEAP_DICT[wt]);
        }
        //Следующая инструкция возвращает новый экземплятWeaponDefinition
        //с типом оружия WeaponType.none, что означает неудачную попытку
        //найти требуемое определение WeaponDefinition
        return(new WeaponDefinition());
    }
    

}

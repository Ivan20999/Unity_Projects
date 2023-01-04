using System.Collections; //Необходим для доступа к массивам и др. коллекциям
using System.Collections.Generic; //Необходим для доступа к спискам и словорям
using UnityEngine; //Необходим для доступа к Unity
using UnityEngine.SceneManagement; //Для загрузки и перезагрузки сцен

public class Main : MonoBehaviour
{
    static public Main S; //Обьект-одиночка Main

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies; //Массив шаблонов Enemy
    public float enemySpawnPerSecond = 0.5f; //Вражеских кораблей в секунуду
    public float enemyDefaultPadding = 1.5f;//Отступ для позиционирования

    private BoundsCheck bndCheck;

    private void Awake()
    {
        S = this;
        //Запись в bndCheck ссылку на компонент BoundsCheck этого игрового лбьекта
        bndCheck = GetComponent<BoundsCheck>();
        //вызывать SpawnEnemy() один раз (в 2 секунды при значениях по умолчанию)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
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


}

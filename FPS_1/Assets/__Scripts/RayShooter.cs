using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //Подключение библиотеки для UI-системы. 

public class RayShooter : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        //Доступ к другим компонентам, присоединенным к этому же обьекту
        _camera = GetComponent<Camera>();
        //Скрываем указатель мыши вцентре экрана.
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

    }

    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        //Команда GUI Label() отображает на экране символ.
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void Update()
    {
        //Реакция на нажатие кнопки мыши + проверяем, что GUI не используется
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //Середина экрана - это половина его ширины и высоты
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            //создание в этой точке луча методом ScreenPointToRay().
            Ray ray = _camera.ScreenPointToRay(point);
            //Испущенный луч заполняется информацией
            RaycastHit hit;
            //Переменная, на которую ссфлается луч
            if (Physics.Raycast(ray, out hit))
            {
                //Получаем обьект в который попал луч.
                GameObject hitObject = hit.transform.parent.gameObject;

                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                //Проверяем наличие у этого обьекта компонента ReactiveTarget.
                if (target != null)
                {
                    //Debug.Log("Target hit");
                    //Вызов метода для мишени вместо генерации отладочного сообщения.
                    target.ReactToHit();
                    Messenger.Broadcast(GameEvent.ENEMY_HIT); //К реакции на попадание добавляется рассылка сообщения.
                }
                else
                {
                    //Запуск сопрограммы в ответ на попадание.
                    StartCoroutine(SphereIndicator(hit.point));
                }
                //Загружаем координаты точки, в которую попал луч
                //Debug.Log("Hit " + hit.point);
            }

        }

    }

    //Сопрограммы пользуются функциями IEnummerator
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        //Ключевое слово yieald указывает сопрограмме, когда следует остановиться.
        yield return new WaitForSeconds(1);

        //Удаляем этот GameObject и очищаем память
        Destroy(sphere);
    }
}

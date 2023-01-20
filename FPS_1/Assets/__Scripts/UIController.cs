using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; //Импорт инфроструктуры для работы с кодом UI.

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreLabel; //Обьект сцены Reference Text, предназначенный
                                                           //для задания свойства text.
    [SerializeField] private SettingsPopup _settingsPopup;
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private TMP_InputField _name;

    private int _score;

    void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit); //Обьявляем, какой метод отвечает на событие ENEMY_HIT
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit); //При разрушении обьекта, удаляйте подписчика, чтобы избежать оишбок
    }

    private void Start()
    {
        _score = 0;
        _scoreLabel.text = _score.ToString();//Присвоение переменной score начального значения 0.

        _settingsPopup.Close(); //Закрываем всплывающее окно в момент начала игры

        _speedSlider.value = PlayerPrefs.GetFloat("speed", 1);
        _name.text = PlayerPrefs.GetString("name", "user");
    }

    private void OnEnemyHit()
    {
        _score += 1;//Увеличение переменной score на 1 в ответ на данное событие.
        _scoreLabel.text = _score.ToString();
    }

    //private void Update()
    //{
    //    //_scroleLabel.text = Time.realtimeSinceStartup.ToString();
    //}

    public void OnOpenSettings() //Метод, вызывающий кнопки настроек
    {
        //Debug.Log("open settings");
        _settingsPopup.Open(); 
    }

    //public void OnPointerDown()
    //{
    //    Debug.Log("pointer down");
    //}
}

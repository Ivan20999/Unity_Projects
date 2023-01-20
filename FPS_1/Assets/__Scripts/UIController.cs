using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; //������ �������������� ��� ������ � ����� UI.

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreLabel; //������ ����� Reference Text, ���������������
                                                           //��� ������� �������� text.
    [SerializeField] private SettingsPopup _settingsPopup;
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private TMP_InputField _name;

    private int _score;

    void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit); //���������, ����� ����� �������� �� ������� ENEMY_HIT
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit); //��� ���������� �������, �������� ����������, ����� �������� ������
    }

    private void Start()
    {
        _score = 0;
        _scoreLabel.text = _score.ToString();//���������� ���������� score ���������� �������� 0.

        _settingsPopup.Close(); //��������� ����������� ���� � ������ ������ ����

        _speedSlider.value = PlayerPrefs.GetFloat("speed", 1);
        _name.text = PlayerPrefs.GetString("name", "user");
    }

    private void OnEnemyHit()
    {
        _score += 1;//���������� ���������� score �� 1 � ����� �� ������ �������.
        _scoreLabel.text = _score.ToString();
    }

    //private void Update()
    //{
    //    //_scroleLabel.text = Time.realtimeSinceStartup.ToString();
    //}

    public void OnOpenSettings() //�����, ���������� ������ ��������
    {
        //Debug.Log("open settings");
        _settingsPopup.Open(); 
    }

    //public void OnPointerDown()
    //{
    //    Debug.Log("pointer down");
    //}
}

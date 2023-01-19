using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; //������ �������������� ��� ������ � ����� UI.

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scroleLabel; //������ ����� Reference Text, ���������������
                                                //��� ������� �������� text.
    private void Update()
    {
        _scroleLabel.text = Time.realtimeSinceStartup.ToString();
    }

    public void OnOpenSettings() //�����, ���������� ������ ��������
    {
        Debug.Log("open settings");
    }

    public void OnPointerDown()
    {
        Debug.Log("pointer down");
    }
}

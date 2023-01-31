using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    /// <summary>
    /// ��������� ����� �� ������ �� ������� �����,
    /// �� � �� �������� �� ������� Project.
    /// </summary>
    [SerializeField] private Material _sky;
    [SerializeField] private Light _sun;

    private float _fullIntensity;
    private float _cloudValue = 0f;

    private void Start()
    {
        _fullIntensity = _sun.intensity;//�������� ������������� ����� �������� ������.
    }

    private void Update()
    {
        SetOvercast(_cloudValue);
        _cloudValue += .005f; //��� ������������� �������� ���������� ����. ������� �����.
    }

    private void SetOvercast(float value)
    {
        _sky.SetFloat("_Blend", value);
        _sun.intensity = _fullIntensity - (_fullIntensity * value);
    }
}

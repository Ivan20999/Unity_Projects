using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    /// <summary>
    /// Ссылаться можно не только на обьекты сцены,
    /// но и на метериал на вкладке Project.
    /// </summary>
    [SerializeField] private Material _sky;
    [SerializeField] private Light _sun;

    private float _fullIntensity;
    private float _cloudValue = 0f;

    private void Start()
    {
        _fullIntensity = _sun.intensity;//Исходная интенсивность света счтается полной.
    }

    private void Update()
    {
        SetOvercast(_cloudValue);
        _cloudValue += .005f; //Для непрерывности перехода увеличваем знач. вкаждом кадре.
    }

    private void SetOvercast(float value)
    {
        _sky.SetFloat("_Blend", value);
        _sun.intensity = _fullIntensity - (_fullIntensity * value);
    }
}

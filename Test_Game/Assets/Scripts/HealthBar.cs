using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private GameObject _text;
    private bool isTrue = false;
    private float _timer = 0;

    private Camera _camera;

    private void Awake()
    {

        OnHealthChange(1, 0);
        _camera = Camera.main;
        _text.GetComponent<Text>();
        _text.SetActive(false);
    }

    public void OnHealthChange(float bar, float damage)
    {
        _text.SetActive(true);
        _healthBarFilling.fillAmount = bar;
        _text.GetComponent<TextMeshProUGUI>().text = damage.ToString();
        isTrue = true;
    }

    private void LateUpdate()
    {

        if (isTrue)
        {
            _timer += Time.deltaTime;

            if(_timer > 2)
            {
                _text.SetActive(false);
                _timer = 0; 
            }
        }

        transform.LookAt(new Vector3(_camera.transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        transform.Rotate(0, 180, 0);
    }
}

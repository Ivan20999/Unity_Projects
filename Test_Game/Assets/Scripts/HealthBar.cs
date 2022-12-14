using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private GameObject _text;

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
        Invoke("SetOff", 2);
    }

    void SetOff()
    {
        _text.SetActive(false);
    }

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        transform.Rotate(0, 180, 0);
    }
}

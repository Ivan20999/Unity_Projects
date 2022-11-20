using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] GameObject _backGround1;
    [SerializeField] GameObject _backGround2;
    public float _speed = -2f;
    public float _lowerYValue = -20f;
    public float _upperYValue = 40;

    void Update()
    {
        _backGround1.transform.Translate(0f, _speed * Time.deltaTime, 0f);
        if (_backGround1.transform.position.y <= _lowerYValue)
        {
            _backGround1.transform.Translate(0f, _upperYValue, 0f);
        }

        _backGround2.transform.Translate(0f, _speed * Time.deltaTime, 0f);
        if (_backGround2.transform.position.y <= _lowerYValue)
        {
            _backGround2.transform.Translate(0f, _upperYValue, 0f);
        }
    }
}

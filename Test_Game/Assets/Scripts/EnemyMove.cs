using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float _moveSpeed;
    public float _rotationSpeed;
    private bool _isMove = true;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 TargetVector = _player.transform.position - gameObject.transform.position;

        if (TargetVector.magnitude < 1.5)
        {
            _isMove = false;
        }

        if (_player != null && _isMove)
        {
            var _lookDir = _player.transform.position - gameObject.transform.position;
            _lookDir.y = 0;
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(_lookDir), _rotationSpeed * Time.deltaTime);
            gameObject.transform.position += gameObject.transform.forward * _moveSpeed * Time.deltaTime;
        }

    }

}

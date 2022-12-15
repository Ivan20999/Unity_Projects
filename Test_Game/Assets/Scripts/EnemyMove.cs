using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float _moveSpeed;
    public float _rotationSpeed;
    private bool _isMove = true;

    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Player != null )
        {
            var _lookDir = Player.transform.position - gameObject.transform.position;
            _lookDir.y = 0;

            if (_isMove)
            {
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(_lookDir), _rotationSpeed * Time.deltaTime);
                gameObject.transform.position += gameObject.transform.forward * _moveSpeed * Time.deltaTime;
            }
        }
        Debug.Log(_isMove);
    }

    private void OnTriggerEnter(Collider o)
    {
        if (o.tag == "Player")
        {
            _isMove = false;
        }
        
    }
}

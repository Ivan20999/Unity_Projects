using UnityEngine;
using System.Collections;


public class WanderingAI : MonoBehaviour
{
    public const float baseSpeed = 3.0f; //������� ��������, ������� � ����������� � ���������� ��������.

    //�������� ��� �������� �������� � ����������, � ��������
    //����������� ������� �� �����������.
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    private bool _alive;

    [SerializeField] private GameObject _fireballPrefab;
    private GameObject _fireball;

    void Awake()
    {
        //Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void OnDestroy()
    {
       // Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    //private void OnSpeedChanged(float value) //�����, ����������� � ���������� ��� ������� SPEED_CHANGED.
    //{
    //    speed = baseSpeed * value;
    //}

    private void Start()
    {
        _alive = true;
    }
    #region
    private void Update()
    {
        //�������� ���������� ������ � ������ ������ ���������.
        if (_alive)
        {
            //����������� �������� � ������ �����, �������� �� ��������.
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        //��� ��������� � ��� �� ��������� � ������������ � ��� ��
        // �����������, ��� � ��������.
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //������� ��� � ��������� ������ ���� �����������
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            //����� ������������ ��� �� ��������, ��� � ������ � �������� 
            //RayShooter.
            PlayerCharacter target = hitObject.GetComponent<PlayerCharacter>();
            if (target)
            {
                if (_fireball == null)
                {
                    _fireball = Instantiate(_fireballPrefab) as GameObject;
                    //�������� �������� ��� ����� ������ � ������� � �����������
                    //��� ��������.
                    _fireball.transform.position =
                        transform.TransformPoint(Vector3.forward * 1.5f);
                    _fireball.transform.rotation = transform.rotation;
                }
            }
            if (hit.distance < obstacleRange)
            {
                //������� � ���������� ��������� ������� �����������.
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    //�������� �����, ���������� �������� ���� �������������� �� 
    //"�����" ���������.
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
    #endregion


}

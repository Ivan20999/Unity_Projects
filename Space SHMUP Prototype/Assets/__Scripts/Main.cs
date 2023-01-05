using System.Collections; //��������� ��� ������� � �������� � ��. ����������
using System.Collections.Generic; //��������� ��� ������� � ������� � ��������
using UnityEngine; //��������� ��� ������� � Unity
using UnityEngine.SceneManagement; //��� �������� � ������������ ����

public class Main : MonoBehaviour
{
    static public Main S; //������-�������� Main
    static Dictionary<WeaponType,WeaponDefinition> WEAP_DICT;

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies; //������ �������� Enemy
    public float enemySpawnPerSecond = 0.5f; //��������� �������� � ��������
    public float enemyDefaultPadding = 1.5f;//������ ��� ����������������
    public WeaponDefinition[] weaponDefinitions;

    private BoundsCheck bndCheck;

    private void Awake()
    {
        S = this;
        //������ � bndCheck ������ �� ��������� BoundsCheck ����� �������� �������
        bndCheck = GetComponent<BoundsCheck>();
        //�������� SpawnEnemy() ���� ��� (� 2 ������� ��� ��������� �� ���������)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);

        //������� � ������� ���� WeaponType
        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach(WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }
    }

    public void SpawnEnemy()
    {
        //������� �������� ������ Enemy ��� ��������
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        //���������� ��������� ������� ��� ������� � ��������� ������� x
        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>()!= null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        //���������� ��������� ���������� ���������� ���������� �������
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        //����� ������� SpawnEnemy()
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void DelayedRestart(float delay)
    {
        //������� ����� Restart() ����� delay ������
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        //������������� _Scene_0, ����� ������������� ����
        SceneManager.LoadScene("_Scene_0");
    }

    
    

}
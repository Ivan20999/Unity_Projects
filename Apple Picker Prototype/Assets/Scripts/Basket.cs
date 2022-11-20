using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{

    [Header("Set Dynamically")]
    public TextMeshProUGUI scoreGT;

    private void Start()
    {
        //�������� ������ �� ������� ������ ScoreCounter
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        //�������� ��������� Text ����� �������� �������
        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
        //���������� �������� ����� ����� ������ 0
        scoreGT.text = "0";
    }

    private void Update()
    {
        //�������� �������� ���������� ��������� ���� �� ������ �� Input
        Vector3 mousePos2D = Input.mousePosition;

        //���������� Z ������ ����������, ��� ������ � ���������� ������������
        //��������� ��������� ����
        mousePos2D.z = -Camera.main.transform.position.z;

        //������������� ����� �� ��������� ��������� ������ � ����������
        //���������� ����
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //����������� ������� ����� ��� � � ���������� � ��������� ����
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;

    }


    void OnCollisionEnter(Collision coll)
    {
        //�������� ������, �������� � ��� �������
        GameObject collideWith = coll.gameObject;
        if (collideWith.tag == "Apple")
        {
            Destroy(collideWith);

            //������������� ����� � scoreGT � ����� �����
            int score = int.Parse(scoreGT.text);
            //�������� ���� �� ��������� ������
            score += 100;
            //������������� ����� ����� ������� � ������ � ������� �� �� �����
            scoreGT.text = score.ToString();

            //��������� ������ ����������
            if (score > HighScore.score)
            {
                HighScore.score = score;
            }
        }

    }
}

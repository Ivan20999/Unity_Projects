using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{

    static public int score = 1000;

    private void Awake()
    {
        //���� �������� HighScore ��� ���������� � PlayerPrefs, ��������� ���
        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore");
        }
        //��������� ������ ���������� HighScore � ���������
        PlayerPrefs.SetInt("HighScro", score);
    }

    private void Update()
    {
        TextMeshProUGUI gt = this.GetComponent<TextMeshProUGUI>();
        gt.text = "High Score: " + score;
        //�������� HighScore � PlayerPrefs, ���� ����������
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore",score);
        }
    }
}

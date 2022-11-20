using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{

    static public int score = 1000;

    private void Awake()
    {
        //Если значение HighScore уже существует в PlayerPrefs, прочитать его
        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore");
        }
        //Сохранить высшее достижение HighScore в хранилище
        PlayerPrefs.SetInt("HighScro", score);
    }

    private void Update()
    {
        TextMeshProUGUI gt = this.GetComponent<TextMeshProUGUI>();
        gt.text = "High Score: " + score;
        //ОБновить HighScore в PlayerPrefs, если необходимо
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore",score);
        }
    }
}

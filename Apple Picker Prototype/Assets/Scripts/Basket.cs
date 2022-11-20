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
        //Получить ссылку на игровой обьект ScoreCounter
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        //Получить компонент Text этого игрового обьекта
        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
        //Установить начально число очков равным 0
        scoreGT.text = "0";
    }

    private void Update()
    {
        //Получить текущуие координаты указателя мыши на экране из Input
        Vector3 mousePos2D = Input.mousePosition;

        //Координаты Z камеры определяют, как далеко в трехмерном пространстве
        //находится указатель мыши
        mousePos2D.z = -Camera.main.transform.position.z;

        //Преобразовать точку на двумерной плоскости экрана в трехмерные
        //координаты игры
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //Переместить корзину вдоль оси Х в координаты Х указателя мыши
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;

    }


    void OnCollisionEnter(Collision coll)
    {
        //Отыскать яблоко, попавшее в эту корзину
        GameObject collideWith = coll.gameObject;
        if (collideWith.tag == "Apple")
        {
            Destroy(collideWith);

            //Преобразовать текст в scoreGT в целое число
            int score = int.Parse(scoreGT.text);
            //Добавить очки за пойманное яблоко
            score += 100;
            //Преобразовать число очков обратно в строку и вывести ее на экран
            scoreGT.text = score.ToString();

            //Запомнить высшее достижение
            if (score > HighScore.score)
            {
                HighScore.score = score;
            }
        }

    }
}

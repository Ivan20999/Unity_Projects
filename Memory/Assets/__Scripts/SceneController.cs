using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    [SerializeField] private MemoryCard _originalCard; //Ссылка на карты в сцене.
    [SerializeField] private Sprite[] _images; //Массив для ссылок на ресурсы-спрайты.
    [SerializeField] private TextMesh _scoreLabel;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;
    private int _score = 0;

    private void Start()
    {
        Vector3 startPos = _originalCard.transform.position; //Положение первой карты, положение остальных карты
                                                             //отсчитывается от этой точки.

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 }; //Обьявляет целочисленный массив с парами индентификаторов
                                                    //для всех четырех спрайтов с изображением карт.
        numbers = ShuffleArray(numbers); //Вызов функции, перемешивающей элементы массива.

        // Вложенные циклы, задающие как столбцы, так и строки нашей сетки.
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card; //Ссылка на контейнер для исходной карты или ее копий.

                if (i == 0 && j == 0)
                {
                    card = _originalCard;
                }
                else
                {
                    card = Instantiate(_originalCard) as MemoryCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index]; //Получаем идентификаторы из перемешанного списка, а не из случайных чисел
                //int id = Random.Range(0, _images.Length);
                card.SetCard(id, _images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;

                //В двумерной графике нам нужно только смещение по осям X и Y, значение Z не меняется.
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }

    }

    private int[] ShuffleArray(int[] numbers) //Алгоритм тасования Кнута (алгоритм Фишера-Йетса).
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i =0; i <newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public bool canReveal
    {
        get
        {
            return _secondRevealed == null; //Функция чтения, которая возвращает значение false,
                                            //если вторая карта уже открыта.
        }
    }

    public void CardRevealed(MemoryCard card)
    {
        //Сохранения карт в одной из двух переменных в зависимости
        //от того, какая из них свободна.
        if(_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            //Debug.Log("Match& " + (_firstRevealed.id == _secondRevealed.id));
            StartCoroutine(CheckMatch()); //Вызывает сопрограмму после открытия двух карт
        }
    }

    private IEnumerator CheckMatch()
    {
        print(_firstRevealed.id);
        print(_secondRevealed.id);

        if(_firstRevealed.id == _secondRevealed.id)
        {
            _score++;
            _scoreLabel.text = "Score: " + _score;
            //Debug.Log("Score: " + _score);
        }
        else
        {
            yield return new WaitForSeconds(.5f);

            //Закрытие несовподающих карт.
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        //Очистка переменных вне зависимости от того, было ли свопадение.
        _firstRevealed = null;
        _secondRevealed = null;
    }
}

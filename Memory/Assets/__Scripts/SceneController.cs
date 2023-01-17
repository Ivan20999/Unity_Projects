using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    [SerializeField] private MemoryCard _originalCard; //������ �� ����� � �����.
    [SerializeField] private Sprite[] _images; //������ ��� ������ �� �������-�������.
    [SerializeField] private TextMesh _scoreLabel;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;
    private int _score = 0;

    private void Start()
    {
        Vector3 startPos = _originalCard.transform.position; //��������� ������ �����, ��������� ��������� �����
                                                             //������������� �� ���� �����.

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 }; //��������� ������������� ������ � ������ ����������������
                                                    //��� ���� ������� �������� � ������������ ����.
        numbers = ShuffleArray(numbers); //����� �������, �������������� �������� �������.

        // ��������� �����, �������� ��� �������, ��� � ������ ����� �����.
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card; //������ �� ��������� ��� �������� ����� ��� �� �����.

                if (i == 0 && j == 0)
                {
                    card = _originalCard;
                }
                else
                {
                    card = Instantiate(_originalCard) as MemoryCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index]; //�������� �������������� �� ������������� ������, � �� �� ��������� �����
                //int id = Random.Range(0, _images.Length);
                card.SetCard(id, _images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;

                //� ��������� ������� ��� ����� ������ �������� �� ���� X � Y, �������� Z �� ��������.
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }

    }

    private int[] ShuffleArray(int[] numbers) //�������� ��������� ����� (�������� ������-�����).
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
            return _secondRevealed == null; //������� ������, ������� ���������� �������� false,
                                            //���� ������ ����� ��� �������.
        }
    }

    public void CardRevealed(MemoryCard card)
    {
        //���������� ���� � ����� �� ���� ���������� � �����������
        //�� ����, ����� �� ��� ��������.
        if(_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            //Debug.Log("Match& " + (_firstRevealed.id == _secondRevealed.id));
            StartCoroutine(CheckMatch()); //�������� ����������� ����� �������� ���� ����
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

            //�������� ������������� ����.
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        //������� ���������� ��� ����������� �� ����, ���� �� ����������.
        _firstRevealed = null;
        _secondRevealed = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreTextCount;
    [SerializeField] TMP_Text _gameOverText;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] GameObject _startButton;
    [SerializeField] GameObject _settingsButton;
    [SerializeField] GameObject _exitButton;
    [SerializeField] GameObject _menuButton;
    [SerializeField] GameObject _joystick;
    [SerializeField] GameObject _fireButton;
    [SerializeField] GameObject _spaceShip;
    [SerializeField] GameObject _meteorSpawner;

    int _playerScore = 0;
    bool _startGame;

    public void AddScore()
    {
        _playerScore++;
        _scoreTextCount.text = _playerScore.ToString();
    }

    public void PlayerDied()
    {
        _spaceShip.SetActive(false);
        _joystick.SetActive(false);
        _fireButton.SetActive(false);
        _startGame = false;
        _gameOverText.enabled = true;
    }

    public void StartGame()
    {
        _startButton.SetActive(false);
        _settingsButton.SetActive(false);
        _exitButton.SetActive(false);
        _joystick.SetActive(true);
        _fireButton.SetActive(true);
        _scoreTextCount.enabled = true;
        _scoreText.enabled = true;
        _spaceShip.SetActive(true);
        _menuButton.SetActive(true);
        _spaceShip.transform.position = new Vector3(0, -5, 0);
        _startGame = true;
        _gameOverText.enabled = false;
    }

    public void MenuViev()
    {
        _menuButton.SetActive(false);
        _startButton.SetActive(true);
        _settingsButton.SetActive(true);
        _exitButton.SetActive(true);
        _joystick.SetActive(false);
        _fireButton.SetActive(false);
        _scoreTextCount.enabled = false;
        _scoreText.enabled = false;
        _startGame = false;
        _gameOverText.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();    // закрыть приложение
    }

    private void Update()
    {
        if (_startGame)
        {
            FindObjectOfType<MeteorSpawner>().TimeRespawn();
        }
    }
}


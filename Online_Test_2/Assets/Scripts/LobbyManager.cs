using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks //В классе есть методы для определения, когда клиент подключился к мастеру
{
    public TextMeshProUGUI LogText;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NickName = "Player " + Random.Range(1000, 9999);
        Log("Player's name is set to " + PhotonNetwork.NickName);   

        PhotonNetwork.AutomaticallySyncScene = true; //Автоматическое переключение сцен на всех клиентах
        PhotonNetwork.GameVersion = "1"; //Контроль версии игры
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() //Вызывакт мастер, при установки связи с сервером
    {
        Log("Connected to  Master");
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2});
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Log("Joined the room");

        PhotonNetwork.LoadLevel("Game");
    }

    private void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;

    }
}

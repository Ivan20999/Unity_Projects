using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Leave()//ѕринудительный выход из комнаты
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        // огда текущий игрок (мы) покидает комнату, случайно
        SceneManager.LoadScene(0);

    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //¬ыводит сообщение, что игрок зашел в комнату
    {
        Debug.LogFormat("Player (0) entered room", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) //»грок покинул комнату
    {
        Debug.LogFormat("Player (0) left room", otherPlayer.NickName);
    }
}

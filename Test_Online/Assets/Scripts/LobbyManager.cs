using Photon.Pun;
using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks //� ������ ���� ������ ��� �����������, ����� ������ ����������� � �������
{
    public TextMeshProUGUI LogText;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NickName = "Player " + Random.Range(1000, 9999);
        Log("Player's name is set to " + PhotonNetwork.NickName);

        PhotonNetwork.AutomaticallySyncScene = true; //�������������� ������������ ���� �� ���� ��������
        PhotonNetwork.GameVersion = "1"; //�������� ������ ����
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() //�������� ������, ��� ��������� ����� � ��������
    {
        Log("Connected to  Master");
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
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

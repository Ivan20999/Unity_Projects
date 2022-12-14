using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab;

    private void Start()
    {
        Vector3 pos = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        PhotonNetwork.Instantiate(PlayerPrefab.name, pos, Quaternion.identity);
    }


    public void Leave()//�������������� ����� �� �������
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        //����� ������� ����� (��) �������� �������, ��������
        SceneManager.LoadScene(0);

    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //������� ���������, ��� ����� ����� � �������
    {
        Debug.LogFormat("Player (0) entered room", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) //����� ������� �������
    {
        Debug.LogFormat("Player (0) left room", otherPlayer.NickName);
    }
}

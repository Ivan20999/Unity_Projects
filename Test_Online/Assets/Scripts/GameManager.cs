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

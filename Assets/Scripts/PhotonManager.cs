using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private readonly string version = "1.0";

    private string userId = "BaseJ";

    private void Awake()
    {
        //������ Ŭ���̾�Ʈ�� �� �ڵ� ����ȭ �ɼ�
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.GameVersion = version;

        PhotonNetwork.NickName = userId;

        Debug.Log(PhotonNetwork.SendRate);

        //���� ���� ����
        PhotonNetwork.ConnectUsingSettings();

    }

    /// <summary>
    /// ���漭���� ���� �� ȣ��Ǵ� �ݹ� �Լ�
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master!");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"photonnetwork.inlobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Failed {returnCode} : {message}");


        RoomOptions ro = new RoomOptions();

        //�뿡 ������ �� �ִ� �ִ� �����ڼ�
        ro.MaxPlayers = 20;

        //�� ���� ����
        ro.IsOpen = true;

        //�κ񿡼� �� ��� �����ų�� ����
        ro.IsVisible = true;
        
        //�� ����
        PhotonNetwork.CreateRoom("My Room",ro);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.CurrentRoom.Name}");

        Debug.Log($"Player.Count = {PhotonNetwork.CurrentRoom.PlayerCount}");


        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName} , { player.Value.ActorNumber}");
        }
    }

}

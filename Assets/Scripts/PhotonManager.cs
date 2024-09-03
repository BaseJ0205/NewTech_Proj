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
        //마스터 클라이언트의 씬 자동 동기화 옵션
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.GameVersion = version;

        PhotonNetwork.NickName = userId;

        Debug.Log(PhotonNetwork.SendRate);

        //포톤 서버 접속
        PhotonNetwork.ConnectUsingSettings();

    }

    /// <summary>
    /// 포톤서버에 접속 후 호출되는 콜백 함수
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

        //룸에 입장할 수 있는 최대 접속자수
        ro.MaxPlayers = 20;

        //룸 오픈 여부
        ro.IsOpen = true;

        //로비에서 룸 목록 노출시킬지 여부
        ro.IsVisible = true;
        
        //룸 생성
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

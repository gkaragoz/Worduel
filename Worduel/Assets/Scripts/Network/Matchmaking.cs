using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using UnityEngine;

public class Matchmaking : MonoBehaviourPunCallbacks {

    public Action onMatchmakingSuccess;
    public Action onMatchmakingViaBot;
    public Action onPlayerLeft;

    [Header("Initialization")]
    [SerializeField]
    private int _searchTimeoutInSeconds = 8;

    [Header("Debug")]
    [SerializeField]
    private bool _isSearching = false;
    [SerializeField]
    private float _searchTimeoutLeft;

    private const string MATCHMAKING_STRING = "[MATCHMAKING]: ";

    private void Update() {
        if (_isSearching) {
            _searchTimeoutLeft -= Time.deltaTime;
        }
    }

    private IEnumerator IWaitForOpponent() {
        yield return new WaitForSeconds(_searchTimeoutInSeconds);

        _isSearching = false;
        MatchmakingViaBot();
    }

    private void MatchmakingSuccess() {
        _isSearching = false;
        StopAllCoroutines();

        Debug.Log(MATCHMAKING_STRING + "Matchmaking completed!");
        Debug.Log(MATCHMAKING_STRING + "Matched with a real user: " + PhotonNetwork.PlayerListOthers[0].NickName);

        onMatchmakingSuccess?.Invoke();
    }

    private void MatchmakingViaBot() {
        _isSearching = false;
        StopAllCoroutines();

        Debug.Log(MATCHMAKING_STRING + "Matchmaking completed!");
        Debug.Log(MATCHMAKING_STRING + "Matched with BOT!");

        onMatchmakingViaBot?.Invoke();
    }

    public void StartMatchmaking() {
        FragmentManager.instance.Open(FragmentManager.FragmentEnum.Matchmaking);

        Debug.Log("Matchmaking has been started!");

        _searchTimeoutLeft = _searchTimeoutInSeconds;

        PhotonNetwork.JoinRandomRoom();
    }

    public void CancelMatchmaking() {
        Debug.Log(MATCHMAKING_STRING + "Matchmaking canceled!");
        StopAllCoroutines();

        _isSearching = false;
    }

    public override void OnJoinedRoom() {
        Debug.Log(MATCHMAKING_STRING + "Joined to a room.");

        _isSearching = true;

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2) {
            MatchmakingSuccess();
        } else {
            Debug.Log(MATCHMAKING_STRING + "Waiting for an opponent...");
            StartCoroutine(IWaitForOpponent());
        }
    }

    public override void OnLeftRoom() {
        Debug.Log(MATCHMAKING_STRING + "Left from room.");

        CancelMatchmaking();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        Debug.Log(MATCHMAKING_STRING + "Opponent found! (" + newPlayer.UserId + ")" + newPlayer.NickName);

        MatchmakingSuccess();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        Debug.Log(MATCHMAKING_STRING + "Player left from room! (" + otherPlayer.UserId + ")" + otherPlayer.NickName);

        onPlayerLeft?.Invoke();
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log("OnCreateRoomFailed: " + message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        Debug.Log(MATCHMAKING_STRING + message);
        Debug.Log(MATCHMAKING_STRING + "Creating a new room.");
        
        RoomOptions options = new RoomOptions { MaxPlayers = 2 };

        PhotonNetwork.CreateRoom(Guid.NewGuid().ToString(), options, TypedLobby.Default);
    }

}

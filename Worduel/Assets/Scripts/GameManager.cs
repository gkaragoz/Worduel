using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks {

    #region Singleton

    public static GameManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [Header("Initialization")]
    [SerializeField]
    private GameObject _playerPrefab = null;
    [SerializeField]
    private float _delayToOpenGameplayScreen = 3f;

    private void Start() {
        Matchmaking.instance.onMatchmakingSuccess += OnMatchmakingSuccess;
        Matchmaking.instance.onMatchmakingViaBot += OnMatchMakingViaBot;
    }

    private void OnMatchmakingSuccess() {
        StartCoroutine(IOpenGameplayScreen());
    }

    private void OnMatchMakingViaBot() {
        StartCoroutine(IOpenGameplayScreen());
    }

    private IEnumerator IOpenGameplayScreen() {
        yield return new WaitForSeconds(_delayToOpenGameplayScreen);

        FragmentManager.instance.Open(FragmentManager.FragmentEnum.Gameplay);
    }

    private void OnPlayerLeft() {
        Debug.Log("OnPlayerLeft");
    }

    private void CreatePlayer() {
        PlayerData playerData = PhotonNetwork.Instantiate(_playerPrefab.name, Vector3.zero, Quaternion.identity).GetComponent<PlayerData>();
        playerData.gameObject.name = PhotonNetwork.PlayerList[PhotonNetwork.PlayerList.Length - 1].NickName;

        Debug.Log("Player is successfully created!");
    }

    public override void OnJoinedRoom() {
        CreatePlayer();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        
    }

    public override void OnPlayerLeftRoom(Player player) {
        Debug.Log("Player left from room!" + player.NickName);
    }

}

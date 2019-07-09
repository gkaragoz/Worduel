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
    private Matchmaking _matchmaking = null;
    [SerializeField]
    private float _delayToOpenGameplayScreen = 3f;

    private void Start() {
        _matchmaking.onMatchmakingSuccess += OnMatchmakingSuccess;
        _matchmaking.onMatchmakingViaBot += OnMatchMakingViaBot;
        _matchmaking.onPlayerLeft += OnPlayerLeft;
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

    private void CreateMyPlayer() {
        PhotonNetwork.Instantiate(_playerPrefab.name, Vector3.zero, Quaternion.identity).GetComponent<PlayerData>();

        Debug.Log("My player is successfully created!");
    }

    public override void OnJoinedRoom() {
        CreateMyPlayer();
    }

}

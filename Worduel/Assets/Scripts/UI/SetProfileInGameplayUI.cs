using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class SetProfileInGameplayUI : MonoBehaviourPunCallbacks {

    [SerializeField]
    private TextMeshProUGUI _txtMyUsername;
    [SerializeField]
    private TextMeshProUGUI _txtOpponentUsername;

    private void Awake() {
        FragmentManager.instance.onTransitionFinished += OnTransitionFinished;
    }

    private void OnTransitionFinished() {
        if (FragmentManager.instance.CurrentFragmentEnum == FragmentManager.FragmentEnum.Gameplay) {
            if (PhotonNetwork.IsConnectedAndReady) {
                foreach (Player player in PhotonNetwork.PlayerList) {
                    if (player.IsLocal) {
                        _txtMyUsername.text = player.NickName.Substring(0, 6);
                    } else {
                        _txtOpponentUsername.text = player.NickName.Substring(0, 6);
                    }
                }
            }
        }
    }
}

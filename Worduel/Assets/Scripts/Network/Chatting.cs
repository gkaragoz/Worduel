using Photon.Pun;
using UnityEngine;

public class Chatting : MonoBehaviourPunCallbacks {

    private void Awake() {
        InputManager.instance.onSendWord += SendWordRPC;
    }

    public void SendWordRPC(string word) {
        if (photonView.IsMine) {
            photonView.RPC("OnSendWordRPC", RpcTarget.Others, word);
        }
    }

    [PunRPC]
    public void OnSendWordRPC(string word) {
        Debug.Log(word);

        InputManager.instance.SendWord(word);
    }

}

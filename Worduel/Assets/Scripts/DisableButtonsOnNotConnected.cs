using Photon.Pun;
using UnityEngine.UI;

public class DisableButtonsOnNotConnected : MonoBehaviourPunCallbacks {

    public Button[] _buttons;

    private void Awake() {
        foreach (Button button in _buttons) {
            button.interactable = false;
        }
    }

    public override void OnConnectedToMaster() {
        foreach (Button button in _buttons) {
            button.interactable = true;
        }
    }

}

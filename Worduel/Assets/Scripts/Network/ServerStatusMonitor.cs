using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ServerStatusMonitor : MonoBehaviour {

    [SerializeField]
    private string _statusPreMessage = "Server status: ";
    [SerializeField]
    private Color _color = Color.white;

    private TextMeshProUGUI _txtStatus = null;

    private void Awake() {
        _txtStatus = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        if (NetworkManager.instance == null) {
            return;
        }

        _txtStatus.text = _statusPreMessage + "<color=#" + ColorUtility.ToHtmlStringRGBA(_color) + ">" + NetworkManager.instance.Status;
    }

}

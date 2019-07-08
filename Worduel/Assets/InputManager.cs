using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private TextMeshProUGUI _txtInput = null;
    [SerializeField]
    private int _maxCreatedInputLength = 25;

    [Header("Debug")]
    [SerializeField]
    private string _createdInputString = string.Empty;

    public string CreatedInputString {
        get {
            return _createdInputString;
        }
        set {
            _createdInputString = value;

            _txtInput.text = _createdInputString;
        }
    }

    private void Start() {
        CreatedInputString = string.Empty;
    }

    public void OnPressed(string letter) {
        if (CreatedInputString.Length >= _maxCreatedInputLength) {
            return;
        }

        CreatedInputString += letter;
    }

    public void OnDelete() {
        if (CreatedInputString.Length <= 0) {
            return;
        }

        CreatedInputString = CreatedInputString.Substring(0, CreatedInputString.Length - 1);
    }

}

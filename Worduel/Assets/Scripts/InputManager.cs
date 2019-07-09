using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour {

    #region Singleton

    public static InputManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public Action<string> onSendWord;

    [Header("Initialization")]
    [SerializeField]
    private TextMeshProUGUI _txtInput = null;
    [SerializeField]
    private Transform _chatContentTransform = null;
    [SerializeField]
    private GameObject _myWordPrefab = null;
    [SerializeField]
    private GameObject _opponentWordPrefab = null;
    [SerializeField]
    private int _maxCreatedInputLength = 25;

    [Header("Debug")]
    [SerializeField]
    private string _createdInputString = string.Empty;
    [SerializeField]
    private List<WordUI> _createdWords = new List<WordUI>();

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

    public void SendWord(string word) {
        WordUI newWordUI = Instantiate(_opponentWordPrefab, _chatContentTransform).GetComponent<WordUI>();
        newWordUI.SetWord(word);

        _createdWords.Add(newWordUI);
    }

    public void SendWord() {
        WordUI newWordUI = Instantiate(_myWordPrefab, _chatContentTransform).GetComponent<WordUI>();

        newWordUI.SetWord(CreatedInputString);

        _createdWords.Add(newWordUI);

        onSendWord?.Invoke(CreatedInputString);

        CreatedInputString = string.Empty;
    }

}

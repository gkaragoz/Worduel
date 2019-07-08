using TMPro;
using UnityEngine;

public class WordUI : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private TextMeshProUGUI _txtWord;

    [Header("Debug")]
    private string _wordString;

    public void SetWord(string word) {
        if (word == string.Empty) {
            word = ":(";
        }

        _wordString = word;

        _txtWord.text = _wordString;
    }

}

using UnityEngine;
using UnityEngine.UI;

public class OpponentAvatarUI : MonoBehaviour {

    [SerializeField]
    private Image _image = null;

    public void SetImage(Sprite sprite) {
        _image.sprite = sprite;
    }

}

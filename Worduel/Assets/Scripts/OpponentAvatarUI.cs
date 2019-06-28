using UnityEngine;
using UnityEngine.UI;

public class OpponentAvatarUI : MonoBehaviour {

    [SerializeField]
    private Image _image = null;
    
    private RectTransform _avatarRectTransform = null;

    public float CurrentPosX { get { return _avatarRectTransform.anchoredPosition.x; } }

    private void Awake() {
        _avatarRectTransform = GetComponent<RectTransform>();
    }

    public void Move(float speed, float posX) {
         _avatarRectTransform.anchoredPosition = Vector2.MoveTowards(_avatarRectTransform.anchoredPosition, new Vector2(posX, _avatarRectTransform.anchoredPosition.y), speed * Time.deltaTime);
    }

    public void ResetPosition(float posX) {
        _avatarRectTransform.anchoredPosition = new Vector2(posX, _avatarRectTransform.anchoredPosition.y);
    }

    public void SetRandomImage(Sprite sprite) {
        _image.sprite = sprite;
    }

}

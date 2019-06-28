using UnityEngine;

public class OpponentAvatarUISettings : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private OpponentAvatarUI _avatarUI_0 = null;
    [SerializeField]
    private OpponentAvatarUI _avatarUI_1 = null;
    [SerializeField]
    private Sprite[] _avatarSprites = null;

    [Header("Settings")]
    [SerializeField]
    private float _speed = 750f;
    [SerializeField]
    private float _delay = 1f;
    [SerializeField]
    private float _outPosX = -260f;
    [SerializeField]
    private float _inPosX = 260f;

    private void Update() {
        if (_avatarUI_0.CurrentPosX > _outPosX) {
            _avatarUI_0.Move(_speed, _outPosX);
        } else {
            _avatarUI_0.ResetPosition(_inPosX);
            _avatarUI_0.SetRandomImage(GetRandomAvatarSprite());
        }

        if (_avatarUI_1.CurrentPosX > _outPosX) {
            _avatarUI_1.Move(_speed, _outPosX);
        } else {
            _avatarUI_1.ResetPosition(_inPosX);
            _avatarUI_1.SetRandomImage(GetRandomAvatarSprite());
        }
    }

    public Sprite GetRandomAvatarSprite() {
        return _avatarSprites[Random.Range(0, _avatarSprites.Length)];
    }

}

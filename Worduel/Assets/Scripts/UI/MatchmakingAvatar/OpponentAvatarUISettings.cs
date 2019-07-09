using UnityEngine;

public class OpponentAvatarUISettings : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private OpponentAvatarUI _avatarUI_1 = null;
    [SerializeField]
    private OpponentAvatarUI _avatarUI_2 = null;
    [SerializeField]
    private Sprite[] _avatarSprites = null;

    private bool _isRunning = true;
    private Animator _animator = null;
    private const string HAS_STOPPED = "HasStopped";

    private void Start() {
        Matchmaking.instance.onMatchmakingSuccess += StopAnimation;
        Matchmaking.instance.onMatchmakingViaBot += StopAnimation;

        _animator = GetComponent<Animator>();
    }

    public void StartAnimation() {
        _isRunning = true;
        _animator.SetBool(HAS_STOPPED, false);
    }

    public void StopAnimation() {
        _isRunning = false;
        _animator.SetBool(HAS_STOPPED, true);
    }

    public void ChangeAvatar1() {
        if (_isRunning) {
            _avatarUI_1.SetImage(_avatarSprites[Random.Range(0, _avatarSprites.Length)]);
        }
    }

    public void ChangeAvatar2() {
        if (_isRunning) {
            _avatarUI_2.SetImage(_avatarSprites[Random.Range(0, _avatarSprites.Length)]);
        }
    }

}

using UnityEngine;

public class Fragment : MonoBehaviour {

    [SerializeField]
    private FragmentManager.FragmentEnum _fragmentEnum = FragmentManager.FragmentEnum.Home;
    [SerializeField]
    private Color _bottomColor = Color.white;

    public FragmentManager.FragmentEnum FragmentEnum { get { return _fragmentEnum; } }
    public Color BottomColor { get { return _bottomColor; } }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

}

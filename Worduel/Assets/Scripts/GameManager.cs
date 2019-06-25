using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton

    public static GameManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [SerializeField]
    private PlayerStats _myPlayerStats = null;

    public PlayerStats MyPlayerStats { get { return _myPlayerStats; } private set { _myPlayerStats = value; } }

    public void CreateMyPlayer(PlayerStats myPlayerStats) {
        this.MyPlayerStats = myPlayerStats;

        Debug.Log("My player is successfully created!");
    }

}

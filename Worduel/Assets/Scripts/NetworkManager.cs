using SocketIO;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    #region Singleton

    public static NetworkManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public string Status { get; set; }

    private SocketIOComponent _socket;

    private void Start() {
        _socket = GetComponent<SocketIOComponent>();

        _socket.On("OnServerConnected", OnServerConnected);
        _socket.On("OnServerDisconnected", OnServerDisconnected);
        _socket.On("OnAuthenticated", OnAuthenticated);
        _socket.On("OnAuthenticateFailed", OnAuthenticateFailed);
        _socket.On("error", OnError);

        ConnectToServer();
    }

    private void OnServerConnected(SocketIOEvent e) {
        Status = "Connected to " + _socket.url;
        Debug.Log("OnServerConnected, " + e.name + ":" + e.data);

        Authenticate();
    }

    private void OnServerDisconnected(SocketIOEvent e) {
        Status = "Disconnected.";
        Debug.Log("OnServerDisconnected, " + e.name + ":" + e.data);
    }

    private void OnAuthenticated(SocketIOEvent e) {
        Status = "Authenticated!";
        Debug.Log("OnAuthenticated, " + e.name + ":" + e.data);

        PlayerStats myPlayerStats = new PlayerStats() {
            Id = e.data["id"].str,
            Username = e.data["username"].str,
            FirstName = e.data["firstName"].str,
            LastName = e.data["lastName"].str,
            Gender = (Gender)int.Parse(e.data["gender"].ToString())
        };

        Debug.Log("Trying to create local player.");
        GameManager.instance.CreateMyPlayer(myPlayerStats);
    }

    private void OnAuthenticateFailed(SocketIOEvent e) {
        Status = "ERROR: Authentication failed!";
        Debug.Log("OnAuthenticateFailed, " + e.name + ":" + e.data);
    }

    private void OnError(SocketIOEvent e) {
        Status = "ERROR: Server is offline!";
        Debug.Log("OnError: " + e.name + ":" + e.data);
    }

    public void ConnectToServer() {
        if (_socket.IsConnected) {
            return;
        }

        Status = "Trying to connect to server..." + _socket.url;
        Debug.Log(Status);
        _socket.Connect();
    }

    public void Authenticate() {
        Status = "Trying to authenticate...";
        Authentication auth = new Authentication("testGoogleId");

        JSONObject jsonObject = new JSONObject(JSONObject.Type.OBJECT);
        jsonObject.AddField("googlePlayId", auth.GoogleId);

        _socket.Emit("OnAuthenticate", jsonObject);
    }

    private void OnApplicationQuit() {
        if (_socket.IsConnected) {
            _socket.Close();
        }
    }

}

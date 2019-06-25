using SocketIO;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

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
        Debug.Log("OnServerConnected, " + e.name + ":" + e.data);

        Authenticate();
    }

    private void OnServerDisconnected(SocketIOEvent e) {
        Debug.Log("OnServerDisconnected, " + e.name + ":" + e.data);
    }

    private void OnAuthenticated(SocketIOEvent e) {
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
        Debug.Log("OnAuthenticateFailed, " + e.name + ":" + e.data);
    }

    private void OnError(SocketIOEvent e) {
        Debug.Log("OnError: " + e.name + ":" + e.data);
    }

    public void ConnectToServer() {
        if (_socket.IsConnected) {
            return;
        }

        Debug.Log("Trying to connect to server..." + _socket.url);
        _socket.Connect();
    }

    public void Authenticate() {
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

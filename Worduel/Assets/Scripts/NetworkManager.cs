using SocketIO;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    private SocketIOComponent _socket;

    private void Start() {
        _socket = GetComponent<SocketIOComponent>();

        _socket.On("OnConnected", OnConnected);
        _socket.On("OnMessage", OnMessage);
        _socket.On("OnDisconnected", OnDisconnected);
        _socket.On("error", OnError);

        ConnectToServer();
    }

    private void OnMessage(SocketIOEvent e) {
        Debug.Log("OnMessage, " + e.name + ":" + e.data);
    }

    private void OnConnected(SocketIOEvent e) {
        Debug.Log("OnConnected, " + e.name + ":" + e.data);
    }

    private void OnDisconnected(SocketIOEvent e) {
        Debug.Log("OnDisconnected, " + e.name + ":" + e.data);
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

    private void OnApplicationQuit() {
        if (_socket.IsConnected) {
            _socket.Close();
        }
    }

}

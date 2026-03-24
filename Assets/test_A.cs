using System;
using UnityEngine;
using NativeWebSocket;

public class BCI_Unity_Client : MonoBehaviour
{
    WebSocket websocket;

    async void Start()
    {
        websocket = new WebSocket("ws://127.0.0.1:8765");

        websocket.OnOpen += () =>
        {
            Debug.Log("✅ Connected to Python server");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("❌ Error: " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("🔌 Connection closed");
        };

        websocket.OnMessage += (bytes) =>
        {
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("📩 From Server: " + message);
        };

        await websocket.Connect();
    }

   void Update()
{
    websocket.DispatchMessageQueue();

    if (UnityEngine.InputSystem.Keyboard.current.sKey.wasPressedThisFrame)
    {
        SendGameStart();
    }

    if (UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame)
    {
        SendGameEnd();
    }
}
    async void SendGameStart()
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText("GAME_START");
            Debug.Log("🚀 Sent GAME_START");
        }
    }

    async void SendGameEnd()
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText("GAME_END");
            Debug.Log("🛑 Sent GAME_END");
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
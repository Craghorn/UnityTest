using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using System.Text;
using System.Dynamic;
using static UnityEditor.Progress;
using Unity.VisualScripting;
using System.Threading.Tasks;

[Serializable]
public class MyDataClass
{
    public string operation;
    public float value;
    public float odometer;
    public bool status;
}
[Serializable]
public class MyDataToSendClass
{
    public string operation;
}



public class Connection : MonoBehaviour
{
    WebSocket websocket;


    // Start is called before the first frame update
    async void Start()
    {
       

        websocket = new WebSocket("ws://185.246.65.199:9090/ws");

        websocket.OnOpen += async () =>
        {
            await GetCurrentOdometer();
            // ask for odometer
            InvokeRepeating("GetRandomStatus", 0.0f, 3.0f);
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            MyDataClass myDataClassMessage = JsonUtility.FromJson<MyDataClass>(Encoding.UTF8.GetString(bytes));
            Debug.Log(myDataClassMessage.operation + " " + myDataClassMessage.value);

            switch (myDataClassMessage.operation)
            {
                case "currentOdometer":
                    SettingsHandler.speedFromServer = myDataClassMessage.odometer;
                    Debug.Log(SettingsHandler.speedFromServer);
                    break;
                case "odometer_val" :
                    SettingsHandler.speedFromServer = myDataClassMessage.value;
                    Debug.Log(SettingsHandler.speedFromServer);
                    break;
                case "randomStatus":
                    SettingsHandler.online = myDataClassMessage.status;
                    Debug.Log(SettingsHandler.online);
                    break;

            }

            Debug.Log("OnMessage!");
            Debug.Log(bytes);

            // getting the message as a string
            var message = Encoding.UTF8.GetString(bytes);
            Debug.Log("OnMessage! " + message);
        };
        // waiting for messages
        await websocket.Connect(); 
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    async void GetRandomStatus()
    {
        Debug.Log("Test2");
        await SendWebSocketMessage("getRandomStatus");
        
    }

    async Task GetCurrentOdometer()
    {
        await SendWebSocketMessage("getCurrentOdometer");
    }

    async Task SendWebSocketMessage(string operation)
    {

        var dataCO = new MyDataToSendClass
        {
            operation = operation
        };

        var jsonStringCO = JsonUtility.ToJson(dataCO);

        Debug.Log(jsonStringCO);

        if (websocket.State == WebSocketState.Open)
        {
            // Sending bytes
            //await websocket.Send(new byte[] { 10, 20, 30 });
            Debug.Log("Test1");
            // Sending plain text
            await websocket.SendText(jsonStringCO);
        }
    }


    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

}
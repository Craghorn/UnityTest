using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHandler : MonoBehaviour
{
    // Static settings
    public static bool onMusic = true;
    public static bool onSounds = true;
    public static bool online = false;
    public static float levelSound = 1f;
    public static float speedFromServer = 0.0f;
    public static float decodedAudio = 0f;

    public static string serverAddress = "192.168.1.137";
    public static string serverPort = "8554";
    public static string streamAddress = "rtsp://192.168.1.137:8554";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

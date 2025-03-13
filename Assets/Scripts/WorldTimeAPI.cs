using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using TMPro;

public class WorldTimeAPI : MonoBehaviour
{
    #region Singleton Pattern
    public static WorldTimeAPI Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    [System.Serializable]
    struct TimeData
    {
        public int year;
        public int month;
        public int day;
        public int hour;
        public int minute;
        public int seconds;
    }

    const string API_URL = "https://timeapi.io/api/Time/current/zone?timeZone=America/Bogota";

    [HideInInspector] public bool IsTimeLoaded = false;
    private DateTime _currentDateTime = DateTime.Now;

    void Start()
    {
        StartCoroutine(GetRealDateTimeFromAPI());
    }

    public DateTime GetCurrentDateTime()
    {
        return _currentDateTime.AddSeconds(Time.realtimeSinceStartup);
    }

    IEnumerator GetRealDateTimeFromAPI()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(API_URL))
        {
            webRequest.SetRequestHeader("accept", "application/json");

            Debug.Log("Getting real datetime...");
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Response: " + webRequest.downloadHandler.text);

                // Convertir JSON a TimeData
                TimeData timeData = JsonUtility.FromJson<TimeData>(webRequest.downloadHandler.text);

                // Crear un DateTime en C#
                _currentDateTime = new DateTime(timeData.year, timeData.month, timeData.day,
                                                timeData.hour, timeData.minute, timeData.seconds);

                IsTimeLoaded = true;
                Debug.Log("Success: " + _currentDateTime);
            }
        }
    }
	
    }



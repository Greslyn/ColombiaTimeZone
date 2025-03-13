  using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class APIConnect : MonoBehaviour
{
    public string CodigoEstudiante;
    private string apiUrl = "http://192.168.42.10:5005/student/servicios/";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartCoroutine(GetRequest(apiUrl + CodigoEstudiante));
    }

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.ConnectionError
              || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error en la solicitud: " + webRequest.error);
            }
            else
            {
                Debug.Log("Respuesta recibida: " + webRequest.downloadHandler.text);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}


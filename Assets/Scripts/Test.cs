using UnityEngine;

using System;
using TMPro;

public class Test : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI  datetimeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   if (WorldTimeAPI.Instance.IsTimeLoaded)
        {
            DateTime currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
            datetimeText.text = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameManagerSingelton : MonoBehaviour
{

    public static NameManagerSingelton Instance;

    public string playerName = "Default Player";

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}

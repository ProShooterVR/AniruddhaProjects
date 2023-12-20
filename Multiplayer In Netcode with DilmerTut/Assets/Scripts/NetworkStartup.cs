using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkStartup : MonoBehaviour
{
    void Start()
    {
        if (SceneTransitionHandler.Instance.InitializeAsHost)
        {
            Debug.Log("Starting As Host");
            NetworkManager.Singleton.StartHost();
        }
        else
        {
            Debug.Log("Starting As Client");
            NetworkManager.Singleton.StartClient();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class NetworkConnect : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    // Start is called before the first frame update
    public void Create()
    {
        NetworkManager.Singleton.StartHost();
         debugText.text += " Hosting The Room \n";
    }


    // Update is called once per frame
    public void Join()
    {
        NetworkManager.Singleton.StartClient();
        debugText.text += " Joining As A Client \n";

    }
}

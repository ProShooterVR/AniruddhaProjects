using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;


public class NetworkPlayer : NetworkBehaviour
{
    public Transform root;
    public Transform head;
    public Transform LeftHand;
    public Transform RightHand;
  //  public TextMeshProUGUI debugText;
    public Renderer[] meshToDisable;

    public override void OnNetworkSpawn()
    {
      
        base.OnNetworkSpawn();
        if(IsOwner)
        {
            foreach(var item in meshToDisable)
            {
                item.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsOwner)
        {
            root.position = VRRigReferences.Singleton.root.position;
            root.rotation = VRRigReferences.Singleton.root.rotation;

            head.position = VRRigReferences.Singleton.head.position;
            head.rotation = VRRigReferences.Singleton.head.rotation;

            LeftHand.position = VRRigReferences.Singleton.leftHand.position;
            LeftHand.rotation = VRRigReferences.Singleton.leftHand.rotation;

            RightHand.position = VRRigReferences.Singleton.rightHand.position;
            RightHand.rotation = VRRigReferences.Singleton.rightHand.rotation;

        }
    }
}

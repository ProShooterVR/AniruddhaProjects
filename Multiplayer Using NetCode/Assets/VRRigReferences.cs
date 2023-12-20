using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class VRRigReferences : MonoBehaviour
{
    public static VRRigReferences Singleton;
    public TextMeshProUGUI debugText;
    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private void Awake()
    {
        Singleton = this;
     
    }
}

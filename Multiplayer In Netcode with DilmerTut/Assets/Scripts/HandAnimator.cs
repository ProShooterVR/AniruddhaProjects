using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimator : MonoBehaviour
{
    public InputActionProperty pinchAnimator;
    public InputActionProperty gripAnimator;
    public Animator Hand;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float triggervalue = pinchAnimator.action.ReadValue<float>();
        Hand.SetFloat("Trigger", triggervalue);

        float gripvalue = gripAnimator.action.ReadValue<float>();
        Hand.SetFloat("Grip", gripvalue);
    }
}

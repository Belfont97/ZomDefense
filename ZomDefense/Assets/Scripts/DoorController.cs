using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator doorAnimator;

    private bool doorOpen = false;

    private void Awake()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
    }

    public void PlayDoorAnimation()
    {
        if(!doorOpen)
        {
            Debug.Log("Opening door");
            doorAnimator.Play("DoorOpen", 0, 0.0f);
            doorOpen = true;
        }
        else
        {
            Debug.Log("Closing door");
            doorAnimator.Play("DoorClose", 0, 0.0f);
            doorOpen = false;
        }
    }
}

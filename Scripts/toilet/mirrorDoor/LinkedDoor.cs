using UnityEngine;
using System.Collections;

// Mirroring door movement to mimic a mirror
public class LinkedDoorSimple : MonoBehaviour
{
    public ButtonAction mainDoor;
    public Animator mirrorDoorAnimator;

    private bool isOpen = false;

    public void OpenLinkedDoor(bool open)
    {
        if (mirrorDoorAnimator == null) return;

        if (open && !isOpen)
        {
            mirrorDoorAnimator.Play("Open");
            isOpen = true;
        }
        else if (!open && isOpen)
        {
            mirrorDoorAnimator.Play("Close");
            isOpen = false;
        }
    }
}

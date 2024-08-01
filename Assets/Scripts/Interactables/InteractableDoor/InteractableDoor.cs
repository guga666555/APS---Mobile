using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : _ObjectInteractable
{
    public DoorSide doorSide;
    public Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public override void OnLookAt() { }

    public override void OnLookAway() { }

    public override void OnInteraction()
    {
        switch (doorSide)
        {
            case DoorSide.front:
                if (!animator.GetBool("DoorOpenFront"))
                {
                    animator.SetBool("DoorClosed", false);
                    animator.SetBool("DoorOpenFront", true);
                    animator.SetBool("DoorCloseFront", false);
                }
                else
                {
                    animator.SetBool("DoorOpenFront", false);
                    animator.SetBool("DoorCloseFront", true);
                }

                break;
            case DoorSide.back:
                if (!animator.GetBool("DoorOpenBack"))
                {
                    animator.SetBool("DoorClosed", false);
                    animator.SetBool("DoorOpenBack", true);
                    animator.SetBool("DoorCloseBack", false);
                }
                else
                {
                    animator.SetBool("DoorOpenBack", false);
                    animator.SetBool("DoorCloseBack", true);
                }
                break;
        }
    }
}

public enum DoorSide
{
    front,
    back
}
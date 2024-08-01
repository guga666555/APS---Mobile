using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : _ObjectInteractable
{
    public List<GameObject> interactables;

    public override void OnLookAt() { }

    public override void OnLookAway() { }

    public override void OnInteraction()
    {
        for (int i = 0; i < interactables.Count; i++)
        {
            interactables[i].gameObject.SetActive(true);
        }
    }
}

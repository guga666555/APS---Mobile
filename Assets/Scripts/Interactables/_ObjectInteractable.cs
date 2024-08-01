using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _ObjectInteractable : MonoBehaviour  
{
    public abstract void OnLookAt();
    public abstract void OnLookAway();
    public abstract void OnInteraction();
}

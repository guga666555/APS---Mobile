using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _EventSystem : MonoBehaviour
{
    public abstract void OnEventStart();
    public abstract void OnEventActive();
    public abstract void OnEventEnd();
}

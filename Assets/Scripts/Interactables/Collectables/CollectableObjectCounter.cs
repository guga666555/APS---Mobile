using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableObjectCounter : MonoBehaviour
{
    public TextMeshProUGUI textString;
    public int collectablesAmount;

    private void Start()
    {
        collectablesAmount = CollectableObject.CollectableCount;
        textString = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        int i = Mathf.Abs(collectablesAmount - CollectableObject.CollectableCount);
        textString.text = i + "/" + collectablesAmount;
    }
}

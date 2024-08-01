using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableObject : MonoBehaviour
{
    public static event Action OnCollected;
    public static int CollectableCount;
    public static List<GameObject> gameObjects = new List<GameObject>();
    public GameObject radioactiveZone;

    private void Start()
    {
        CollectableObject.gameObjects.Add(gameObject);
    }

    private void OnEnable()
    {
        CollectableCount++;
     //   radioactiveZone.SetActive(true);
    }

    private void OnDisable()
    {
        CollectableCount--;
        //      radioactiveZone.SetActive(false);
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Euler(0, Time.time * 100f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
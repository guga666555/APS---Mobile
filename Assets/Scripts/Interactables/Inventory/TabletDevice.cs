using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabletDevice : _Inventory
{
    private AudioSource currentAudioSource = default;
    public AudioClip radBeep = default;
    public TextMeshProUGUI radStats;
    public Slider radAmount;
    [HideInInspector] public float beepTimer, beepDelay;

    private void Start()
    {
        currentAudioSource = GetComponent<AudioSource>();
        currentAudioSource.volume = 0.45f;
    }

    public void PlayDangerSound()
    {
        beepDelay -= Time.deltaTime;
        if (beepDelay <= 0)
        {
            currentAudioSource.PlayOneShot(radBeep);
            beepDelay = beepTimer;
        }
    }

    public override void ObtainInventory(PlayerController playerReference)
    {
        transform.SetParent(playerReference.tabletContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        UnequipInventory();
    }
}

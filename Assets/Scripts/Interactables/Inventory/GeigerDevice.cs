using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeigerDevice : _Inventory
{
    private AudioSource currentAudioSource = default;
    [SerializeField] private Transform pointerMeter;

    public AudioClip radLow = default;
    public AudioClip radMedium = default;
    public AudioClip radHigh = default;

    [HideInInspector] public float zTargetRotation;
    [HideInInspector] public float minMeterValue = 125f;
    [HideInInspector] public float maxMeterValue = 235f;
    private float meterSpeed = 1.1f;
    private float currentZRotation;

    private void Start()
    {
        currentAudioSource = GetComponent<AudioSource>();
        currentZRotation = pointerMeter.localRotation.eulerAngles.z;
        zTargetRotation = minMeterValue;
    }

    private void Update()
    {
        this.GeigerMeter();
    }

    public void PlayAudio(AudioClip clip)
    {
        currentAudioSource.clip = clip;
        currentAudioSource.loop = true;
        if (!currentAudioSource.isPlaying)
            currentAudioSource.Play();
    }

    public void StopAudio()
    {
        currentAudioSource.clip = null;
    }

    public void GeigerMeter()
    {
        currentZRotation = Mathf.LerpAngle(currentZRotation, zTargetRotation, Time.deltaTime * meterSpeed);
        currentZRotation = Mathf.Clamp(currentZRotation, 125, 235);
        pointerMeter.localRotation = Quaternion.Euler(0, 0, currentZRotation);
    }

    public override void ObtainInventory(PlayerController playerReference)
    {
        transform.SetParent(playerReference.geigerContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        UnequipInventory();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EventRadiation : _EventSystem
{
    public RadiationStrenght radiationStrenght;
    private PlayerInventory playerInventory;
    private GeigerDevice geigerDevice;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<PlayerController>(out var player);
        playerInventory = player.playerInventory;
        geigerDevice = player.geigerDevice;
    }

    private void OnTriggerExit(Collider other)
    {
        if (geigerDevice)
        {
            geigerDevice.StopAudio();
            geigerDevice.zTargetRotation = geigerDevice.minMeterValue;
            OnEventEnd();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerInventory) OnEventActive();
    }

    public override void OnEventStart() { }

    public override void OnEventActive()
    {
        playerInventory.UpdateRadiationStats(radiationStrenght);
    }

    public override void OnEventEnd() { }
}

public enum RadiationStrenght
{
    radiationLevel0,
    radiationLevel1,
    radiationLevel2,
    radiationLevel3,
    radiationLevel4,
    radiationLevel5,
}
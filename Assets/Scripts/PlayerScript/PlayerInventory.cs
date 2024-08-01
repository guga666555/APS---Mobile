using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField] private string radStatsLvl0;
    [SerializeField] private string radStatsLvl1;
    [SerializeField] private string radStatsLvl2;
    [SerializeField] private string radStatsLvl3;

    public float currentRadiationAmount;
    public float radiationMultiplier = 1;
    public bool hardDifficultyGeiger = false;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (playerController.TabletEquiped) this.TabletDeviceScreen();
    }

    private void TabletDeviceScreen()
    {
        if (currentRadiationAmount > 0.1 && currentRadiationAmount < 0.4)
        {
            playerController.tabletDevice.beepTimer = 2.4f;
            playerController.tabletDevice.radStats.text = radStatsLvl1;
        }
        else if (currentRadiationAmount > 0.4 && currentRadiationAmount < 0.7)
        {
            playerController.tabletDevice.beepTimer = 1.6f;
            playerController.tabletDevice.radStats.text = radStatsLvl2;
        }
        else if (currentRadiationAmount > 0.7)
        {
            playerController.tabletDevice.beepTimer = 0.8f;
            playerController.tabletDevice.radStats.text = radStatsLvl3;
        }
        else
        {
            playerController.tabletDevice.radStats.text = radStatsLvl0;
        }

        if (currentRadiationAmount > 0.1) playerController.tabletDevice.PlayDangerSound();
        playerController.tabletDevice.radAmount.value = currentRadiationAmount;
    }

    public void UpdateRadiationStats(RadiationStrenght radiationStrenght)
    {
        float radiationMultiplier;

        switch (radiationStrenght)
        {
            case RadiationStrenght.radiationLevel0:
                if (playerController.GeigerEquiped && !hardDifficultyGeiger){
                    playerController.geigerDevice.StopAudio();
                    playerController.geigerDevice.zTargetRotation = playerController.geigerDevice.minMeterValue;}
                radiationMultiplier = 0 * this.radiationMultiplier;
                break;
            case RadiationStrenght.radiationLevel1:
                if (playerController.GeigerEquiped && !hardDifficultyGeiger){
                    playerController.geigerDevice.PlayAudio(playerController.geigerDevice.radLow);
                    playerController.geigerDevice.zTargetRotation = 145f;}
                radiationMultiplier = 0.2f * this.radiationMultiplier;
                break;
            case RadiationStrenght.radiationLevel2:
                if (playerController.GeigerEquiped && !hardDifficultyGeiger){
                    playerController.geigerDevice.PlayAudio(playerController.geigerDevice.radMedium);
                    playerController.geigerDevice.zTargetRotation = 170f;}
                radiationMultiplier = 0.5f * this.radiationMultiplier;
                break;
            case RadiationStrenght.radiationLevel3:
                if (playerController.GeigerEquiped && !hardDifficultyGeiger){
                    playerController.geigerDevice.PlayAudio(playerController.geigerDevice.radHigh);
                    playerController.geigerDevice.zTargetRotation = 185f;}
                radiationMultiplier = 1f * this.radiationMultiplier;
                break;
            case RadiationStrenght.radiationLevel4:
                if (playerController.GeigerEquiped && !hardDifficultyGeiger){
                    playerController.geigerDevice.PlayAudio(playerController.geigerDevice.radHigh);
                    playerController.geigerDevice.zTargetRotation = 210f;}
                radiationMultiplier = 2f * this.radiationMultiplier;
                break;
            case RadiationStrenght.radiationLevel5:
                if (playerController.GeigerEquiped && !hardDifficultyGeiger){
                    playerController.geigerDevice.PlayAudio(playerController.geigerDevice.radHigh);
                    playerController.geigerDevice.zTargetRotation = playerController.geigerDevice.maxMeterValue;}
                radiationMultiplier = 2.5f * this.radiationMultiplier;
                break;
            default:
                if (playerController.GeigerEquiped && !hardDifficultyGeiger){
                    playerController.geigerDevice.StopAudio();
                    playerController.geigerDevice.zTargetRotation = playerController.geigerDevice.minMeterValue;}
                radiationMultiplier = 0 * this.radiationMultiplier;
                break;
        }
        if (currentRadiationAmount < 1) currentRadiationAmount += (radiationMultiplier / 100f) * Time.deltaTime;
        else currentRadiationAmount = 1;
    }
}

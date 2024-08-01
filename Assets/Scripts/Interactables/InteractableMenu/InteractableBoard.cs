using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableBoard : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject difficultyButton;
    [SerializeField] private GameObject difficultyDesc;

    [SerializeField] private List<string> easyDifficultyDesc;
    [SerializeField] private List<string> normalDifficultyDesc;
    [SerializeField] private List<string> hardDifficultyDesc;

    [SerializeField] private GameDifficulty gameDifficulty;
    [SerializeField] private Transform arenaSpawnPoint;

    [SerializeField] private PlayerController playerReference;
    [SerializeField] private GeigerDevice geigerRef;
    [SerializeField] private TabletDevice tabletRef;

    [SerializeField] private GameObject[] normalExtraCollectables;
    [SerializeField] private GameObject[] hardExtraCollectables;
    [SerializeField] private EventDialogue eventDialogueRef;
    private bool confirmStart = false;

    private void Start()
    {
        this.ChangeDifficulty();
    }

    public void ChangeDifficulty()
    {
        switch (gameDifficulty)
        {
            case GameDifficulty.hardDiff:   // FROM HARD TO EASY...
                difficultyButton.GetComponent<TextMeshProUGUI>().text = "Difficulty < Easy >";
                difficultyButton.GetComponent<TextMeshProUGUI>().color = Color.green;
                difficultyDesc.GetComponent<TextMeshProUGUI>().text = null;
                playerReference.GetComponent<PlayerInventory>().radiationMultiplier = 0.9f;
                foreach (var descItem in easyDifficultyDesc) difficultyDesc.GetComponent<TextMeshProUGUI>().text += descItem + "\n";
                gameDifficulty = GameDifficulty.easyDiff;
                break;
            case GameDifficulty.easyDiff:   // FROM EASY TO NORMAL...
                difficultyButton.GetComponent<TextMeshProUGUI>().text = "Difficulty < Normal >";
                difficultyButton.GetComponent<TextMeshProUGUI>().color = Color.blue;
                difficultyDesc.GetComponent<TextMeshProUGUI>().text = null;
                playerReference.GetComponent<PlayerInventory>().radiationMultiplier = 1.15f;
                foreach (var descItem in normalDifficultyDesc) difficultyDesc.GetComponent<TextMeshProUGUI>().text += descItem + "\n";
                gameDifficulty = GameDifficulty.normalDiff;
                break;
            case GameDifficulty.normalDiff:   // FROM NORMAL TO HARD...
                difficultyButton.GetComponent<TextMeshProUGUI>().text = "Difficulty < Hard >";
                difficultyButton.GetComponent<TextMeshProUGUI>().color = Color.red;
                difficultyDesc.GetComponent<TextMeshProUGUI>().text = null;
                playerReference.GetComponent<PlayerInventory>().radiationMultiplier = 1.3f;
                foreach (var descItem in hardDifficultyDesc) difficultyDesc.GetComponent<TextMeshProUGUI>().text += descItem + "\n";
                gameDifficulty = GameDifficulty.hardDiff;
                break;
        }
        confirmStart = false;
    }

    public void StartGame()
    {
        string check1 = null, check2 = null, check3 = null;

        if (!playerReference.GetComponent<PlayerController>().inventorySlots.Contains(geigerRef.gameObject)) check1 = "\nYou forgot your Geiger!";
        if (!playerReference.GetComponent<PlayerController>().inventorySlots.Contains(tabletRef.gameObject)) check2 = "\nYou forgot your Radtron!";
        if (gameDifficulty == GameDifficulty.hardDiff) check3 = "\nHard difficulty is ON!\nYour geiger device will not work!";

        if (confirmStart)
        {
            if (gameDifficulty != GameDifficulty.hardDiff)
            {
                foreach (GameObject gObject in hardExtraCollectables) { gObject.SetActive(false); }
                if (gameDifficulty == GameDifficulty.easyDiff) foreach (GameObject gObject in normalExtraCollectables) { gObject.SetActive(false); }
            }
            else playerReference.GetComponent<PlayerController>().playerInventory.hardDifficultyGeiger = true;
            StartCoroutine(ArenaTeleport());
        }
        startButton.GetComponent<TextMeshProUGUI>().text = "Are you sure?" + check1 + check2 + check3;
        confirmStart = true;
    }

    private IEnumerator ArenaTeleport()
    {
        StartCoroutine(playerReference.GetComponent<PlayerController>().eventCameraFade.CameraFadeEffect(255f, playerReference.GetComponent<PlayerController>().fadeGUI, 0f, 1f, 0.34f));
        playerReference.DisableGUI();

        yield return new WaitForSeconds(3.0f);
        playerReference.gameObject.SetActive(false);
        playerReference.transform.SetParent(arenaSpawnPoint);
        playerReference.transform.localPosition = Vector3.zero;
        eventDialogueRef.StopDialogue();

        yield return new WaitForSeconds(0.01f);
        playerReference.gameObject.SetActive(true);
        StartCoroutine(playerReference.GetComponent<PlayerController>().eventCameraFade.CameraFadeEffect(255f, playerReference.GetComponent<PlayerController>().fadeGUI, 1f, 0f, 0.7f));
        playerReference.EnableGUI();
        playerReference.collectableGUI.SetActive(true);
    }
}

public enum GameDifficulty
{
    easyDiff, normalDiff, hardDiff
}
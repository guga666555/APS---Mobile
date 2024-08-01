using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventGameOver : _EventSystem
{
    [SerializeField] private Camera gameOverCamera;
    [SerializeField] private PlayerController playerReference;

    [SerializeField] private TextMeshProUGUI gameOverTitle;
    [SerializeField] private TextMeshProUGUI gameOverMessage;

    [SerializeField] private string defeatTitle;
    [SerializeField] private string defeatMessage;
    [SerializeField] private AudioClip defeatSound;
    [SerializeField] private string victoryTitle;
    [SerializeField] private string victoryMessage;
    [SerializeField] private AudioClip victorySound;
    private bool gameEnded;

    private void Update()
    {
        this.GameCondition();
    }

    private void GameCondition()
    {
        if (gameEnded) return;

        if (playerReference.playerInventory.currentRadiationAmount >= 1)
            StartCoroutine(GameOverDefeat());
        if (CollectableObject.CollectableCount == 0)
            StartCoroutine(GameOverVictory());
    }

    public IEnumerator GameOverDefeat()
    {
        this.OnEventStart();
        StartCoroutine(playerReference.eventCameraFade.CameraFadeEffect(0f, playerReference.fadeGUI, 0f, 1f, 0.2f));
        gameOverTitle.text = defeatTitle;
        gameOverMessage.text = defeatMessage;
        playerReference.playerAudioSource.PlayOneShot(defeatSound);

        yield return new WaitForSeconds(9f);
        this.OnEventEnd();
    }

    public IEnumerator GameOverVictory()
    {
        this.OnEventStart();
        StartCoroutine(playerReference.eventCameraFade.CameraFadeEffect(0f, playerReference.fadeGUI, 0f, 1f, 0.29f));
        gameOverTitle.text = victoryTitle;
        gameOverMessage.text = victoryMessage;
        playerReference.playerAudioSource.PlayOneShot(victorySound);
        yield return new WaitForSeconds(6f);
        this.OnEventEnd();
    }

    public void Reload()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnEventStart()
    {
        gameEnded = true;
        playerReference.DisableGUI();
    }

    public override void OnEventActive() { }

    public override void OnEventEnd()
    {
        playerReference.gameObject.SetActive(false);
        gameOverCamera.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventStart : _EventSystem
{
    // Habilita o pós-processamento;
    public static bool enablePostProcessing = true;

    [SerializeField] private AudioClip audioClip = default;
    [SerializeField] private BoardButton startButton;
    [SerializeField] private PlayerController playerReference;
    [SerializeField] private EventDialogue eventDialogue = default;
    [SerializeField] private List<string> dialogues = new List<string>();

    private void Start()
    {
        StartCoroutine(_StartSceneEvent(5f));
    }

    private IEnumerator _StartSceneEvent(float time)
    {
        playerReference.DisableGUI();
        StartCoroutine(playerReference.eventCameraFade.CameraFadeEffect(0f,  playerReference.fadeGUI, 1f, 0f, 0.15f));
        yield return new WaitForSeconds(time);
        playerReference.EnableGUI();
        startButton._enabled = true;
        this.OnEventStart();
    }

    public override void OnEventStart() 
    {
        StartCoroutine(eventDialogue.PlayDialogue(audioClip, dialogues));
    }

    public override void OnEventActive() { }

    public override void OnEventEnd() { }
}

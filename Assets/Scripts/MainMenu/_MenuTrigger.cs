using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class _MenuTrigger : MonoBehaviour
{
    private EventCameraFade eventCameraFade;

    [SerializeField] private GameObject menu1, menu2, menu3, menu3A, menu3B, menu4;
    [SerializeField] private GameObject graphicsLowCheckmark, graphicsMedCheckmark, graphicsHighCheckmark, skipIntroCheckmark;
    [SerializeField] private int graphicsIndex;

    [SerializeField] private AudioSource menuAudioSource;
    [SerializeField] private AudioClip[] audioClipTransition;
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private RawImage fadeImage;
    [SerializeField] private string[] startDialogue;
    public bool skipIntroBool = false;

    private void Start()
    {
        eventCameraFade = GetComponent<EventCameraFade>();
        graphicsIndex = QualitySettings.GetQualityLevel();
        fadeImage.gameObject.SetActive(false);
        Application.targetFrameRate = 60;
        this._ReturnToMenu();
    }

    public void _GenericSkipIntro() 
    {
        if (!skipIntroBool)
        {
            skipIntroCheckmark.SetActive(true);
            skipIntroBool = true;
        }
        else
        {
            skipIntroCheckmark.SetActive(false);
            skipIntroBool = false;
        }
    }

    public void _GameStart()
    {
        menuAudioSource.PlayOneShot(audioClipTransition[Random.Range(0, audioClipTransition.Length)]);
        StartCoroutine(GameStartEvent());
    }

    public IEnumerator GameStartEvent()
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(eventCameraFade.CameraFadeEffect(0, fadeImage, 0f, 1f, 0.55f));
        yield return new WaitForSeconds(3.6f);

        if (!skipIntroBool)
        {
            menu1.SetActive(false);
            menu4.SetActive(true);
            StartCoroutine(eventCameraFade.CameraFadeEffect(0, fadeImage, 1f, 0f, 0.75f));

            for (int i = 0; i < startDialogue.Count(); i++)
            {
                dialogueBox.text = startDialogue[i];
                yield return new WaitForSeconds(startDialogue[i].Count() * 0.036f);
            }
            StartCoroutine(eventCameraFade.CameraFadeEffect(0, fadeImage, 0f, 1f, 0.35f));
            yield return new WaitForSeconds(5f);
        }
        SceneManager.LoadScene("Game");
    }

    public void _GraphicsLow() { graphicsIndex = 0; this._ChangeGraphics(); }
    public void _GraphicsMedium() { graphicsIndex = 1; this._ChangeGraphics(); }
    public void _GraphicsHigh() { graphicsIndex = 2; this._ChangeGraphics(); }

    public void _ChangeGraphics()
    {
        switch (graphicsIndex) {
            case 0:
                QualitySettings.SetQualityLevel(0);
                graphicsLowCheckmark.SetActive(true);
                EventStart.enablePostProcessing = false;
                break;
            case 1:
                QualitySettings.SetQualityLevel(1);
                graphicsMedCheckmark.SetActive(true);
                EventStart.enablePostProcessing = true;
                break;
            case 2:
                QualitySettings.SetQualityLevel(2);
                graphicsHighCheckmark.SetActive(true);
                EventStart.enablePostProcessing = true;
                break;
        }
    }

    public void _ReturnToMenu()
    {
        menuAudioSource.PlayOneShot(audioClipTransition[Random.Range(0, audioClipTransition.Length)]);
        menu1.SetActive(true);
        menu2.SetActive(false);
        menu3.SetActive(false);
        menu4.SetActive(false);
    }

    public void _ReturnToSettingsMenu()
    {
        menuAudioSource.PlayOneShot(audioClipTransition[Random.Range(0, audioClipTransition.Length)]);
        menu3.SetActive(true);
        menu3A.SetActive(false);
        menu3B.SetActive(false);
    }

    public void _ExtrasMenuA()
    {
        menuAudioSource.PlayOneShot(audioClipTransition[Random.Range(0, audioClipTransition.Length)]);
        menu3.SetActive(false);
        menu3A.SetActive(true);
    }

    public void _ExtrasMenuB()
    {
        menuAudioSource.PlayOneShot(audioClipTransition[Random.Range(0, audioClipTransition.Length)]);
        menu3.SetActive(false);
        menu3B.SetActive(true);
    }

    public void _SettingsMenu()
    {
        menuAudioSource.PlayOneShot(audioClipTransition[Random.Range(0, audioClipTransition.Length)]);
        this._ChangeGraphics();
        menu1.SetActive(false);
        menu2.SetActive(true);
    }

    public void _ExtrasMenu()
    {
        menuAudioSource.PlayOneShot(audioClipTransition[Random.Range(0, audioClipTransition.Length)]);
        menu1.SetActive(false);
        menu3.SetActive(true);
    }
}

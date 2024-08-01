using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EventDialogue : _EventSystem
{
    [SerializeField] private AudioSource audioSource = default;
    [SerializeField] private GameObject dialogueContainer = default;
    private TextMeshProUGUI textMessage = default;

    private void Start()
    {
        textMessage = dialogueContainer.GetComponentInChildren<TextMeshProUGUI>();
        dialogueContainer.gameObject.SetActive(false);
    }

    public IEnumerator PlayDialogue(AudioClip audioClip, List<string> dialogues)
    {
        audioSource.PlayOneShot(audioClip);
        yield return null;

//        this.OnEventStart();
//        for (int i = 0; i < dialogues.Count; i++){
//            textMessage.text = dialogues[i];
//            yield return new WaitForSeconds(dialogues[i].Count() * 0.06f);
//        }
//        this.OnEventEnd();
    }

    public void StopDialogue()
    {
        audioSource.Stop();
        StopAllCoroutines();
        this.OnEventEnd();
    }

    public override void OnEventStart() { } // dialogueContainer.SetActive(true); }

    public override void OnEventActive() { }

    public override void OnEventEnd() { } // dialogueContainer.SetActive(false); }
}

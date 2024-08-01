using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] doorOpen;
    public AudioClip[] doorClose;
    public Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void AnimationDoorClose()
    {
        audioSource.PlayOneShot(doorClose[Random.Range(0, doorClose.Length - 1)]);
        animator.SetBool("DoorClosed", true);
    }

    public void AnimationDoorOpen()
    {
        if(animator.GetBool("DoorClosed") == false)
           audioSource.PlayOneShot(doorOpen[Random.Range(0, doorOpen.Length - 1)]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioBotton : MonoBehaviour, InterectableInterface
{
    [SerializeField] AudioSource BottonAudio;
    public bool isOn;
    public bool isPlayed = false;

    void Start()
    {
        BottonAudio.enabled = isOn;
    }

    public string GetDiscription()
    {
        return "ֽאזלטעו [ֵ]";
    }

    public void Interact()
    {
        if (isOn)
        {
            AudioManager.PlayAudio(BottonAudio);
            isPlayed = true;
        }
        else if (!isOn)
        {
            BottonAudio.Stop();
        }
    }
}

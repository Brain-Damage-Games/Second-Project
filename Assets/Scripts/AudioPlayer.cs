using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] public List<AudioClip> audioClips = new List<AudioClip>();
    private float pitchValue = 1f;
    private float volumeValue=0.5f ;
    private AudioSource audioSource ;
    private int index;

    void Awake(){
        audioSource = gameObject.GetComponent<AudioSource>();
        SetIndex(0);
        Loop(true);
        PlaySound(index);
    }

    public void SetIndex(int index){
        this.index=index;
    }

    public void Loop(bool loop){
        audioSource.loop=loop;
    }
    public void PlaySound(int index){
        
        AudioClip clip = audioClips[index];
        audioSource.clip = clip;
        audioSource.pitch=pitchValue;
        audioSource.volume=volumeValue;
        audioSource.Play();
    }


}

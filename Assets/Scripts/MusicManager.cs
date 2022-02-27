using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource ambience;
    [SerializeField] AudioClip intro;
    [SerializeField] AudioClip full;
    [SerializeField] AudioClip loop;
    [SerializeField] AudioClip gIntro;
    [SerializeField] AudioClip gFull;
    [SerializeField] AudioClip gLoop;
    [SerializeField] AudioClip vintro;
    [SerializeField] AudioClip vfull;
    [SerializeField] AudioClip vloop;
 
    private void Awake(){
        Invoke(nameof(LoopMusic), source.clip.length);
    }
    public void StartGame()
    {
        CancelInvoke();
        source.clip = gFull;
        source.loop = false;
        source.Play();
        Invoke(nameof(LoopGameplay), source.clip.length);
        ambience.Play();
    }
    public void StopGame()
    {
        CancelInvoke();
        ambience.Stop();
        source.clip = intro;
        source.loop = false;
        source.Play();
        Invoke(nameof(LoopMusic), source.clip.length);
    }
    void LoopMusic(){
        source.clip = loop;
        source.loop = true;
        source.Play();
    }
    void LoopGameplay(){
        source.clip = gLoop;
        source.loop = true;
        source.Play();
    }
    public void WinMusic(){
        source.clip = vfull;
        source.loop = false;
        source.Play();
        Invoke(nameof(WinLoop), source.clip.length);
    }
    public void WinLoop(){
        source.clip = vloop;
        source.loop = true;
        source.Play();
    }
}

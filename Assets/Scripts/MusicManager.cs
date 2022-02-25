using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip loop;
 
    void Start()
    {
        Invoke(nameof(LoopMusic), source.clip.length);
    }
    void LoopMusic(){
        source.clip = loop;
        source.loop = true;
        source.Play();
    }
}

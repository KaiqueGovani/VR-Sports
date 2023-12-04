using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonsBoliche : MonoBehaviour
{
    public AudioSource[] audioSources;

    public void PinoSom()
    {
        audioSources[0].Play();
    }

    public void PistaSom()
    {
        audioSources[1].Play();
    }

    public void TristeSom()
    {
        audioSources[2].Play();
    }

    public void FelizSom()
    {
        audioSources[3].Play();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonsArchery : MonoBehaviour
{
    public AudioSource[] audioSources;

    public void SoltarFlechaSom()
    {
        audioSources[0].Play();
    }

    public void PuxarFlechaSom()
    {
        audioSources[1].Play();
    }

    public void AcertarFlechaSom()
    {
        audioSources[2].Play();
    }

    public void FelizSom()
    {
        audioSources[3].Play();
    }
}

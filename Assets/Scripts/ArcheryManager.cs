using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcheryManager : MonoBehaviour
{
    public Text pontosText;
    public int pontos = 0;

    public int flechas = 5;

    void Update(){
        pontosText.text = pontos.ToString();
    }
}

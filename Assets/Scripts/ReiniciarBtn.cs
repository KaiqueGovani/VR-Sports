using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarBtn : MonoBehaviour
{
    public void ReiniciarCena(){
        SceneManager.LoadScene("BolicheVR");
    }
}

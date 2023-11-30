using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarBtn : MonoBehaviour
{
    public string sceneName;
    public GameObject objetoDesativar;
    public void ReiniciarCena(){
        SceneManager.LoadScene(sceneName);
    }
    public void InativarObjeto(){
        objetoDesativar.SetActive(false);
    }
}

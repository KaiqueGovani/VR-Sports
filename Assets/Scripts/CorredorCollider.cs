using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CorredorCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstaculo"))
        {
            SceneManager.LoadScene("Corrida");
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            other.gameObject.GetComponent<FimCorrida>().FimDaCorrida();
        }
    }
}

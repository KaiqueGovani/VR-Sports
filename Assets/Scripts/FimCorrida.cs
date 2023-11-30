using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FimCorrida : MonoBehaviour
{
    public Corredor corredor;
    public GameObject fimUi;

    public void FimDaCorrida()
    {
        corredor.gameObject.GetComponent<Corredor>().enabled = false;
        fimUi.SetActive(true);
    }
}

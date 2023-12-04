using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlvoSegmento : MonoBehaviour
{
    public ArcheryManager manager;
    public SonsArchery sonsArchery;
    public int valorPontos = 0;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Flecha") && collision.gameObject.GetComponent<Arrow>().hasHit == false)
        {
            Debug.Log("Acertou, pontos: " + valorPontos.ToString());
            collision.gameObject.GetComponent<Arrow>().hasHit = true;
            manager.pontos += valorPontos;
            collision.gameObject.GetComponent<Collider>().enabled = false;
            sonsArchery.AcertarFlechaSom();
        }
    }
}

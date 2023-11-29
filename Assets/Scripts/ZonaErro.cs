using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZonaErro : MonoBehaviour
{
    public BolicheManager bolicheManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bola"))
        {
            bolicheManager.Missed();
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Bola>().ResetBall();
        }
    }
}

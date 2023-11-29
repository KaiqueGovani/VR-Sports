using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canaleta : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bola"))
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0, 0, 1.5f);
        }
    }
}

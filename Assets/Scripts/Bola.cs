using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    public Vector3 startPostion;
    public Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        startPostion = transform.position;
        startRotation = transform.rotation;
    }

    public void ResetBall(){
        transform.SetPositionAndRotation(startPostion, startRotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Corredor : MonoBehaviour
{
    public float speed = 5f;
    public float speedIncrease = 0.05f;
    public float speedDecrease = 0.1f;
    public Transform leftHand;
    public Transform rightHand;
    public Transform lastTopHand;

    void Start()
    {
        lastTopHand = leftHand;
    }

    void Update()
    {

        Transform topHand = leftHand.position.y > rightHand.position.y ? leftHand : rightHand;
        if (topHand != lastTopHand)
        {
            speed += speedIncrease * Time.deltaTime;
            lastTopHand = topHand;
        }


        speed = Mathf.Clamp(speed, 0, 10);
        transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        speed -= speedDecrease * Time.deltaTime;

    }
    
}

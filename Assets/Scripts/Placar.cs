using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placar : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pinos")) // Make sure each pin has the tag "Pin"
        {
            score++;
            // You can also deactivate the pin here if needed
        }
    }

    private void Start() {
        scoreText.text = score.ToString();
    }

    private void Update() {
        Debug.Log(score);
        scoreText.text = score.ToString();
    }
}

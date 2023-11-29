using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placar : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    private List<GameObject> pinosCaidos = new List<GameObject>();

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pinos") && !pinosCaidos.Contains(other.gameObject))
        {
            pinosCaidos.Add(other.gameObject);
            score++;
        }
    }

    private void Start() {
        scoreText.text = score.ToString();
    }

    private void Update() {
        scoreText.text = score.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstaculo : MonoBehaviour
{
    public List<GameObject> spawns = new List<GameObject>();
    public List<GameObject> obstaculos = new List<GameObject>();

    
    void Start()
    {
        // Aleatoriamente escolhe um dos 3 spawns points e um dos 3 obstaculos para spawnar
        for (int i = 0; i < 2; i++)
        {
            int spawnIndex = Random.Range(0, spawns.Count);
            int obstaculoIndex = Random.Range(0, obstaculos.Count);
            Instantiate(obstaculos[obstaculoIndex], spawns[spawnIndex].transform.position, Quaternion.identity);
            spawns.RemoveAt(spawnIndex);
            obstaculos.RemoveAt(obstaculoIndex);
        }

    }
}

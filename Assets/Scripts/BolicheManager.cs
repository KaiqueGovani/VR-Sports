using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class BolicheManager : MonoBehaviour
{
    public List<GameObject> pinos = new();
    public List<TransformData> pinosSpawns = new();
    public GameObject bola;
    public List<GameObject> pinosCaidos = new();
    public List<GameObject> camposPlacar = new();
    public GameObject campoTotal;
    public List<Frame> frames = new();
    public bool hitPins = false;
    private float hitTime;
    public float timeToReset = 5f;
    public int currentFrame = 0;
    public int currentRoll = 0;
    public int totalScore = 0;
    public GameObject fimDeJogo;
    public SonsBoliche sonsBoliche;

    // Define a class to store position and rotation
    public class TransformData
    {
        public Vector3 position;
        public Quaternion rotation;

        public TransformData(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }

    [Serializable]
    public class Frame
    {
        public int roll1;
        public int roll2;
        public int score;
        public bool strike;
        public bool spare;
        public bool isComplete;
        public bool isHalfComplete = false;

        public Frame(int roll1, int roll2, int score, bool strike, bool spare, bool isComplete)
        {
            this.roll1 = roll1;
            this.roll2 = roll2;
            this.score = score;
            this.strike = strike;
            this.spare = spare;
            this.isComplete = isComplete;
        }
    }

    void Start()
    {
        getPinosSpawnInfo();
        GenerateEmptyFrames();
        UpdateScoreBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPins && Time.time - hitTime > timeToReset)
        {
            CountRoll();
        }
    }

    void CountRoll(){
        if(currentFrame == 10){
            FinishGame();
            return;
        }
        if(currentRoll == 0){
            frames[currentFrame].roll1 = pinosCaidos.Count;
            if(frames[currentFrame].roll1 == 10){
                frames[currentFrame].strike = true;
                sonsBoliche.FelizSom();
                frames[currentFrame].isComplete = true;
                FinishFrame();
            }
            else{
                frames[currentFrame].isHalfComplete = true;
                FinishRoll();
            }
        }
        else{
            frames[currentFrame].roll2 = pinosCaidos.Count - frames[currentFrame].roll1;
            if((frames[currentFrame].roll1 + frames[currentFrame].roll2) == 10){
                frames[currentFrame].spare = true;
            }
            frames[currentFrame].isComplete = true;
            FinishFrame();
            currentRoll = 0;
        }
        hitPins = false;
    }

    void FinishGame(){
        Debug.Log("Fim de Jogo!");
        sonsBoliche.FelizSom();
        campoTotal.GetNamedChild("Total").GetComponent<Text>().text = totalScore.ToString();
        fimDeJogo.SetActive(true);
    }

    void FinishFrame(){
        CalculateFrameScore(currentFrame);
        resetPinos();
        pinosCaidos.Clear();
        UpdateScoreBoard();
        if (currentFrame == 10){
            FinishGame();
            return;
        }
    }

    void FinishRoll(){
        currentRoll++;
        resetPinos();
        foreach(GameObject pino in pinosCaidos){
            pino.SetActive(false);
        }
        UpdateScoreBoard();
    }

    void GenerateEmptyFrames(){
        for(int i = 0; i < 10; i++){
            frames.Add(new Frame(0, 0, 0, false, false, false));
        }
    }

    void CalculateFrameScore(int frame){
        // Calculate frame scores
        if (frames[frame].strike){
            totalScore += 30;
        }
        else if (frames[frame].spare){
            totalScore += 10 + frames[frame].roll1;
        }
        else {
            totalScore += frames[frame].roll1 + frames[frame].roll2;
        }
        frames[frame].score = totalScore;
        currentFrame++;
    }

    void UpdateScoreBoard(){
        int i = 0;
        foreach(GameObject campoPlacar in camposPlacar){
            campoPlacar.GetNamedChild("Titulo").GetComponent<Text>().text = (i+1).ToString();
            campoPlacar.GetNamedChild("Valor1").GetComponent<Text>().text = frames[i].strike ? "X" : (frames[i].isHalfComplete ? frames[i].roll1.ToString() : "");
            campoPlacar.GetNamedChild("Valor2").GetComponent<Text>().text = frames[i].spare ? "/" : (frames[i].isComplete ? frames[i].roll2.ToString() : "");
            campoPlacar.GetNamedChild("Total").GetComponent<Text>().text = frames[i].isComplete ? frames[i].score.ToString() : "";
            i++;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pinos") && !pinosCaidos.Contains(other.gameObject))
        {
            hitPins = true;
            sonsBoliche.PinoSom();
            hitTime = Time.time;
            pinosCaidos.Add(other.gameObject);
        }
    }

    void getPinosSpawnInfo()
    {
        foreach (GameObject pino in pinos)
        {
            pinosSpawns.Add(new TransformData(pino.transform.position, pino.transform.rotation));
        }
    }

    void resetPinos(){
        for (int i = 0; i < pinosSpawns.Count; i++)
        {
            pinos[i].transform.SetPositionAndRotation(pinosSpawns[i].position, pinosSpawns[i].rotation);
            pinos[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            pinos[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pinos[i].SetActive(true);
        }
    }

    public void Missed(){
        hitPins = true;
        hitTime = Time.time;
        if (pinosCaidos.Count == 0){
            sonsBoliche.TristeSom();
        }
    }
}

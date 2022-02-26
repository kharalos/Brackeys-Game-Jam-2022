using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [Header("Dependencies")]
    [SerializeField] UIBar timerUIBar;
    [SerializeField] TMP_Text scoreUI;
    [Header("Parameters")]
    [SerializeField] float maxTimer = 120;
    public float timerIncreaseAmount = 10;
    public float scoreIncreaseAmount = 10;
    [SerializeField] float scoreAnimScaleFactor = 1.5f;
    [SerializeField] float scoreAnimScaleDownRate = 1;

    float currentScore = 0;
    float currentTimer;
    public event Action OnTimerReachesZero;
    public event Action OnScoreIncrease;

    public void IncreaseTimer() {
        currentTimer += timerIncreaseAmount;
        if (currentTimer > maxTimer) currentTimer = maxTimer;
    }

    public void IncreaseScore() {
        currentScore += scoreIncreaseAmount;
        OnScoreIncrease?.Invoke();

        UpdateScore();

        //visual effects
        StartCoroutine(ScoreAnimation());
        IEnumerator ScoreAnimation() {
            
            Transform t = scoreUI.gameObject.transform;
            t.localScale *= scoreAnimScaleFactor;
            while (t.localScale.x > 1) {
                float value = scoreAnimScaleDownRate * Time.deltaTime;
                t.localScale = new Vector3(t.localScale.x/(1 + value), t.localScale.y/(1+value), t.localScale.z/(1 + value));
                yield return null;
            }
            t.localScale = Vector3.one;
        }
    }

    void UpdateScore() {
        scoreUI.text = $"{currentScore}";

    }

    private void Awake() {
        //enforce singleton
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else {
            _instance = this;
            //regular awake function code (non singleton related) goes here.
            currentTimer = maxTimer;
            UpdateScore();
            //----------------------------
        }
    }

    private void Update() {

        //Handle Timer
        {
            //decay
            if (currentTimer > 0) {
                currentTimer -= Time.deltaTime;
            }
            if (currentTimer <= 0) {
                OnTimerReachesZero?.Invoke();
            }
            //update UI
            timerUIBar.UpdateBarPercentFill(currentTimer / maxTimer);
        }
    }
}

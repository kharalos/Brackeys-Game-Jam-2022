using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [Header("Dependencies")]
    [SerializeField] UIBar timerUIBar;
    [Header("Parameters")]
    [SerializeField] float maxTimer = 120;
    public float timerIncreaseAmount = 10;

    float currentTimer;
    public event Action OnTimerReachesZero;

    public void IncreaseTimer() {
        currentTimer += timerIncreaseAmount;
        if (currentTimer > maxTimer) currentTimer = maxTimer;
    }

    private void Awake() {
        //enforce singleton
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else {
            _instance = this;
            //regular awake function code (non singleton related) goes here.
            currentTimer = maxTimer;
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

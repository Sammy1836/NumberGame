using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    float currentTime = 0f;
    public float startingTime = 120f;

    [SerializeField] Text countdownText;

    void Start()
    {
        currentTime = startingTime;
        countdownText.color = Color.yellow;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 30)
        {
            countdownText.color = Color.red;
        }
        if (currentTime <= 0)
        {
            countdownText.text = "Time Over";
        }
    }
}

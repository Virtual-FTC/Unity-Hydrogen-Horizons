using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public UnityEvent OnCountdownEnd;
    public UnityEvent OnCountdownStart;
    public float countdownTime;

    private Text text;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    public void StartCountdown()
    {
        count = (int)countdownTime;
        OnCountdownStart.Invoke();
        InvokeRepeating("UpdateCountdown", 0, 1);
    }

    void UpdateCountdown()
    {
        text.text = count.ToString();
        count--;
        if(count < 0) { CancelInvoke();  OnCountdownEnd.Invoke(); }
    }
}

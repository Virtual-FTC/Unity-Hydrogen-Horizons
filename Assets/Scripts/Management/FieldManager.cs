using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.Events;

public enum GameMode
{
    Autonomous=0,
    Teleop=1,
    Fullgame=2,
    Freeplay=3
}
public class FieldManager : MonoBehaviour
{
    public static TeamColor botColor;

    public GameMode mode;

    public GameTimeManager gameTimeManager;

    

    private static FieldManager _instance;
    public static FieldManager instance
    {
        get { return _instance; }
    }

    [SerializeField] GlobalInt currentSession;

    public UnityEvent onResetField;

    [DllImport("__Internal")]
    private static extern void updateSignalSensor(int signal);
    private bool hasCheckedSignalSensor = false;
    int currentGameMode = 0;

    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void Start()
    {
#if !UNITY_EDITOR
        if (vrs_messenger.instance == null)
            mode = GameMode.Teleop;
        else
            mode = vrs_messenger.instance.GetPlaymode();
#endif

        GameTimeReceiver timeReceiver = FindObjectOfType<GameTimeReceiver>();

        //SetGameMode(0);
    }
    void Update()
    {
        if(!hasCheckedSignalSensor)
            checkSignalSensor();
    }
    void checkSignalSensor()
    {
        int signalType = 0;
        GameObject signalConeParent = FindObjectOfType<SignalRandomizer>().gameObject;
        foreach(Transform child in signalConeParent.transform)
        {
            signalType++;
            if(child.gameObject.activeInHierarchy)
                break;
        }
#if UNITY_WEBGL && !UNITY_EDITOR
        try{updateSignalSensor(signalType);}
        catch{}
#endif

        hasCheckedSignalSensor = true;
    }

    public void ResetToExistingGameMode()
    {
        SetGameMode(currentGameMode);
    }

    public void resetField()
    {
        onResetField.Invoke();
    }

    public void SetGameMode(int mode)
    {
        currentGameMode = mode;

        Session autonomous = gameTimeManager.sessions[0];
        Session teleOpMid = gameTimeManager.sessions[1];
        Session teleOpEnd = gameTimeManager.sessions[2];
        Session freePlay = gameTimeManager.sessions[3];
        Session gameOver = gameTimeManager.sessions[4];

        switch(mode)
        {
            case (int)GameMode.Autonomous:
                autonomous.PlayThisSession = true;
                teleOpMid.PlayThisSession = false;
                teleOpEnd.PlayThisSession = false;
                freePlay.PlayThisSession = false;
                gameOver.PlayThisSession = true;
                break;
            case (int)GameMode.Teleop:
                autonomous.PlayThisSession = false;
                teleOpMid.PlayThisSession = true;
                teleOpEnd.PlayThisSession = true;
                freePlay.PlayThisSession = false;
                gameOver.PlayThisSession = true;
                break;
            case (int)GameMode.Fullgame:
                autonomous.PlayThisSession = true;
                teleOpMid.PlayThisSession = true;
                teleOpEnd.PlayThisSession = true;
                freePlay.PlayThisSession = false;
                gameOver.PlayThisSession = true;
                break;
            case (int)GameMode.Freeplay:
                autonomous.PlayThisSession = false;
                teleOpMid.PlayThisSession = false;
                teleOpEnd.PlayThisSession = false;
                freePlay.PlayThisSession = true;
                gameOver.PlayThisSession = false;
                break;
        }

    }

}

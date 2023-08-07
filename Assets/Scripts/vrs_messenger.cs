using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vrs_messenger : MonoBehaviour
{
    public static vrs_messenger instance;
    [SerializeField] private GameMode playmode = GameMode.Teleop;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    public void SetPlaymode(int playmode)
    {
        //Debug.Log("playmode = " + playmode);
        this.playmode = (GameMode)playmode;
    }

    public GameMode GetPlaymode()
    {
        return playmode;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public Text redScoreText;
    public Text blueScoreText;

    private int redScore = 0;
    private int blueScore = 0;

    // score values
    [Header("score values")]
    public int HubLevel1Score;
    public int HubLevel2Score;
    public int HubLevel3Score;

    public int HubScore;

    public int BalancedHubScore;
    public int UnbalancedHubScore;
    public float BalanceThreshold;

    public int StoragePartialScore;
    public int StorageCompleteScore;
    public int WarehousePartialScore;
    public int WarehousePartialEndScore;
    public int WarehouseCompleteScore;
    public int WarehouseCompleteEndScore;
    public int duckDropScore;
    public int duckDropEndScore;


    public int DuckScore;
   
    private bool freeze = true;

    public Light[] lights;

    static ScoreKeeper _sk;

    public static ScoreKeeper sk
    {
        get
        {
            return _sk;
        }
    }

    private void Awake()
    {
        _sk = this;
    }

    public void addScoreRed(int points)
    {
        if (!freeze)
        {
            redScore += points;
            Debug.Log("redScore: " + points);
        }
        updateRedScore();
        if (redScore > blueScore)
            setLightsRed();
        else if (blueScore > redScore)
            setLightsBlue();
        else
            setLightsNorm();
    }

    public void addScoreBlue(int points)
    {
        print(points);

        if (!freeze)
        {
            blueScore += points;
            Debug.Log("blueScore: " + points);
        }

        updateBlueScore();
        if (redScore > blueScore)
            setLightsRed();
        else if (blueScore > redScore)
            setLightsBlue();
        else
            setLightsNorm();
    }

    public int getScoreRed()
    {
        return redScore;
    }

    public int getScoreBlue()
    {
        return blueScore;
    }

    void updateRedScore()
    {
        redScoreText.text = "" + redScore;
    }

    void updateBlueScore()
    {
        blueScoreText.text = "" + blueScore;
    }

    public void resetScore()
    {
        freeze = false;
        redScore = 0;
        blueScore = 0;
        updateRedScore();
        updateBlueScore();

        setLightsNorm();
    }

    public void freezeScore()
    {
        freeze = true;
        Debug.Log("FREEZE: " + freeze);
    }

    public void setLightsNorm()
    {
        /*
        Color myColor = new Color();
        var color = "#846032";
        ColorUtility.TryParseHtmlString(color, out myColor);
        for (int x = 0; x < lights.Length; x++)
        {
            lights[x].color = myColor;
        }
        */
    }

    public void setLightsBlue()
    {
        /*
        Color myColor = new Color();
        var color = "#323E84";
        ColorUtility.TryParseHtmlString(color, out myColor);
        for (int x = 0; x < lights.Length; x++)
        {
            lights[x].color = myColor;
        }
        */
    }

    public void setLightsRed()
    {
        /*
        Color myColor = new Color();
        var color = "#A93C4E";
        ColorUtility.TryParseHtmlString(color, out myColor);
        for (int x = 0; x < lights.Length; x++)
        {
            lights[x].color = myColor;
        }
        */
    }

    public void setLightsGreen()
    {
        /*
        Color myColor = new Color();
        var color = "#41A83C";
        ColorUtility.TryParseHtmlString(color, out myColor);
        for (int x = 0; x < lights.Length; x++)
        {
            lights[x].color = myColor;
        }
        */
    }
}

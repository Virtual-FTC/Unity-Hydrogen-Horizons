using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckGoal : MonoBehaviour
{
    public ScoreKeeper scoreKeeper;
    public GameTimer gameTimer;

    bool gamovr=true;

    string team;

    // Start is called before the first frame update
    void Awake()
    {
        if (!scoreKeeper)
        {
            scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        }
        if (!gameTimer)
        {
            gameTimer = GameObject.Find("ScoreKeeper").GetComponent<GameTimer>();
        }
        if (gameObject.name.ToLower().Contains("red")) { team = "Red"; }
        if (gameObject.name.ToLower().Contains("blue")) { team = "Blue"; }

    }

    void Update()
    {
        if (gameTimer.getTimer() <= 0 && !gamovr) { GameOver(); }
    }

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag != "duck") { return; }
        if (team == "Red")
        {
            if (RobotGameManager.rg.currentRound < 3) ScoreKeeper.sk.addScoreBlue(ScoreKeeper.sk.duckDropScore);
            else ScoreKeeper.sk.addScoreBlue(ScoreKeeper.sk.duckDropEndScore);
        }
        else
        {
            if (RobotGameManager.rg.currentRound < 3) ScoreKeeper.sk.addScoreRed(ScoreKeeper.sk.duckDropScore);
            else ScoreKeeper.sk.addScoreRed(ScoreKeeper.sk.duckDropEndScore);
        }

    }
    private void OnTriggerStay(Collider collider)
    {

    }

    void GameOver()
    {
        gamovr = true;
    }
    public void Reset()
    {
        gamovr = false;
    }
}

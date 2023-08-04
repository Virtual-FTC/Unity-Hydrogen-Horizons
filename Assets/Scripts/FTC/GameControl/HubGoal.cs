using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubGoal : MonoBehaviour
{
    bool isBalanced=true;

    public Transform parent;

    string team = "";

    public int HubLevel;

    bool gamovr = true;

    private void Awake()
    {

        if (parent.name.ToLower().Contains("red")) { team = "Red"; }
        if (parent.name.ToLower().Contains("blue")) { team = "Blue"; }
    }

    private void Start()
    {
        RobotGameManager.rg.GameOver += GameOver;
    }

    private void Update()
    {
        if (HubLevel < 4)
        {
            bool deltaBal = isBalanced;
            int balScore = 0;
            if (parent.localEulerAngles.x > ScoreKeeper.sk.BalanceThreshold) { balScore += 1; }
            if (parent.localEulerAngles.x < -ScoreKeeper.sk.BalanceThreshold) { balScore += 1; }
            if (parent.localEulerAngles.z > ScoreKeeper.sk.BalanceThreshold) { balScore += 1; }
            if (parent.localEulerAngles.z < -ScoreKeeper.sk.BalanceThreshold) { balScore += 1; }
            if (balScore > 0) { isBalanced = false; } else { isBalanced = true; }
            if (deltaBal != isBalanced)
            {
                deltaBal = isBalanced;
            }
        }
    }

    List<Collider> Countedcolliders = new List<Collider>();
    bool active = false;
    private void OnTriggerEnter(Collider collider)
    {
        //in game score
        if ((collider.tag == "Cube" || collider.tag == "Ball" || collider.tag == "Duck") && HubLevel <4 && !Countedcolliders.Contains(collider))
        {
            if (team == "Blue")
            {


                active = true;
                if (HubLevel == 1) { ScoreKeeper.sk.addScoreBlue(ScoreKeeper.sk.HubLevel1Score); }
                if (HubLevel == 2) { ScoreKeeper.sk.addScoreBlue(ScoreKeeper.sk.HubLevel2Score); }
                if (HubLevel == 3) { ScoreKeeper.sk.addScoreBlue(ScoreKeeper.sk.HubLevel3Score); }
            }
        
            if (team == "Red")
            {


                active = true;
                if (HubLevel == 1) { ScoreKeeper.sk.addScoreRed(ScoreKeeper.sk.HubLevel1Score); }
                if (HubLevel == 2) { ScoreKeeper.sk.addScoreRed(ScoreKeeper.sk.HubLevel2Score); }
                if (HubLevel == 3) { ScoreKeeper.sk.addScoreRed(ScoreKeeper.sk.HubLevel3Score); }
            }
           
            Countedcolliders.Add(collider);
        }

        //End of Game
        /*if (collider.tag != "Cube" && collider.tag != "Ball" && collider.tag != "Duck" || CountedBalancedColliders.Contains(collider)) { return; }
        if (gameTimer.getTimer() > 0) { return; }

        if (team == "Blue")
        {
            if (isBalanced)
            {
                if (HubLevel == 1) { scoreKeeper.addScoreBlue(scoreKeeper.BalancedHubScore); }
                if (HubLevel == 2) { scoreKeeper.addScoreBlue(scoreKeeper.BalancedHubScore); }
                if (HubLevel == 3) { scoreKeeper.addScoreBlue(scoreKeeper.BalancedHubScore); }
            }
            if (!isBalanced)
            {
                if (HubLevel == 1) { scoreKeeper.addScoreBlue(scoreKeeper.UnbalancedHubScore); }
                if (HubLevel == 2) { scoreKeeper.addScoreBlue(scoreKeeper.UnbalancedHubScore); }
                if (HubLevel == 3) { scoreKeeper.addScoreBlue(scoreKeeper.UnbalancedHubScore); }
            }
        }
        if (team == "Red")
        {
            if (isBalanced)
            {
                if (HubLevel == 1) { scoreKeeper.addScoreRed(scoreKeeper.BalancedHubScore); }
                if (HubLevel == 2) { scoreKeeper.addScoreRed(scoreKeeper.BalancedHubScore); }
                if (HubLevel == 3) { scoreKeeper.addScoreRed(scoreKeeper.BalancedHubScore); }
            }
            if (!isBalanced)
            {
                if (HubLevel == 1) { scoreKeeper.addScoreRed(scoreKeeper.UnbalancedHubScore); }
                if (HubLevel == 2) { scoreKeeper.addScoreRed(scoreKeeper.UnbalancedHubScore); }
                if (HubLevel == 3) { scoreKeeper.addScoreRed(scoreKeeper.UnbalancedHubScore); }
            }
        }
        CountedBalancedColliders.Add(collider);
        Destroy(collider.gameObject);*/
    }

    private void OnTriggerExit(Collider collider)
    {
        if ((collider.tag == "Cube" || collider.tag == "Ball" || collider.tag == "Duck") && HubLevel < 4 && !Countedcolliders.Contains(collider))
        {
            if (team == "Blue")
            {
                active = false;

                if (HubLevel == 1) { ScoreKeeper.sk.addScoreBlue(-ScoreKeeper.sk.HubLevel1Score); }
                if (HubLevel == 2) { ScoreKeeper.sk.addScoreBlue(-ScoreKeeper.sk.HubLevel2Score); }
                if (HubLevel == 3) { ScoreKeeper.sk.addScoreBlue(-ScoreKeeper.sk.HubLevel3Score); }
            }

            if (team == "Red")
            {

                active = true;
                if (HubLevel == 1) { ScoreKeeper.sk.addScoreRed(-ScoreKeeper.sk.HubLevel1Score); }
                if (HubLevel == 2) { ScoreKeeper.sk.addScoreRed(-ScoreKeeper.sk.HubLevel2Score); }
                if (HubLevel == 3) { ScoreKeeper.sk.addScoreRed(-ScoreKeeper.sk.HubLevel3Score); }
            }

            if(Countedcolliders.Contains(collider))Countedcolliders.Remove(collider);
        }
    }

    public void AddAutoScore()
    {
        if (Countedcolliders.Count > 0)
        {
            if (team == "Blue")
            {
                for (int i = 0; i < Countedcolliders.Count; i++)
                {
                    ScoreKeeper.sk.addScoreBlue(-ScoreKeeper.sk.HubScore);
                }
            }
            else if (team == "Red")
            {
                for (int i = 0; i < Countedcolliders.Count; i++)
                {
                    ScoreKeeper.sk.addScoreRed(-ScoreKeeper.sk.HubScore);
                }
            }
        }
    }

    public void GameOver()
    {
        if (gamovr) { return; }
        
        if(isBalanced && HubLevel < 4 && !gamovr && active)
        {
            if (team == "Blue")
            {
                Debug.Log("HubGoal " + gameObject.name + " HUB#" + HubLevel + " " + team + " GameOver");
                ScoreKeeper.sk.addScoreBlue(ScoreKeeper.sk.BalancedHubScore);
            }
            if (team == "Red")
            {
                Debug.Log("HubGoal " + gameObject.name + " HUB#" + HubLevel + " " + team + " GameOver");
                ScoreKeeper.sk.addScoreRed(ScoreKeeper.sk.BalancedHubScore);
            }
        }
        if ( HubLevel == 4 && !gamovr && active)
        {
            if (transform.localEulerAngles.z < 0f)
            {
                Debug.Log("HubGoal " + gameObject.name + " HUB#" + HubLevel + " " + team + " GameOver");
                ScoreKeeper.sk.addScoreBlue(ScoreKeeper.sk.UnbalancedHubScore);
            }
            if (transform.localEulerAngles.z > 0f)
            {
                Debug.Log("HubGoal " + gameObject.name + " HUB#" + HubLevel + " " + team + " GameOver");
                ScoreKeeper.sk.addScoreRed(ScoreKeeper.sk.UnbalancedHubScore);
            }
        }
        gamovr = true;

    }

    public void Reset()
    {
        gamovr = false;
        active = false;
        Countedcolliders = new List<Collider>();
        //CountedBalancedColliders = new List<Collider>();
    }

}

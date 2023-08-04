using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGoal : MonoBehaviour
{

    string team="";
    string type = "";

    bool gamovr = true;

    private void Awake()
    {

        if (gameObject.name.ToLower().Contains("red")) { team = "Red"; }
        if (gameObject.name.ToLower().Contains("blue")) { team = "Blue"; }
        if (gameObject.name.ToLower().Contains("warehouse")) { type = "warehouse"; }
        if (gameObject.name.ToLower().Contains("storage")) { type = "storage"; }


    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    int triggers = 0;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "RobotTrigger") { triggers += 1; }

    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "RobotTrigger") { triggers -= 1; }

    }

    public void AddScore(int roundIndex)
    {
        if (roundIndex == 0)
        {
            if (triggers == 4)
            {
                if (team == "Blue")
                {

                    ScoreKeeper.sk.addScoreBlue(ScoreKeeper.sk.StorageCompleteScore);
 
                }

                if (team == "Red")
                {

                    ScoreKeeper.sk.addScoreRed(ScoreKeeper.sk.StorageCompleteScore);

                }

            }
            else
            {
                if (triggers > 0)
                {
                    if (team == "Blue")
                    {
                        ScoreKeeper.sk.addScoreBlue(ScoreKeeper.sk.StoragePartialScore);

                    }

                    if (team == "Red")
                    {
                        ScoreKeeper.sk.addScoreRed(ScoreKeeper.sk.StoragePartialScore);

                    }
                }
            }
        }
    

    }
    public void Reset()
    {
        triggers = 0;
        gamovr = false;
    }
}

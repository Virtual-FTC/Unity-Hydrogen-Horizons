using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRobotColor : MonoBehaviour, ICustomGoalChecker
{
    GoalZoneBaseData goalZoneBaseData;
    GoalZoneScoreLink goalZoneScoreLink;

    // Start is called before the first frame update
    void Start()
    {
        goalZoneBaseData = GetComponent<GoalZoneBaseData>();
        goalZoneScoreLink = GetComponent<GoalZoneScoreLink>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoCustomCheck()
    {
        throw new System.NotImplementedException();
    }
    public void DoCustomCheck(GameObject objectToCheck, int scoreDirection)
    {
        if (goalZoneBaseData.scoreZoneColor ==
            objectToCheck.GetComponentInParent<ScoreObjectTypeLink>().LastTouchedTeamColor)
            goalZoneScoreLink.OptionalBoolValue = true;
    }
}

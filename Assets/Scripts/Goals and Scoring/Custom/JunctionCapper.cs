using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JunctionCapper : MonoBehaviour, ICustomGoalEvents, ICustomGoalChecker
{
    Stack<TeamColor> objectsOnJunction;
    public bool IsCapped { get { if (objectsOnJunction.Count > 0) return true; else return false; } }
    public TeamColor CurrentCapColor { get => objectsOnJunction.Peek(); }

    [SerializeField] GlobalInt currentRound;

    [SerializeField] ScoringGuide cappedScoringGuide;

    [SerializeField] GameEvent checkCircuit;

    bool currentCapsAccountedFor;
    int scoringIndexOfObjectTypeCappingJunction;
    private GoalZoneScoreLink goalZoneScoreLink;

    // Start is called before the first frame update
    void Start()
    {
        objectsOnJunction = new Stack<TeamColor>();
    }

    // Update is called once per frame
    void Update()
    {
        // Start scoring in Round 3 (index 2)
        if (currentRound.globalInt == GameTimeManager.GameOverRound 
            && !currentCapsAccountedFor && objectsOnJunction.Count > 0)
        {
            currentCapsAccountedFor = true;
            ChangeTheScore(goalZoneScoreLink, 1, objectsOnJunction.Peek());
        }
    }
    
    public void DoCustomOffEvent(UnityEngine.Object objectToPass)
    {
        goalZoneScoreLink = (GoalZoneScoreLink)objectToPass;
        bool foundObj = objectsOnJunction.TryPop(out TeamColor departingColor);

        if (objectsOnJunction.Count > 0 && departingColor != objectsOnJunction.Peek() &&
            currentRound.globalInt == 2)
        {
            ChangeTheScore(goalZoneScoreLink, -1, departingColor);
            ChangeTheScore(goalZoneScoreLink, 1, objectsOnJunction.Peek(), 1);
        }
        else if (objectsOnJunction.Count < 1 && currentRound.globalInt == 2)
            ChangeTheScore(goalZoneScoreLink, -1, departingColor);

        checkCircuit.Raise();
    }

    public void DoCustomOnEvent(UnityEngine.Object objectToPass)
    {
        goalZoneScoreLink = (GoalZoneScoreLink)objectToPass;


        if (objectsOnJunction.Count > 0)
        {
            TeamColor replacedColorInStack = objectsOnJunction.Peek();
            objectsOnJunction.Push(goalZoneScoreLink.LastObjectTeamColor);

            if (replacedColorInStack != objectsOnJunction.Peek() && 
                currentRound.globalInt == 2 && currentCapsAccountedFor)
            {
                ChangeTheScore(goalZoneScoreLink, -1, replacedColorInStack, 1);
                ChangeTheScore(goalZoneScoreLink, 1, objectsOnJunction.Peek());
            }
        }
        else
        {
            objectsOnJunction.Push(goalZoneScoreLink.LastObjectTeamColor);
            if (currentRound.globalInt == 2 && currentCapsAccountedFor)
            {
                ChangeTheScore(goalZoneScoreLink, 1, objectsOnJunction.Peek());
            }
        }

        checkCircuit.Raise();

    }

    private void ChangeTheScore(GoalZoneScoreLink goalZoneScoreLink, 
        int direction, TeamColor teamColor)
    {
        goalZoneScoreLink.ChangeScore(scoringIndexOfObjectTypeCappingJunction, 
            cappedScoringGuide, direction, teamColor);
    }
    private void ChangeTheScore(GoalZoneScoreLink goalZoneScoreLink, 
        int direction, TeamColor teamColor, int scoreIndexOfObject)
    {
        goalZoneScoreLink.ChangeScore(scoreIndexOfObject, 
            cappedScoringGuide, direction, teamColor);
    }

    public void DoCustomCheck()
    {
    }

    public void DoCustomCheck(GameObject objectToCheck, int scoreDirection)
    {
        ScoreObjectTypeLink scoreObjectTypeLink = 
            objectToCheck.GetComponentInParent<ScoreObjectTypeLink>();
        ObjectType objectType = scoreObjectTypeLink.ScoreObjectType_;

        foreach (ObjectType thisObjectType in cappedScoringGuide.scoreObjectTypes)
        {
            if (objectType == thisObjectType)
            {
                scoringIndexOfObjectTypeCappingJunction = 
                    Array.FindIndex(cappedScoringGuide.scoreObjectTypes, w => w == objectType);
            }
        }
    }
}

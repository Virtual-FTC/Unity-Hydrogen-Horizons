using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedOverConeStackPenalizer : MonoBehaviour
{
    TeamColor coneStackColor, lastTouchedColor;
    [SerializeField] ScoreTracker blueScoreTracker;
    [SerializeField] ScoreTracker redScoreTracker;

    [SerializeField] GlobalInt penalty;

    [SerializeField] int numberOfPenalties;

    // Start is called before the first frame update
    void Start()
    {
        coneStackColor = GetComponent<ConeStackColorSwitcher>().TeamColor_;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ScoreObjectTypeLink scoreObjectTypeLink;
        if (collision.gameObject.GetComponentInParent<ScoreObjectTypeLink>() != null)
        {
           scoreObjectTypeLink = 
                collision.gameObject.GetComponentInParent<ScoreObjectTypeLink>();

            lastTouchedColor = scoreObjectTypeLink.LastTouchedTeamColor;
        }
    }

    public void PenalizeOrNot()
    {
        if (lastTouchedColor != coneStackColor)
        { 
            switch (lastTouchedColor)
            {
                case TeamColor.Blue:
                    blueScoreTracker.AddOrSubtractScore(-penalty.globalInt * numberOfPenalties);
                    break;
                case TeamColor.Red:
                    redScoreTracker.AddOrSubtractScore(-penalty.globalInt * numberOfPenalties);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeStackOrientationChecker : MonoBehaviour
{
    List<ConeOrientation> coneOrientations;
    public bool IsRightSideUp { get; private set; } = true;
    KnockedOverConeStackPenalizer knockedOverConeStackPenalizer;

    ConeStackRBManager coneStackRBManager;

    // Start is called before the first frame update
    void Start()
    {
        coneStackRBManager = GetComponent<ConeStackRBManager>();
        coneOrientations = new List<ConeOrientation>();
        knockedOverConeStackPenalizer = GetComponent<KnockedOverConeStackPenalizer>(); 

        foreach(ConeOrientation coneOrientation in GetComponentsInChildren<ConeOrientation>())
        {
            coneOrientations.Add(coneOrientation);  
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRightSideUp)
        {
            IsRightSideUp = GetConeOrientations();
            if (!IsRightSideUp)
            {
                coneStackRBManager.SwitchOnPhysicalCones();
                knockedOverConeStackPenalizer.PenalizeOrNot();
            }
        }
    }

    bool GetConeOrientations()
    {
        int numberOfKnockedOverCones = 0;
        foreach (ConeOrientation coneOrientation in coneOrientations)
        {
            if (!coneOrientation.IsRightSideUp)
                numberOfKnockedOverCones++;
        }

        return numberOfKnockedOverCones < 4;
    }
}

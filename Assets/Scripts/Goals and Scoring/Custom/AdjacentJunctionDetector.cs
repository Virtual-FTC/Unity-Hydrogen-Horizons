using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjacentJunctionDetector : MonoBehaviour
{
    public List<JunctionCapper> AdjacentJunctionCappers { get; set; }
    BoxCollider boxCollider;
    JunctionCapper thisJunctionCapper;
    // Start is called before the first frame update
    void Start()
    {
        thisJunctionCapper = transform.parent.GetComponentInChildren<JunctionCapper>();
        AdjacentJunctionCappers = new List<JunctionCapper>();
        boxCollider = GetComponent<BoxCollider>();
        CheckForAdjacentJunctionCappers();
    }

    private void CheckForAdjacentJunctionCappers()
    {
        Collider[] overlappingColliders = Physics.OverlapBox(transform.position, boxCollider.bounds.extents);

        for (int i = 0; i < overlappingColliders.Length; i++)
        {
            if (overlappingColliders[i].GetComponent<JunctionCapper>() != null)
            {
                AdjacentJunctionCappers.Add(overlappingColliders[i].GetComponentInChildren<JunctionCapper>());
            }
        }

        JunctionCapper theJunctionCapperOnThisJunction = transform.parent.GetComponentInChildren<JunctionCapper>();
        AdjacentJunctionCappers.Remove(theJunctionCapperOnThisJunction);
    }

    public void CheckAdjacentColor(CheckForCircuit checkForCircuit)
    {

        if (thisJunctionCapper.IsCapped == false)
            return;

        checkForCircuit.JunctionCappersChecked.Add(thisJunctionCapper);

        foreach (JunctionCapper junctionCapper in AdjacentJunctionCappers)
        {
            if (junctionCapper.IsCapped && 
                junctionCapper.CurrentCapColor == thisJunctionCapper.CurrentCapColor)
            {
                if (CheckForTerminus(checkForCircuit) && junctionCapper.CurrentCapColor == checkForCircuit.CircuitColor)
                {
                    checkForCircuit.CircuitFound.boolValue = true;
                    break;
                }

                if (!checkForCircuit.JunctionCappersChecked.Contains(junctionCapper))
                {
                    AdjacentJunctionDetector nextAdjacentJunctionDetector = junctionCapper.transform.parent.parent.parent.GetComponentInChildren<AdjacentJunctionDetector>();
                    nextAdjacentJunctionDetector.CheckAdjacentColor(checkForCircuit);
                }
            }
        }
    }

    bool CheckForTerminus(CheckForCircuit checkForCircuit)
    {
        bool terminusFound = false;

        for (int i = 0; i < checkForCircuit.TerminusJunctions.Length; i++)
        {
            if (checkForCircuit.TerminusJunctions[i] == transform.parent.gameObject) 
                terminusFound = true;
        }

        return terminusFound;
    }
}

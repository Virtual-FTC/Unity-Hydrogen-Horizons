using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalRandomizer : MonoBehaviour
{
    [SerializeField] List<GameObject> locations;
    // Start is called before the first frame update
    void Start()
    {
        SetRandomLocation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetRandomLocation()
    {
        foreach (GameObject location in locations)
        {
            location.SetActive(false);
        }

        int randomLocation = Random.Range(0, 3);

        locations[randomLocation].SetActive(true);
    }

}

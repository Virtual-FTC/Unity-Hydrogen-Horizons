using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCarousel : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Vector3 dir;
    public bool hasDuck;
    bool isSpinning;
    RobotController controller;

    [SerializeField] bool redTeam;

    [SerializeField] float baseRotateSpeed = 0.2f;

    float timer, timerSet = 3;

    bool gameStarted;

    [SerializeField] int team;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = timerSet;

        RobotGameManager.rg.GameStart += StartGame;
    }

    void StartGame()
    {
        gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted) return;
        if (hasDuck && isSpinning)
        {
            if(controller == null) controller = FindObjectOfType<RobotController>(); 

            float speed = controller.rightTrigger;
            transform.eulerAngles += dir * Time.deltaTime * (baseRotateSpeed + speed);

        }
        if(!hasDuck && !isSpinning)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                SpawnNewDuck();
                timer = timerSet;
            }
        }
        
    }


    void SpawnNewDuck()
    {
        if (!gameStarted || hasDuck) return;

        if (redTeam)
        {
            RobotGameManager.rg.SpawnNewDuck(RobotGameManager.rg.redCarouselDuckSpawn);
        }else
        {
            RobotGameManager.rg.SpawnNewDuck(RobotGameManager.rg.blueCarouselDuckSpawn);
        }
    }

    private void OnTriggerStay(Collider collision)
    {

        if (collision.tag == "Spinner" && !isSpinning && hasDuck)
        {
            isSpinning = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Spinner")
        {
            isSpinning = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.collider.tag == "Duck" && !hasDuck)
        {
            hasDuck = true;
            timer = timerSet;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Duck" && hasDuck)
        {
            hasDuck = false;
            isSpinning = false;
        }

    }

}
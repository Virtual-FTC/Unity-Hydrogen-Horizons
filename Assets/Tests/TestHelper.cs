using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TestHelper
{
    public static Dictionary<string, ScoreTracker> scores = new Dictionary<string, ScoreTracker>();
    public static SelectBotOptions botOptions;
    public static GameObject[,] gameGrid;
    static int gridHeight = 5;
    static int gridWidth = 5;
    public static JunctionCapper[] scoringLocs;
    public static string testPattern = "TB0,A0,B1,C2,D3,E4,TB1";
    public static Dictionary<string,GameObject> terminals = new Dictionary<string,GameObject>();
    //TB0,TB1,TR0,TR1
    public static FieldManager fieldManager;
    public static GameTimeManager timeManager;

    public static GameObject getGoalOnGrid(string coords)
    {
        if(coords[0] == 'T')
        {
            return terminals[coords];
        }
        Vector2Int loc = getGridLocation(coords);
        return gameGrid[loc.x, loc.y];
    }

    public static Vector2Int getGridLocation(string coords)
    {
        int acode = (int)'A';
        int column = (int)coords[0];
        column -= acode;
        return new Vector2Int(column, int.Parse(coords[1].ToString()));
    }

    public static void setGoalOnGrid(string coords, GameObject obj)
    {
        Vector2Int loc = getGridLocation(coords);
        gameGrid[loc.x, loc.y] = obj;
    }

    public static void CreateGrid()
    {
        terminals.Clear();
        CheckRobotColor[] terms = GameObject.FindObjectsOfType<CheckRobotColor>();
        foreach(CheckRobotColor term in terms)
        {
            if (term.transform.name.Split('-').Length == 1) { continue; }
            string coords = term.transform.name.Split('-')[1];
            terminals.Add(coords, term.gameObject);
        }
        scoringLocs = GameObject.FindObjectsOfType<JunctionCapper>();
        foreach (JunctionCapper cap in scoringLocs)
        {
            string coords = cap.transform.parent.parent.parent.name.Split('-')[1];//need a 
            setGoalOnGrid(coords, cap.gameObject);
        }
    }

    public static void StartMode(GameMode mode)
    {
        fieldManager.SetGameMode((int)mode);
        //botOptions.StartGame();
        timeManager.SetUpTimer();
        timeManager.Play();
    }

    public static void EndGame()
    {
        timeManager.EndGame();
        timeManager.Stop();
    }
    public static void EndRound()
    {
        timeManager.EndRound();
    }

    public static void ReadyTest()//this needs to be called after scene is ready, which is why its not in setup.
    {

        ScoreTracker[] trackers = Resources.LoadAll<ScoreTracker>("");
        scores.Clear();
        foreach (ScoreTracker tracker in trackers)
        {
            if (tracker.name.Contains("Red"))
            {
                scores.Add("Red", tracker);
            }
            else
            {
                scores.Add("Blue", tracker);
            }
        }
        gameGrid = new GameObject[gridWidth, gridHeight];

        CreateGrid();
        fieldManager = GameObject.FindObjectOfType<FieldManager>();
        timeManager = GameObject.FindObjectOfType<GameTimeManager>();
        botOptions = GameObject.FindObjectOfType<SelectBotOptions>();
        if (botOptions) { botOptions.gameObject.SetActive(false); }

    }
}

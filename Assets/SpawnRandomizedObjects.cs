using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SpawnObjData
{
    public GameObject prefab;
    public int amount;
}

public class SpawnRandomizedObjects : MonoBehaviour
{
    [SerializeField]
    public List<SpawnObjData> objectsToSpawn;
    public float horizontalRadius = 1;
    public float verticalRadius = 0;

    public bool inPlay = false;
    public TeamColor owningTeam = TeamColor.Either;
    public Transform dispenseLocation;
    //colorize option for owning team?

    private Stack<GameObject> currentPieces = new Stack<GameObject>();
    //need a way of notifying remove from a stack that is in play or just count children

    public int Count { get { return currentPieces.Count; } }

    private int childCount()
    {
        int count = 0;
        foreach (Transform t in gameObject.transform)
        {
            count++;
        }
        return count;
    }
    /*
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            StartCoroutine(clearAndRestack());
        }
    }

    private IEnumerator clearAndRestack()
    {
        //wait for end of frame only works if game view is active.
        //yield return new WaitForEndOfFrame();
        yield return StartCoroutine(clearStack());
        //yield return new WaitForEndOfFrame();
        CreateStack();
    }
    private IEnumerator clearStack()
    {
        Transform[] objs = GetComponentsInChildren<Transform>();
        yield return new WaitForSecondsRealtime(0.01f);
        //yield return new WaitForEndOfFrame();
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i] == this.transform || objs[i] == false) { continue; }
            if (Application.isEditor && !Application.isPlaying)
            {
                UnityEngine.Object.DestroyImmediate(objs[i].gameObject);
            }
            else
            {

                UnityEngine.Object.Destroy(objs[i].gameObject);
            }
        }
        yield return false;
    }
    */
    public void CreateStack()
    {
        foreach(SpawnObjData data in objectsToSpawn)
        {
            for (int i = 0; i < data.amount; i++)
            {
                GameObject piece = GameObject.Instantiate(data.prefab);
                Vector3 pos = transform.position;
                pos.x += Random.Range(-horizontalRadius,horizontalRadius);
                pos.z += Random.Range(-horizontalRadius,horizontalRadius);
                pos.y += Random.Range(-verticalRadius,verticalRadius);
                piece.transform.position = pos;
            }
        }
        /*MeshRenderer r = piecePrefab.GetComponentInChildren<MeshRenderer>();
        for (int i = 0; i < startingAmt; i++)
        {
            GameObject piece = GameObject.Instantiate(piecePrefab);
            piece.transform.position = transform.position;
            piece.transform.position += new Vector3(0, (r.bounds.size.y + 0.001f) * i, 0);
            if (!inPlay)
            {
                SetRigidbody(piece, false);
                piece.transform.parent = transform;//parent objects transform/rot affects child objects rot in a weird way.
                currentPieces.Push(piece);
                piece.hideFlags = HideFlags.DontSaveInEditor;
            }
            else if (!Application.isPlaying)
            {
                piece.transform.parent = transform;
            }

        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateStack();
        //StartCoroutine(clearAndRestack());
    }
}


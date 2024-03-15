using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsetScript : MonoBehaviour
{
    public int maxObstacles;
    public List<Tuple<GameObject, float>> ClosestObstacles;
    //public Dictionary<GameObject, float> Obstacles;

    [HideInInspector] public float closestDist;
    [HideInInspector] public GameObject closestObj;
    private GameObject prevClosestObj;
    // Start is called before the first frame update
    void Start()
    {
        ClosestObstacles = new List<Tuple<GameObject, float>>();
    }

    // Update is called once per frame
    void Update()
    {
        //set audio and vibration intensity to closestDist
        //Debug.Log(closestDist);
        //closestDist = float.MaxValue;
        foreach (var pair in ClosestObstacles)
        {
            pair.Item1.GetComponentInChildren<Renderer>().material.color = Color.red;
        }
        ClosestObstacles.Clear();
    }
}

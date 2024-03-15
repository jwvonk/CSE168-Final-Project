using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsetScript : MonoBehaviour
{
    public List<GameObject> closestObstacles;

    [HideInInspector] public float closestDist;
    [HideInInspector] public GameObject closestObj;
    private GameObject prevClosestObj;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //set audio and vibration intensity to closestDist
        Debug.Log(closestDist);
        closestDist = float.MaxValue;
        if (closestObj != prevClosestObj)
        {
            if (prevClosestObj != null) 
            {
                prevClosestObj.GetComponentInChildren<Renderer>().material.color = Color.blue;
            }
            if (closestObj != null)
            {
                closestObj.GetComponentInChildren<Renderer>().material.color = Color.red;
            }
            prevClosestObj = closestObj;
        }
        
    }
}

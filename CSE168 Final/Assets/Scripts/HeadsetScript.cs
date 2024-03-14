using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsetScript : MonoBehaviour
{
    public float closestDist;
    public GameObject closestObj;
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
        closestObj.GetComponentInChildren<Renderer>().material.color = Color.red;
        closestObj = null;
    }
}

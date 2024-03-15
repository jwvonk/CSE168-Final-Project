using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsetScript : MonoBehaviour
{
    public int maxObstacles;
    public List<Tuple<GameObject, float>> ClosestObstacles;
    public AudioClip audioClip;
    //public Dictionary<GameObject, float> Obstacles;
    private List<AudioSource> audioSources = new List<AudioSource>();

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
            AudioSource audioSource = pair.Item1.GetComponent<AudioSource>();
            if(audioSource != null)
            {
               float volume = CalculateVolume(pair.Item2);
               audioSource.volume = volume;
                Debug.Log($"Volume: {volume}");
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
        ClosestObstacles.Clear();
    }
    float CalculateVolume(float distance)
    {
        // Adjust the volume based on the distance
        // You can customize the function to fit your desired volume curve
        // For example, you might want the volume to decrease linearly with distance
        float maxVolume = 1f;
        float minVolume = 0f;
        float maxDistance = 10f; // Adjust this value based on your scene
        float minDistance = 1f; // Adjust this value based on your scene

        float volume = maxVolume - ((distance - minDistance) / (maxDistance - minDistance)) * (maxVolume - minVolume);
        volume = Mathf.Clamp01(volume); // Ensure volume is within range [0, 1]
        return volume;
    }
}

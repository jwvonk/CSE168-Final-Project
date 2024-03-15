using Oculus.Haptics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public int maxObstacles;
    public Tuple<GameObject, float> ClosestObstacle;
    public bool isLeftController;
    public HapticClip clip;	 // Assign this in the Unity editor
    private HapticClipPlayer player;

    private GameObject prevClosestObj;

    void Awake()
    {
        player = new HapticClipPlayer(clip);
    }
    // Start is called before the first frame update
    void Start()
    {
        ClosestObstacle = Tuple.Create<GameObject, float>(null, float.MaxValue);
    }

    // Update is called once per frame
    void Update()
    {
        //set audio and vibration intensity to closestDist
        if (ClosestObstacle.Item1 != null)
        {
            float amplitude = CalculateAmplitude(ClosestObstacle.Item2);
            player.amplitude = amplitude;
            if(isLeftController == true)
            {
                player.Play(Controller.Left);
            }
            else
            {
                player.Play(Controller.Right);
            }
             
        }
        ClosestObstacle = Tuple.Create<GameObject, float>(null, float.MaxValue);

    }
    public void StopHaptics()
    {
        player.Stop();
    }

    void OnDestroy()
    {
        // Free the HapticClipPlayer
        player.Dispose();
    }

    float CalculateAmplitude(float distance)
    {
        // Adjust the Amplitude based on the distance
        // You can customize the function to fit your desired Amplitude curve
        // For example, you might want the Amplitude to decrease linearly with distance
        float maxAmplitude = 1f;
        float minAmplitude = 0f;
        float maxDistance = 10f; // Adjust this value based on your scene
        float minDistance = 1f; // Adjust this value based on your scene

        float Amplitude = maxAmplitude - ((distance - minDistance) / (maxDistance - minDistance)) * (maxAmplitude - minAmplitude);
        Amplitude = Mathf.Clamp01(Amplitude); // Ensure Amplitude is within range [0, 1]
        return Amplitude;
    }
}

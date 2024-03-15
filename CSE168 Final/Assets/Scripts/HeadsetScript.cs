using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HeadsetScript : MonoBehaviour
{
    public int maxObstacles;
    public List<Tuple<GameObject, float, Vector3>> ClosestObstacles;
    //public Dictionary<GameObject, float> Obstacles;
    [SerializeField] private Material TrackedMaterial;
    [SerializeField] private List<AudioClip> Clips;
    [SerializeField] private GameObject AudioSourcePrefab;
    private List<GameObject> AudioSources;

    // Start is called before the first frame update
    void Start()
    {
        ClosestObstacles = new List<Tuple<GameObject, float, Vector3>>();
        AudioSources = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(closestDist);
        //closestDist = float.MaxValue;
        for (int i = 0; i < ClosestObstacles.Count; i++)
        {
            var pair = ClosestObstacles[i];

            if (AudioSources.Count <= i)
            {
                AudioSources.Add(Instantiate(AudioSourcePrefab));
            }
            AudioSources[i].SetActive(true);
            AudioSources[i].transform.position = pair.Item3;
            AudioSource audioSource = AudioSources[i].GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.clip = Clips[(Clips.Count / ClosestObstacles.Count) * i];
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            // pair.Item1.AddComponent sound thing
            // soundthing.volume = pair.item2
            // set audio and vibration intensity to closestDist
            pair.Item1.GetComponentInChildren<Renderer>().material = TrackedMaterial;

        }

        for (int i = ClosestObstacles.Count; i < maxObstacles; i++)
        {
            if (AudioSources.Count > i)
            {
                AudioSources[i].SetActive(false);
            }
        }
        ClosestObstacles.Clear();
    }
}

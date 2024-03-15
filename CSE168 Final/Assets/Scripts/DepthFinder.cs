using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFinder : MonoBehaviour
{
    public GameObject mainCamera;
    private HeadsetScript headsetScript;
    public Collider collider;
    bool run = true;

    private void Awake()
    {
        var a = GetComponent<OVRScenePlane>();
        if (a)
        {
            a.OffsetChildren = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        var a = GetComponent<OVRScenePlane>();
        if (a)
        {
            a.OffsetChildren = false;
        }
        if (transform.GetComponent<OVRSemanticClassification>().Contains("FLOOR") || transform.GetComponent<OVRSemanticClassification>().Contains("CEILING"))
        {
            run = false;
        } else
        {
            if (mainCamera == null)
            {
                mainCamera = GameObject.FindWithTag("MainCamera");
            }
            headsetScript = GameObject.FindWithTag("CameraRig").GetComponent<HeadsetScript>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<Renderer>().material.color = Color.blue;
        if (run && headsetScript.ClosestObstacles != null)
        {
            var userPoints = new List<Vector3>
            {
                mainCamera.transform.position,
                new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y / 2, mainCamera.transform.position.z),
                new Vector3(mainCamera.transform.position.x, 0.2f, mainCamera.transform.position.z)
            };
            var nearestDist = float.MaxValue;
            foreach (var point in userPoints)
            {
                Vector3 nearestVertex = collider.ClosestPoint(mainCamera.transform.position);
                float dist = Vector3.Distance(nearestVertex, mainCamera.transform.position);
                nearestDist = Mathf.Min(nearestDist, dist);
            }

            if (headsetScript.ClosestObstacles.Count < headsetScript.maxObstacles)
            {
                headsetScript.ClosestObstacles.Add(Tuple.Create(gameObject, nearestDist));
                headsetScript.ClosestObstacles.Sort((x, y) => x.Item2.CompareTo(y.Item2));

            }
            else
            {
                if (headsetScript.ClosestObstacles[headsetScript.ClosestObstacles.Count - 1].Item2 > nearestDist)
                {
                    headsetScript.ClosestObstacles[headsetScript.ClosestObstacles.Count - 1] = Tuple.Create(gameObject, nearestDist);
                    headsetScript.ClosestObstacles.Sort((x, y) => x.Item2.CompareTo(y.Item2));
                }
            }

        }

    }

    //public Vector3 NearestVertexTo(Vector3 point)
    //{
    //    point = transform.InverseTransformPoint(point);

    //    float minDist = float.MaxValue;

    //    Vector3 nearestVertex = Vector3.zero;

    //    foreach(Vector3 vertex in mesh.mesh.vertices) {
    //        if (GetComponent<OVRSceneAnchor>().Uuid.ToString() == "e3b41f99-4960-4736-b1b4-548f343f40e5")
    //        {
    //            Debug.Log(vertex);
    //        }
    //        float diff = getDist(point, vertex);
    //        if (diff < minDist)
    //        {
    //            minDist = diff;
    //            nearestVertex = vertex;
    //        }

    //    }

    //    return transform.TransformPoint(nearestVertex);
    //}

    //public float getDist(Vector3 point1, Vector3 point2)
    //{
    //    return (point1 - point2).sqrMagnitude;
    //}
}

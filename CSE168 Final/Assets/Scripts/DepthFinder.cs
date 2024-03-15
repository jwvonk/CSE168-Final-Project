using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFinder : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject LeftController;
    public GameObject RightController;
    private HeadsetScript headsetScript;
    private ControllerScript leftControllerScript;
    private ControllerScript rightControllerScript;
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
            if (LeftController == null)
            {
                LeftController = GameObject.FindWithTag("LeftController");
            }
            leftControllerScript = GameObject.FindWithTag("LeftControllerTracker").GetComponent<ControllerScript>();
            if (RightController == null)
            {
                RightController = GameObject.FindWithTag("RightController");
            }
            rightControllerScript = GameObject.FindWithTag("RightControllerTracker").GetComponent<ControllerScript>();
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

        //Call NearestVertexTo(leftController.position, roomObj)
        if (run && leftControllerScript.ClosestObstacles != null)
        {
            var userPoints = new List<Vector3>
            {
                LeftController.transform.position,
                new Vector3(LeftController.transform.position.x, LeftController.transform.position.y / 2, LeftController.transform.position.z),
                new Vector3(LeftController.transform.position.x, 0.2f, LeftController.transform.position.z)
            };
            var nearestDist = float.MaxValue;
            foreach (var point in userPoints)
            {
                Vector3 nearestVertex = NearestVertexTo(LeftController.transform.position);
                float dist = getDist(nearestVertex, LeftController.transform.position);
                nearestDist = Mathf.Min(nearestDist, dist);
            }

            if (leftControllerScript.ClosestObstacles.Count <= leftControllerScript.maxObstacles)
            {
                leftControllerScript.ClosestObstacles.Add(Tuple.Create(gameObject, nearestDist));
                leftControllerScript.ClosestObstacles.Sort((x, y) => x.Item2.CompareTo(y.Item2));

            }
            else
            {
                if (leftControllerScript.ClosestObstacles[leftControllerScript.ClosestObstacles.Count - 1].Item2 > nearestDist)
                {
                    leftControllerScript.ClosestObstacles[leftControllerScript.ClosestObstacles.Count - 1] = Tuple.Create(gameObject, nearestDist);
                    leftControllerScript.ClosestObstacles.Sort((x, y) => x.Item2.CompareTo(y.Item2));
                }
            }

            if (nearestDist < leftControllerScript.closestDist)
            {
                leftControllerScript.closestDist = nearestDist;
                leftControllerScript.closestObj = gameObject;
            }

        }


        //Call NearestVertexTo(rightController.position, roomObj)
        if (run && rightControllerScript.ClosestObstacles != null)
        {
            var userPoints = new List<Vector3>
            {
                RightController.transform.position,
                new Vector3(RightController.transform.position.x, RightController.transform.position.y / 2, RightController.transform.position.z),
                new Vector3(RightController.transform.position.x, 0.2f, RightController.transform.position.z)
            };
            var nearestDist = float.MaxValue;
            foreach (var point in userPoints)
            {
                Vector3 nearestVertex = NearestVertexTo(RightController.transform.position);
                float dist = getDist(nearestVertex, RightController.transform.position);
                nearestDist = Mathf.Min(nearestDist, dist);
            }

            if (rightControllerScript.ClosestObstacles.Count <= rightControllerScript.maxObstacles)
            {
                rightControllerScript.ClosestObstacles.Add(Tuple.Create(gameObject, nearestDist));
                rightControllerScript.ClosestObstacles.Sort((x, y) => x.Item2.CompareTo(y.Item2));

            }
            else
            {
                if (rightControllerScript.ClosestObstacles[rightControllerScript.ClosestObstacles.Count - 1].Item2 > nearestDist)
                {
                    rightControllerScript.ClosestObstacles[rightControllerScript.ClosestObstacles.Count - 1] = Tuple.Create(gameObject, nearestDist);
                    rightControllerScript.ClosestObstacles.Sort((x, y) => x.Item2.CompareTo(y.Item2));
                }
            }

            if (nearestDist < rightControllerScript.closestDist)
            {
                rightControllerScript.closestDist = nearestDist;
                rightControllerScript.closestObj = gameObject;
            }

        }
        //Call NearestVertexTo(camera.position, roomObj)

    }

    public Vector3 NearestVertexTo(Vector3 point)
    {
        point = transform.InverseTransformPoint(point);

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

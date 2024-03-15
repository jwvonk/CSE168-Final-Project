using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFinder : MonoBehaviour
{
    private GameObject mainCamera;
    private GameObject LeftController;
    private GameObject RightController;
    private HeadsetScript headsetScript;
    private ControllerScript leftControllerScript;
    private ControllerScript rightControllerScript;
    public Collider collider;
    bool run = true;
    [SerializeField] private Material DefaultMaterial;
    [HideInInspector] public Renderer ren;

    // Start is called before the first frame update
    void Start()
    {

        if (transform.GetComponent<OVRSemanticClassification>().Contains("FLOOR") || transform.GetComponent<OVRSemanticClassification>().Contains("CEILING"))
        {
            run = false;
        } 
        else
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
            ren = GetComponentInChildren<Renderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (run && headsetScript.ClosestObstacles != null)
        {
            ren.material = DefaultMaterial;
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

        //Call NearestVertexTo(leftController.position, roomObj)
        if (run && leftControllerScript.ClosestObstacle.Item1 != null)
        {
            var userPoint = LeftController.transform.position;

            Vector3 nearestVertex = collider.ClosestPoint(LeftController.transform.position);
            float nearestDist = Vector3.Distance(nearestVertex, LeftController.transform.position);

            if (nearestDist < leftControllerScript.ClosestObstacle.Item2)
            {
                leftControllerScript.ClosestObstacle = Tuple.Create(gameObject, nearestDist);
            }

        }


        if (run && rightControllerScript.ClosestObstacle.Item1 != null)
        {
            var userPoint = RightController.transform.position;
            Vector3 nearestVertex = collider.ClosestPoint(RightController.transform.position);
            float nearestDist = Vector3.Distance(nearestVertex, RightController.transform.position);

            if (nearestDist < leftControllerScript.ClosestObstacle.Item2)
            {
                leftControllerScript.ClosestObstacle = Tuple.Create(gameObject, nearestDist);
            }

        }
    }
}

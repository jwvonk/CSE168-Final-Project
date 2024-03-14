using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFinder : MonoBehaviour
{
    public GameObject mainCamera;
    private HeadsetScript headsetScript;
    public MeshFilter mesh;
    bool run = true;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.GetComponent<OVRSemanticClassification>().Contains("FLOOR"))
        {
            run = false;
            Debug.Log("NO FLOOR");
        }
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindWithTag("MainCamera");
            headsetScript = GameObject.FindWithTag("CameraRig").GetComponent<HeadsetScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            Vector3 nearestVertex = NearestVertexTo(mainCamera.transform.position);
            float dist = getDist(nearestVertex, mainCamera.transform.position);
            if (dist < headsetScript.closestDist)
            {
                headsetScript.closestDist = dist;
                headsetScript.closestObj = gameObject;
            }
        }

        //Call NearestVertexTo(rightController.position, roomObj)
        //Call NearestVertexTo(camera.position, roomObj)

    }

    public Vector3 NearestVertexTo(Vector3 point)
    {
        point = transform.InverseTransformPoint(point);

        float minDist = float.MaxValue;

        Vector3 nearestVertex = Vector3.zero;

        foreach(Vector3 vertex in mesh.mesh.vertices) {
            float diff = getDist(point, vertex);
            if (diff < minDist)
            {
                minDist = diff;
                nearestVertex = vertex;
            }

        }

        return transform.TransformPoint(nearestVertex);
    }

    public float getDist(Vector3 point1, Vector3 point2)
    {
        return (point1 - point2).sqrMagnitude;
    }
}

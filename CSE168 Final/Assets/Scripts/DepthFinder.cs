using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFinder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Call NearestVertexTo(leftController.position, roomObj)
        //Call NearestVertexTo(rightController.position, roomObj)
        //Call NearestVertexTo(camera.position, roomObj)

        //set vibrationIntensity of both controllers to magntiude using getDist
        //set audio intensity of headset to magntiude using getDist
    }

    public Vector3 NearestVertexTo(Vector3 point, Transform obj)
    {
        point = transform.InverseTransformPoint(point);

        Mesh mesh = obj.GetComponent<MeshFilter>().mesh;
        float minDist = float.MaxValue;

        Vector3 nearestVertex = Vector3.zero;

        foreach(Vector3 vertex in mesh.vertices) {
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

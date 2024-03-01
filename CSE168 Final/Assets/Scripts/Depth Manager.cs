using System.Collections;
using System.Collections.Generic;
using Unity.XR.Oculus;
using UnityEngine;
using UnityEngine.XR;

public class NewBehaviourScript : MonoBehaviour
{
    private XRDisplaySubsystem _xrDisplay;
    private RenderTexture _depthTexture;

    // Start is called before the first frame update
    void Start()
    {
        _xrDisplay = OVRManager.GetCurrentDisplaySubsystem();

        Utils.SetupEnvironmentDepth(new Utils.EnvironmentDepthCreateParams {  removeHands = true });
        Utils.SetEnvironmentDepthRendering(true);
    }

    // Update is called once per frame
    void Update()
    {
        uint id = 0;
        Utils.GetEnvironmentDepthTextureId(ref id);

        _depthTexture = _xrDisplay.GetRenderTexture(id);

        // Create a quad and apply the depth visualization shader
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.GetComponent<Renderer>().material.mainTexture = _depthTexture;

    }
}

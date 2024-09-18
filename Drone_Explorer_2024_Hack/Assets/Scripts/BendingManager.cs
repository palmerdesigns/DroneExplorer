
//Original Source created by: https://github.com/notslot/tutorial-world-bending

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class BendingManager : MonoBehaviour
{
    public bool BendingFeature;
    private string BENDING_FEATURE = "ENABLE_BENDING";
    private string PLANET_FEATURE = "ENABLE_BENDING_PLANET";
    private static readonly int BENDING_AMOUNT =
      Shader.PropertyToID("_BendingAmount");


    [SerializeField]
    private bool enablePlanet = default;
    [SerializeField]
    [Range(0.005f, 0.1f)]
    public float bendingAmount = 0.015f;
    private float _prevAmount;

    private void Awake()
    {
        if (enablePlanet)
            Shader.EnableKeyword(PLANET_FEATURE);
        else
            Shader.DisableKeyword(PLANET_FEATURE);

        UpdateBendingAmount();
    }

    private void OnEnable()
    {
        if (!Application.isPlaying)
            return;

        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    private void Update()
    {
        //Toggle Bending Feature
        if (BendingFeature)
        {
            Shader.EnableKeyword(BENDING_FEATURE);
        }
        else
        {
            Shader.DisableKeyword(BENDING_FEATURE);
        }


        if (Math.Abs(_prevAmount - bendingAmount) > Mathf.Epsilon)
            UpdateBendingAmount();
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    private void UpdateBendingAmount()
    {
        _prevAmount = bendingAmount;
        Shader.SetGlobalFloat(BENDING_AMOUNT, bendingAmount);
    }

    private static void OnBeginCameraRendering(ScriptableRenderContext ctx,
                                                Camera cam)
    {
        cam.cullingMatrix = Matrix4x4.Ortho(-99, 99, -99, 99, 0.001f, 99) *
                            cam.worldToCameraMatrix;
    }

    private static void OnEndCameraRendering(ScriptableRenderContext ctx,
                                              Camera cam)
    {
        cam.ResetCullingMatrix();
    }
}
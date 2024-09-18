using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class BendingManagerEditor
{
    static BendingManagerEditor()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        Handles.BeginGUI();
        GUILayout.BeginArea(new Rect(10, 10, 200, 150));

        if (GUILayout.Button("Toggle Bending Feature"))
        {
            ToggleBendingFeature();
        }

        BendingManager bendingManager = Object.FindObjectOfType<BendingManager>();
        if (bendingManager != null)
        {
            float newBendingAmount = GUILayout.HorizontalSlider(bendingManager.bendingAmount, 0.005f, 0.1f);
            if (Mathf.Abs(newBendingAmount - bendingManager.bendingAmount) > Mathf.Epsilon)
            {
                bendingManager.bendingAmount = newBendingAmount;
                EditorUtility.SetDirty(bendingManager);
            }
        }
        else
        {
            GUILayout.Label("No BendingManager found in the scene.");
        }

        GUILayout.EndArea();
        Handles.EndGUI();
    }

    private static void ToggleBendingFeature()
    {
        BendingManager bendingManager = Object.FindObjectOfType<BendingManager>();
        if (bendingManager != null)
        {
            bendingManager.BendingFeature = !bendingManager.BendingFeature;
            EditorUtility.SetDirty(bendingManager);
        }
        else
        {
            Debug.LogWarning("No BendingManager found in the scene.");
        }
    }
}

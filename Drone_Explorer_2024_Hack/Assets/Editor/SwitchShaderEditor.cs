using UnityEditor;
using UnityEngine;

public class SwitchShaderEditor : EditorWindow
{
    private Shader selectedShader;

    [MenuItem("Window/Shader Changer")]
    public static void ShowWindow()
    {
        GetWindow<SwitchShaderEditor>("Shader Changer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Shader Changer", new GUIStyle(EditorStyles.boldLabel) { fontSize = 20 });
        GUILayout.Label("This tool allows you to change the shader of all selected GameObjects' materials to the shader you choose.", EditorStyles.wordWrappedLabel);
        GUILayout.Space(10); 

        selectedShader = EditorGUILayout.ObjectField("Shader", selectedShader, typeof(Shader), false) as Shader;

        GUILayout.Space(20);

        GUILayout.FlexibleSpace(); 

        if (GUILayout.Button("Change Shader", GUILayout.Height(40))) 
        {
            ChangeShader();
        }
    }

    private void ChangeShader()
    {
        if (selectedShader == null)
        {
            Debug.LogWarning("No shader selected.");
            return;
        }

        foreach (GameObject obj in Selection.gameObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                foreach (Material mat in renderer.sharedMaterials)
                {
                    mat.shader = selectedShader;
                }
            }
        }

        Debug.Log("Shader changed for selected GameObjects.");
    }
}

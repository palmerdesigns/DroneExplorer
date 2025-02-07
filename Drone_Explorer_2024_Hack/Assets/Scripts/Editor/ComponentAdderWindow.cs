using UnityEngine;
using UnityEditor;

public class ComponentAdderWindow : EditorWindow
{
    [MenuItem("Tools/Component Adder")]
    public static void ShowWindow()
    {
        GetWindow<ComponentAdderWindow>("Component Adder");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Add Components to Selected GameObjects"))
        {
            AddComponentsToSelectedGameObjects();
        }
    }

    private void AddComponentsToSelectedGameObjects()
    {
        // Get all selected GameObjects in the hierarchy  
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (GameObject obj in selectedObjects)
        {
            AutoAddComponents autoAdd = obj.GetComponent<AutoAddComponents>();
            if (autoAdd != null)
            {
                autoAdd.Awake(); // Call the Awake method to add components based on the GameObject's name  
            }
            else
            {
                Debug.LogWarning($"No AutoAddComponents script found on {obj.name}");
            }
        }
    }
}
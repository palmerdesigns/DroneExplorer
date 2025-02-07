using UnityEngine;
using System.Collections.Generic;

public class AutoAddComponents : MonoBehaviour
{
    // List to hold the name-component pairs  
    public List<NameComponentPair> componentMappings;

    public void Awake()
    {
        // Check the GameObject's name  
        AddComponentsBasedOnName(gameObject);

        // Check the names of child GameObjects  
        foreach (Transform child in transform)
        {
            AddComponentsBasedOnName(child.gameObject);
        }
    }

    private void AddComponentsBasedOnName(GameObject obj)
    {
        // Iterate through the component mappings  
        foreach (var mapping in componentMappings)
        {
            // Check if the GameObject's name matches the mapping  
            if (obj.name == mapping.gameObjectName)
            {
                // Get the component type from the string  
                System.Type componentType = GetTypeFromString(mapping.componentType);
                if (componentType != null)
                {
                    // Check if the component already exists  
                    if (obj.GetComponent(componentType) == null)
                    {
                        // Add the component  
                        obj.AddComponent(componentType);
                        Debug.Log($"Added {componentType.Name} to {obj.name}");
                    }
                }
            }
        }
    }

    private System.Type GetTypeFromString(string typeName)
    {
        // Attempt to find the type by name  
        System.Type type = System.Type.GetType(typeName);
        if (type == null)
        {
            // If the type is not found, look in the currently loaded assemblies  
            foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(typeName);
                if (type != null)
                    break;
            }
        }
        return type;
    }
}
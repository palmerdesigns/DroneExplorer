using UnityEngine;
using System.Collections.Generic;

public class AutoAddComponents : MonoBehaviour
{
    // Define a dictionary to map GameObject names to the components to add  
    private static readonly Dictionary<string, System.Type> componentMap = new Dictionary<string, System.Type>
    {
        { "Player", typeof(Rigidbody) }, // Example: Add Rigidbody to GameObjects named "Player",
        { "btn primary", typeof(BoxCollider) }
    };

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
        // Check if the GameObject's name is in the map  
        if (componentMap.TryGetValue(obj.name, out System.Type componentType))
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
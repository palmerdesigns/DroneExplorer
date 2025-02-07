using UnityEngine;

[System.Serializable]
public class NameComponentPair
{
    public string gameObjectName; // The name of the GameObject  
    public string componentType; // The type of the component as a string  

    public System.Type GetComponentType()
    {
        return System.Type.GetType(componentType);
    }
}
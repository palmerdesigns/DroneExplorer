using UnityEngine;

[System.Serializable]
public class NameComponentPair
{
    public string gameObjectName; // The name of the GameObject  
    public string componentType; // The type of the component as a string  
    public MatchType matchType; // Matching strategy  
}

public enum MatchType
{
    Contains,
    StartsWith,
    EndsWith
}
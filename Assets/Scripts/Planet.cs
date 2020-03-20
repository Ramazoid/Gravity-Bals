using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/New Planet Parameters", order = 1)]
public class Planet : ScriptableObject
{
    public string PlanetName;
    public float gravity;

    public Color SkyColor;
    public int NumberOfPlatforms;
    public float platformMaxWidth;
    public float platformMaxHeight;
}
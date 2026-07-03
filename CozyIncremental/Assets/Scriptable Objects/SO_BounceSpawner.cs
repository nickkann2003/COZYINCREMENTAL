using UnityEngine;

[CreateAssetMenu(fileName = "Bounce Spawner", menuName = "Scriptable Objects/Bounce Triggers/Spawner")]
public class SO_BounceSpawner : ScriptableObject
{
    public GameObject prefab;

    // Spawn type prefab
    public BounceSpawnerTypes spawnType;

    // Center spawn options
    public Vector3 start = Vector3.zero;

    // Radius spawn options
    public float radius = 1f;

    // Edge spawn options
    public bool horizontal;
    public bool vertical;

    // General options
    public float arcDegrees;
    public float minDistance;
    public float maxDistance;
}

public enum BounceSpawnerTypes
{
    FromCenter,
    FromRadius,
    FromEdge
}

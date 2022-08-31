using UnityEngine;

[System.Serializable]
public class SceneData
{
    [Header("Player")]
    public PlayerConfig PlayerConfig;

    [Header("Environmet")]
    public DoorConfig[] Doors;

    public FloorButtonConfig[] FloorButtons;
}

using UnityEngine;

[System.Serializable]
public class DoorConfig
{
    public int Id;
    public Transform Transform;
    public Vector3 PositionInClosedState;
    public Vector3 PositionInOpenedState;
}

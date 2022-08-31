using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DoorComponent
{
    public int Id;
    public DoorState DoorState;
    public Vector3 PositionInClosedState;
    public Vector3 PositionInOpenedState;
}

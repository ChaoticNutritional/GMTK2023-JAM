using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{

    private int GCost; // distance of node examined to start node
    private int HCost; // distance of node examined to destination node
    private int FCost; // G + H

    public bool walkable;
    public Vector3 worldPos;

    public GridNode(bool _walkable, Vector3 _worldPos)
    {
        walkable = _walkable;
        worldPos = _worldPos;
    }
}

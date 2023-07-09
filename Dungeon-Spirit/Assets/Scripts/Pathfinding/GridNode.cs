using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode : IHeapItem<GridNode>
{
    int heapIndex;
    public GridNode parent;
    public int GCost; // distance of node examined to start node
    public int HCost; // distance of node examined to destination node
    public int FCost // FCost;
    {
        get
        {
            return GCost + HCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public bool walkable;
    public Vector3 worldPos;
    public int gridX;
    public int gridY;


    public GridNode(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPos = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int CompareTo(GridNode node2Compare)
    {
        int comparison = FCost.CompareTo(node2Compare.FCost);
        if (comparison == 0)
        {
            comparison = HCost.CompareTo(node2Compare.HCost);
        }
        return -comparison;
    }
}

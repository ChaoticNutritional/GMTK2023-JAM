using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAStar : MonoBehaviour
{
    public bool displayGridGizmos;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    GridNode[,] grid;

    float nodeDiameter;
    public int gridSizeX, gridSizeY;



    void Awake()
    {


        Invoke("CreateGrid", .001f);
    }

    private void OnEnable()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();

        Debug.Log("Grid World Size: " + gridWorldSize);
        Debug.Log("Node Diameter: " + nodeDiameter);
        Debug.Log("Grid Size X: " + gridSizeX);
        Debug.Log("Grid Size Y: " + gridSizeY);
    }

    void Start()
    {

    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    void CreateGrid()
    {
        grid = new GridNode[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new GridNode(walkable, worldPoint, x, y);
            }
        }
    }

    public List<GridNode> GetNeighbors(GridNode node)
    {
        List<GridNode> neighbours = new List<GridNode>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }


    public GridNode NodeFromWorldPoint(Vector3 worldPosition)
    {
        Debug.Log("WORLD POS: " + worldPosition);
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        Debug.Log("PercentX: " + percentX);
        Debug.Log("PercentY: " + percentY);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        Debug.Log("PercentX POST CLAMP: " + percentX);
        Debug.Log("PercentY POST CLAMP: " + percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        Debug.Log("X: " + x + ", Y: " + y);

        if (x < 0 || x >= gridSizeX || y < 0 || y >= gridSizeY)
        {
            Debug.LogError("NodeFromWorldPoint: Invalid grid indices! X: " + x + ", Y: " + y);
            return null;
        }
        Debug.Log(grid == null);
        return grid[x, y];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null && displayGridGizmos)
        {
            foreach (GridNode n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}

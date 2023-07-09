using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS_HeroMovement : MonoBehaviour
{
    public TesterCube[] testerCubes;
    private float[] tileDesire;

    void Start()
    {
        int i = 0;
        foreach (TesterCube tc in GetComponentsInChildren<TesterCube>())
        {

            testerCubes[i] = tc;
            i++;
        }
    }

    public Vector3 CheckMovementDirection()
    {
        for (int i = 0; i < testerCubes.Length; i++)
        {

            if (testerCubes[i].isColliding)
            {
                tileDesire[i] -= 10000f;
            }
            RaycastHit hit;
            if (Physics.Raycast(testerCubes[i].transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("FloorTile"))
                {
                    TileInputHandler tile = hit.collider.GetComponent<TileInputHandler>();
                    switch (tile.tileState)
                    {
                        case TileInputHandler.TileState.pit:
                            tileDesire[i] -= 25f;
                            break;
                        case TileInputHandler.TileState.floor:
                            tileDesire[i] -= 50f;
                            break;
                        case TileInputHandler.TileState.raised:
                            tileDesire[i] -= 15f;
                            break;
                        case TileInputHandler.TileState.enemy:
                            tileDesire[i] -= 20f;
                            break;
                        case TileInputHandler.TileState.spike:
                            tileDesire[i] -= 10;
                            break;
                        case TileInputHandler.TileState.fountain:
                            tileDesire[i] = 30f;
                            break;
                        default:
                            break;
                    }
                }
            }
            
        }
        
        return Vector3.zero;
    }
}

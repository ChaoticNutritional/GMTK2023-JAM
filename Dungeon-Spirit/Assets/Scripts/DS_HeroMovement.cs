using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS_HeroMovement : MonoBehaviour
{
    public GameObject[] testerObjects;
    private DS_TestColliderScript[] testerScripts;
    private float[] tileDesire;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public TileInputHandler CheckMovementDirection()
    {
        for(int i = 0; i < testerObjects.Length; i++)
        {
            if(testerScripts[i].isColliding)
            {
                tileDesire[i] -= 10000f;
            }
            RaycastHit hit;
            if(Physics.Raycast(testerObjects[i].transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("FloorTile"))
                {
                    TileInputHandler tile = hit.collider.GetComponent<TileInputHandler>();
                    switch (tile.tileState)
                    {
                        case TileInputHandler.TileState.pit:
                            break;
                        case TileInputHandler.TileState.floor:
                            break;
                        case TileInputHandler.TileState.raised:
                            break;
                        case TileInputHandler.TileState.enemy:
                            break;
                        case TileInputHandler.TileState.spike:
                            break;
                        case TileInputHandler.TileState.fountain:
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        return DS_SceneManager.instance.activeTile;
    }
}

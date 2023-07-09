using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS_SpiritController : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        DS_SceneManager.instance.spiritController = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RaiseTile()
    {
        if(DS_SceneManager.instance.activeTile != null)
        {

            // If the tile is lowered, raise it to level. If level, set to raised. If raised, do nothing. 
            Debug.Log("Raise active tile");
        }
    }

    public void LowerTile()
    {
        if (DS_SceneManager.instance.activeTile != null)
        {
            // if the tile is raised, set it to level. If it is level, set it to lowered. If lowered, do nothing. 
            Debug.Log("Lower active tile");
        }
    }

    public void SpawnSlime()
    {
        if (DS_SceneManager.instance.activeTile != null)
        {
            Debug.Log("Spawn slime");
            // if the tile is level and contains 0 monsters, make it a Monster tile and add a Slime. If it does
            // contain monsters, but less than three, access Monster tile and add one Slime. Otherwise, do nothing.
        }
    }

    public void SpawnGhoul()
    {
        if (DS_SceneManager.instance.activeTile != null)
        {
            Debug.Log("Spawn ghoul");
            // if the tile is level and contains 0 monsters, make it a Monster tile and add a Ghoul. If it does
            // contain monsters, but less than three, access Monster tile and add one Ghoul. Otherwise, do nothing
        }
    }

    public void SpikePit()
    {
        if (DS_SceneManager.instance.activeTile != null)
        {
            Debug.Log("Add spikes to pit, lowering terrain to pit");
            // If the tile is raised, do nothing. If the tile is level, lower and add spikes. If the tile is lowered,
            // add spikes for 1 less AP.
        }
    }

    public void HealingFountain()
    {
        if (DS_SceneManager.instance.activeTile != null)
        {
            Debug.Log("Add healing fountain");
            // If the tile is raised, do nothing. If the tile is level, add a healing fountain. If the tile is lowered,
            // do nothing.
        }
    }
}

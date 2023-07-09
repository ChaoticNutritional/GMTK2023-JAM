/* Written by Marshall Nystrom for the GMTK 2023 Game Jam.
 * This script handles the Inventory House bar, which is 
 * enabled or disabled depending on whether or not the
 * DS_SceneManager script has a tile selected or not.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHouseScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        DS_SceneManager.instance.inventoryHouse = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);

        //if(gameObject.activeInHierarchy)
        //{
        //    DS_SceneManager.instance.activeTile.GetComponent<TileInputHandler>().DisableSelection();
        //}
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}

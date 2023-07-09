using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS_SpiritController : MonoBehaviour
{
    [Tooltip("Contains prefabs to instantiate. Floor, Raised, Pit, Enemy, Spike, Fountain")]
    public GameObject[] prefabs = new GameObject[6];

    public int AP;
    public int APGain = 4;

    public bool skipTurn;

    public CrusaderUI.Scripts.HPFlowController hPFlow;

    // Start is called before the first frame update
    void Awake()
    {

    }

    void Start()
    {
        DS_SceneManager.instance.spiritController = this;
        AP = 4;
        SetAPDisplay();
        print(AP);
    }

    // Update is called once per frame
    void Update()
    {
        if (skipTurn)
        {
            StartOfTurn();
            skipTurn = false;
        }
    }

    public void StartOfTurn()
    {
        AP += APGain;
        if (AP > APGain * 1.5)
        {
            int APMax = (int)(APGain * 1.5);
            AP = APMax;

        }
        SetAPDisplay();
        print(AP);
    }

    public void SetAPDisplay()
    {
        int APMax = (int)(APGain * 1.5);
        print(((float)AP / APMax));
        Debug.LogFormat("AP: " + AP);
        Debug.LogFormat("APMAX: " + APMax);
        Debug.Log("Is HPFlow null?: " + hPFlow == null);

        //hPFlow.SetValue(AP / APMax);
        hPFlow.SetValue(15f);
    }

    public void RaiseTile()
    {
        if (DS_SceneManager.instance.activeTile != null && AP > 0)
        {
            TileInputHandler tile = DS_SceneManager.instance.activeTile;

            if (tile.tileState == TileInputHandler.TileState.pit)
            {
                GameObject newTileGO = Instantiate(prefabs[0], tile.transform.parent.transform.position, tile.transform.parent.gameObject.transform.rotation);
                TileInputHandler newTile = newTileGO.GetComponentInChildren<TileInputHandler>();
                newTile.ReplaceSelection();
                AP--;
                SetAPDisplay();
                Debug.Log("Raise active tile");
            }
            else if (tile.tileState == TileInputHandler.TileState.floor)
            {
                GameObject newTileGO = Instantiate(prefabs[1], tile.transform.parent.gameObject.transform.position, tile.transform.parent.gameObject.transform.rotation);
                TileInputHandler newTile = newTileGO.GetComponentInChildren<TileInputHandler>();
                newTile.ReplaceSelection();
                AP--;
                SetAPDisplay();
                Debug.Log("Raise active tile");
            }
            // If the tile is lowered, raise it to level. If level, set to raised. If raised, do nothing.
        }
    }

    public void LowerTile()
    {
        if (DS_SceneManager.instance.activeTile != null && AP > 0)
        {
            TileInputHandler tile = DS_SceneManager.instance.activeTile;

            if (tile.tileState == TileInputHandler.TileState.floor)
            {
                GameObject newTileGO = Instantiate(prefabs[2], tile.transform.parent.gameObject.transform.position, tile.transform.parent.gameObject.transform.rotation);
                TileInputHandler newTile = newTileGO.GetComponentInChildren<TileInputHandler>();
                newTile.ReplaceSelection();
                AP--;
                SetAPDisplay();
                Debug.Log("Lower active tile");
            }
            else if (tile.tileState == TileInputHandler.TileState.raised)
            {
                GameObject newTileGO = Instantiate(prefabs[0], tile.transform.parent.transform.position, tile.transform.parent.gameObject.transform.rotation);
                TileInputHandler newTile = newTileGO.GetComponentInChildren<TileInputHandler>();
                newTile.ReplaceSelection();
                AP--;
                SetAPDisplay();
                Debug.Log("Lower active tile");
            }
        }
    }

    public void SpawnSlime()
    {
        if (DS_SceneManager.instance.activeTile != null && AP > 0)
        {
            TileInputHandler tile = DS_SceneManager.instance.activeTile;
            if (tile.tileState == TileInputHandler.TileState.floor)
            {
                GameObject newTileGO = Instantiate(prefabs[3], tile.transform.parent.gameObject.transform.position, tile.transform.parent.gameObject.transform.rotation);
                TileInputHandler newTile = newTileGO.GetComponentInChildren<TileInputHandler>();
                newTile.ReplaceSelection();
                newTile.AddSlime();
                AP--;
                SetAPDisplay();
                Debug.Log("Spawn Slime");
            }
            else if (tile.tileState == TileInputHandler.TileState.enemy)
            {
                if (tile.numberOfEnemies < 3)
                {
                    tile.AddSlime();
                    AP--;
                    SetAPDisplay();
                    Debug.Log("Spawn Slime");
                }
            }
            // if the tile is level and contains 0 monsters, make it a Monster tile and add a Slime. If it does
            // contain monsters, but less than three, access Monster tile and add one Slime. Otherwise, do nothing.
        }
    }

    public void SpawnGhoul()
    {
        if (DS_SceneManager.instance.activeTile != null && AP >= 3)
        {
            TileInputHandler tile = DS_SceneManager.instance.activeTile;
            if (tile.tileState == TileInputHandler.TileState.floor)
            {
                GameObject newTileGO = Instantiate(prefabs[3], tile.transform.parent.gameObject.transform.position, tile.transform.parent.gameObject.transform.rotation);
                TileInputHandler newTile = newTileGO.GetComponentInChildren<TileInputHandler>();
                newTile.ReplaceSelection();
                newTile.AddGhoul();
                AP -= 3;
                SetAPDisplay();
                Debug.Log("Spawn Ghoul");
            }
            else if (tile.tileState == TileInputHandler.TileState.enemy)
            {
                if (tile.numberOfEnemies < 3)
                {
                    tile.AddGhoul();
                    AP -= 3;
                    SetAPDisplay();
                    Debug.Log("Spawn Ghoul");
                }
            }
            // if the tile is level and contains 0 monsters, make it a Monster tile and add a Slime. If it does
            // contain monsters, but less than three, access Monster tile and add one Slime. Otherwise, do nothing.
        }
    }

    public void SpikePit()
    {
        TileInputHandler tile = DS_SceneManager.instance.activeTile;
        if (tile.tileState == TileInputHandler.TileState.floor && AP >= 2)
        {
            GameObject newTileGO = Instantiate(prefabs[4], tile.transform.parent.gameObject.transform.position, tile.transform.parent.gameObject.transform.rotation);
            TileInputHandler newTile = newTileGO.GetComponentInChildren<TileInputHandler>();
            newTile.ReplaceSelection();
            AP -= 2;
            SetAPDisplay();
        }
        else if (tile.tileState == TileInputHandler.TileState.pit && AP >= 1)
        {
            GameObject newTileGO = Instantiate(prefabs[4], tile.transform.parent.gameObject.transform.position, tile.transform.parent.gameObject.transform.rotation);
            TileInputHandler newTile = newTileGO.GetComponentInChildren<TileInputHandler>();
            newTile.ReplaceSelection();
            AP--;
            SetAPDisplay();
        }
    }

    public void HealingFountain()
    {
        TileInputHandler tile = DS_SceneManager.instance.activeTile;
        if (tile.tileState == TileInputHandler.TileState.floor && AP >= 2)
        {
            GameObject newTileGO = Instantiate(prefabs[5], tile.transform.parent.gameObject.transform.position, tile.transform.parent.gameObject.transform.rotation);
            TileInputHandler newTile = newTileGO.GetComponentInChildren<TileInputHandler>();
            newTile.ReplaceSelection();
            AP -= 2;
            SetAPDisplay();
        }
    }
}

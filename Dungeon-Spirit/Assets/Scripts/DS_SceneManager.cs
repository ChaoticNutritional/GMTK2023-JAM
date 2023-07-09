using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS_SceneManager : MonoBehaviour
{
    public static DS_SceneManager instance;

    public GameObject hero;
    public CameraController cameraObject;
    public TileInputHandler activeTile;
    public InventoryHouseScript inventoryHouse;
    public PauseScreenMenu pauseScreenMenu;
    public DS_SpiritController spiritController;

    public bool spiritTurn;
    public int turnNumber;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdvanceRound()
    {
        if (spiritTurn == true)
        {
            spiritTurn = false;
            // Hero spends actions
        }

        else
        {
            turnNumber++;
        }
    }
}

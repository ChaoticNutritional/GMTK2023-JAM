using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileInputHandler : MonoBehaviour, IMouseable
{
    // Start is called before the first frame update

    // reference the camera controller to check if isSelected is true;

    // Start is called before the first frame update

    // reference the camera controller to check if isSelected is true;

    // HOVER MATERIA
    public AbilityBar _abilityBar;
<<<<<<< HEAD
<<<<<<< Updated upstream

    public TileState tileState;
    public float tileDesire;
    public bool heroHasSteppedOn;
    public int numberOfEnemies;
    public List<GameObject> enemies = new List<GameObject>();
>>>>>>> Stashed changes
=======
>>>>>>> parent of 8a907f2a (Added Collision Tester Children to hero and added additional behavior and movement functionality)

    private Material myMat;
    private Color originalColor;
    private bool touched = false;

<<<<<<< HEAD
<<<<<<< Updated upstream

    public GameObject exitDoor;

    public enum TileState
    {
        floor = 0,
        raised = 1,
        pit = 2,
        enemy = 3,
        spike = 4,
        fountain = 5,
        exit = 6
    }

>>>>>>> Stashed changes
=======
>>>>>>> parent of 8a907f2a (Added Collision Tester Children to hero and added additional behavior and movement functionality)
    void Awake()
    {
        myMat = this.GetComponent<MeshRenderer>().material;
        originalColor = myMat.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        myMat.color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myMat.color = originalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _abilityBar.CreateBar();
    }


}




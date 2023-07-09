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
<<<<<<< Updated upstream

    public TileState tileState;
    public float tileDesire;
    public bool heroHasSteppedOn;
    public int numberOfEnemies;
    public List<GameObject> enemies = new List<GameObject>();
>>>>>>> Stashed changes

    private Material myMat;
    private Color originalColor;
    private bool touched = false;

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
    void Awake()
    {
        exitDoor = GameObject.Find("DS_ExitHatch");
        tileDesire += 10 * Mathf.Clamp01(Vector3.Distance(this.transform.position, exitDoor.transform.position));
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
<<<<<<< Updated upstream
        _abilityBar.CreateBar();
=======
        // show ability bar
        // show abilities

        // call out to scene manager with reference to self

        // have scene manager to subscribe 
        //_abilityBar.enabled = true;

        //_abilityBar.CreateBar(this.gameObject);
        if(selected)
        {
            DisableSelection();
            myMat.color = Color.green;
        }
        else
        {
            EnableSelection();
        }
>>>>>>> Stashed changes
    }


}




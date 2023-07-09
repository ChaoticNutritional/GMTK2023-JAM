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



    private bool selected;
    public AbilityBar _abilityBar;

    // HOVER MATERIAL
    private Material myMat;
    private Color originalColor;
    private bool touched = false;

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

    void Awake()
    {
        myMat = this.GetComponent<MeshRenderer>().material;
        originalColor = myMat.color;
        selected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selected == false)
        {
            myMat.color = Color.green;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selected == false)
        {
            myMat.color = originalColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
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
    }

    public void EnableSelection()
    {
        selected = true;
        if(DS_SceneManager.instance.activeTile != null)
        {
            DS_SceneManager.instance.activeTile.GetComponent<TileInputHandler>().DisableSelection();
        }
        DS_SceneManager.instance.activeTile = this;
        myMat.color = Color.cyan;
    }

    public void DisableSelection()
    {
        selected = false;
        DS_SceneManager.instance.activeTile = null;
        myMat.color = originalColor;
    }
}




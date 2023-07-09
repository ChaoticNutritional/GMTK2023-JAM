using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityBar : MonoBehaviour
{
    [SerializeField] private AbilitySlot _abilitySlotPrefab;

    public AbilitySlot[] _abilityArray = new AbilitySlot[6];

    public CameraController _cameraController;

    protected GameObject myTile;

    public void CreateBar(GameObject referencedTile)
    {
        Debug.Log("hello");

        for(int i = 0; i < 6; i++)
        {
            _abilityArray[i]._tile = referencedTile;
        }
    }


    void OnDisable()
    {
        DestroyBar();
    }

    private void DestroyBar()
    {
        this.enabled = false;
    }
}

// button for each ability
// when clicked, spawn at referenced tile
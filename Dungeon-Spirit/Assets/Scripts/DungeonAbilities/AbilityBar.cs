using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityBar : MonoBehaviour, IMouseable
{
    [SerializeField] public BaseAbility _ability;

    [SerializeField] private AbilitySlot _abilitySlotPrefab;

    [SerializeField] private int _abilityCount = 6;

    private GameObject barHolder;

    private AbilitySlot[] _abilityArray;

    void OnDisable()
    {
        DestroyBar();
    }

    private void DestroyBar()
    {
        Destroy(barHolder);
    }

    public void CreateBar()
    {
        barHolder = new GameObject("barHolder");
        // Preallocate slots array memory
        _abilityArray = new AbilitySlot[_abilityCount];

        for (int i = 0; i < +_abilityCount; i++)
        {
            AbilitySlot thisAbilitySlot = Instantiate(_abilitySlotPrefab, transform);
            _abilityArray[i] = thisAbilitySlot;
            thisAbilitySlot.transform.parent = barHolder.transform;
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // raise ability
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // lower ability back down
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBar : MonoBehaviour
{
    [SerializeField] private AbilitySlot _abilitySlotPrefab;

    [SerializeField] private int _abilityCount = 6;

    private AbilitySlot[] _abilityArray;

    private void Awake()
    {
        CreateBar();
    }

    public void CreateBar()
    {
        // Preallocate slots array memory
        _abilityArray = new AbilitySlot[_abilityCount];

        for (int i = 0; i < +_abilityCount; i++) 
        {
            AbilitySlot thisAbilitySlot = Instantiate(_abilitySlotPrefab, transform);
            _abilityArray[i] = thisAbilitySlot;
        }
    }
}
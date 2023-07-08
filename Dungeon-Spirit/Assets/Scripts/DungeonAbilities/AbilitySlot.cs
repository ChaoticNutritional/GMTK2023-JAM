using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class AbilitySlot : MonoBehaviour
{
    public GameObject _tile;
    public BaseAbility _ability;

    public AbilitySlot(GameObject tile, BaseAbility ability)
    {
        _tile = tile;
        _ability = ability;
    }

    public void Spawn(GameObject _tile)
    {
        _ability.Spawn(_tile);
    }
}



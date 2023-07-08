using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    [SerializeField] protected float _health;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _xpReward;
    [SerializeField] protected GameObject _target;

    // Wait for hero to enter tile
        // TODO
        // Have a reference to a dungeon tile that it's on
            // so that we may check if a hero entered it
        
        // Have that dungeonTile pass reference of the player to the monster

    // When hero enter tile, wait for hero to attack
        // attack

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

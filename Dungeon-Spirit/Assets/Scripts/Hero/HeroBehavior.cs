using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float HP = 40f;

    public float armor = 0f;

    // how many squares cost one action point
    public int movement = 1;

    public float baseDmg = 7f;

    public Vector2 bonusDmg = new Vector2(0f, 3f); // on turn set this to Range(0,3);

    public float healBonus = 0f;

    public int actionPoints = 4;

    public float exp = 0;

    public int currentLvl = 1;

    public float expToLevel;

    public float staticHealthOnLvl = 5;
    public float staticDmgOnLvl = 0.5f;
    public Vector2 staticBonusDmgOnLvl = new Vector2(0f, 1f);


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGainXP(float expGained)
    {
        // when we gain XP,
        exp += expGained;
        if (exp >= currentLvl * 3)
        {
            currentLvl++;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        
    }

    public void DoAttack() // pass in enemy target
    {
        // TODO
        // 

    }
}

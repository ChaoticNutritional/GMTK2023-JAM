using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehavior : MonoBehaviour
{
    // Script Reference Variables
    public DS_HeroMovement heroMoveScript;
    public Animator anim;
    private Vector3 originalPosition;
    public TileInputHandler tile;

    // BASE HERO VALUES
    public float _maxHP = 40f;
    public float _currentHP; // set to max on start
    public float _armor = 0f;
    // how many squares cost one action point
    public float _movement = 1;
    public float _currentMovePoints;
    public float _baseDmg = 7f;
    public Vector2 _bonusDmg = new Vector2(0f, 3f); // on turn set this to Range(0,3);
    public float _healBonus = 0f;
    public int _actionPoints = 4;
    public int _currentAP = 0;
    public float _exp = 0;
    public int _currentLvl = 1;
    public float _expToLevel;

    // STATIC ON LEVEL UP VALUES
    public float staticHealthOnLvl = 5;
    public float staticDmgOnLvl = 0.5f;
    public Vector2 staticBonusDmgOnLvl = new Vector2(0f, 1f);
    public int actionPointIncreaseOnLvl; // Math.Max( (1/3 * currentLvl), 6);

    // DYNAMIC ON LVL UP VALUES

    public enum DynamicLvlBonus
    {
        dynamicHealthOnLvl = 0,
        dynamicArmorOnLvl = 1,
        dynamicDmgBonusOnLvl = 2,
        dynamicMoveBonusOnLvl = 3,
        dynaimcBonusDmgOnLvl = 4,
        dynamicHealBonusOnLvl = 5
    }



    void Start()
    {
        DS_SceneManager.instance.hero = this;
        anim = transform.GetChild(0).GetComponent<Animator>();
        heroMoveScript = GetComponent<DS_HeroMovement>();
        _currentHP = _maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (DS_SceneManager.instance.spiritTurn == false && _currentAP != 0)
        {
            if (tile.tileState == TileInputHandler.TileState.enemy)
            {
                DoAttack();
            }
            else if (tile.tileState == TileInputHandler.TileState.fountain && _currentHP < _maxHP)
            {
                DoDrinkFountain();
            }
            else if (_currentMovePoints >= 1)
                Move();
            else
                DoMove();
        }
    }

    public void ProcessStartOfTurn()
    {
        _currentAP = _actionPoints;
    }

    public void OnGainXP(float expGained)
    {
        // when we gain XP,
        _exp += expGained;
        if (_exp >= _currentLvl * 3)
        {
            _currentLvl++;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        for (int i = 0; i < 4; i++)
        {
            switch (RollRandom())
            {
                case DynamicLvlBonus.dynamicHealthOnLvl:
                    _maxHP += 10f;
                    break;
                case DynamicLvlBonus.dynamicArmorOnLvl:
                    _armor += 0.5f;
                    break;
                case DynamicLvlBonus.dynamicDmgBonusOnLvl:
                    _baseDmg += 0.5f;
                    break;
                case DynamicLvlBonus.dynamicMoveBonusOnLvl:
                    _movement += 0.5f;
                    break;
                case DynamicLvlBonus.dynaimcBonusDmgOnLvl:
                    _bonusDmg += new Vector2(0f, 2f);
                    break;
                case DynamicLvlBonus.dynamicHealBonusOnLvl:
                    _healBonus += 5f;
                    break;
                default:
                    break;
            }

        }
    }

    public void DoAttack() // pass in enemy target
    {


        // TODO
        // if we step in a space occupied by enemies
        // if have action points
        // attack til Hero Dies
    }

    public void DoDrinkFountain()
    {
        _currentHP += _maxHP * 0.5f;
        _currentHP += _healBonus;
        if (_currentHP > _maxHP)
            _currentHP = _maxHP;

        tile.FountainDrunk();

        _currentAP--;
        // TODO
        // on enter
        // if action point available && missing health
        // spend action point to drink
        // _health += (_maxHP * 5) + _healBonus
    }

    public void DoMove()
    {
        _currentMovePoints += _movement;
        _currentAP--;
    }

    public void HandleDeath()
    {
        // play death anim
        // destroy gameobject
    }

    public void TakeDmg(float dmgAmt)
    {
        _currentHP -= dmgAmt;

        if(_currentHP <= 0)
        {
            HandleDeath();
        }
    }

    public void OnKill() // pass in enemy target
    {
        // _exp += enemy.XPvalue
        
        // if (have more action points)
        // keep moving
    }

    public void Move()
    {
        //heroMove.CheckMovementDirection();
        StartCoroutine("LerpToPosition", heroMoveScript.CheckMovementDirection());
        //A* hooboy
    }

    public IEnumerable LerpToPosition(GameObject destObj)
    {
        float i = 0;
        originalPosition = transform.position;
        transform.forward = new Vector3((destObj.transform.position - transform.position).x, 0, (destObj.transform.position - transform.position).z);
        anim.Play("1H@Run01 - Forward");
        while (i < 1f)
        {
            transform.position = Vector3.Lerp(originalPosition, destObj.transform.position, i);
            yield return null;
        }
        anim.Play("Item-Idle");
        tile = destObj.GetComponent<TileInputHandler>();
        tile.heroHasSteppedOn++;
        yield break;
    }

    private DynamicLvlBonus RollRandom()
    {
        return (DynamicLvlBonus)Random.Range(0, 5);
    }
}

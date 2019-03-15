using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat
{
    private float strength;
    private float agility;
    private float constitution;

    private float armorStat = 0.0f;

    private float hp;

    private float minDamage;
    private float maxDamage;

    private int level = 1;
    private float requiredEXP = 10.0f;
    private float exp = 0.0f;

    private int money = 0;

    private int point = 0;

    public CharacterStat(float str, float agi, float con)
    {
        strength = str;
        agility = agi;
        constitution = con;
        hp = 5 * constitution;
        minDamage = strength / 2;
        maxDamage = minDamage + 4;
    }

    public float HitChance(float targetAGI)
    {
        return (75.0f + agility - targetAGI);
    }

    public float FinalDamage(float targetArmor)
    {
        return (BasicDamage * (1 - targetArmor / 200));
    }

    public void EnemyLevelUp()
    {
        level += 1;
        strength *= 1.15f;
        agility *= 1.15f;
        constitution *= 1.15f;
    }

    public void LevelUP()
    {
        point += 5;
        level++;
        exp -= requiredEXP;
        requiredEXP += requiredEXP * 1.5f;
    }

    // getter and setter
    public float Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    public float Agility
    {
        get { return agility; }
        set { agility = value; }
    }

    public float Constitution
    {
        get { return constitution; }
        set { constitution = value; }
    }

    public float ArmorStat
    {
        get { return armorStat; }
        set {
            armorStat = value;
        }
    }

    public float HP
    {
        get { return hp; }
    }

    public float MinDamage
    {
        get { return minDamage; }
    }

    public float MaxDamage
    {
        get { return maxDamage; }
    }

    public int Level
    {
        get { return level; }
    }

    public float EXP
    {
        get { return exp; }
        set { exp += value; }
    }

    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    public float BasicDamage
    {
        get { return (Random.Range(minDamage, maxDamage)); }
    }

    public float RequiredEXP
    {
        get { return (requiredEXP); }
    }

    public int Point
    {
        get { return (point); }
        set { point = value; }
    }
}
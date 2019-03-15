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
    private float exp = 0.0f;

    private int money = 0;

    public CharacterStat(float str, float agi, float con)
    {
        strength = str;
        agility = agi;
        constitution = con;
        hp = 5 * constitution;
        minDamage = strength / 2;
        maxDamage = minDamage + 4;
    }


    // getter and setter
    public float Strength
    {
        get { return strength; }
    }

    public float Agility
    {
        get { return Agility; }
    }

    public float Constitution
    {
        get { return constitution; }
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
        set { exp = value; }
    }

    public int Money
    {
        get { return money; }
        set { money = value; }
    }
}
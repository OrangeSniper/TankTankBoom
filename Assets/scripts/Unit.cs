using System.Collections;
using System;
using UnityEngine;

public enum tankTeam { blue, red };

[System.Serializable]
public class Unit
{
    public tankTeam team;

    public float attack; //How much damage you do
    public float defense; //Maximum health
    public float support; //how much health you regain passively

    public float versatillity; //speed
    public int ralley; //how much attack other players gain when around you
    public int shield; //how much all players get as an added sheild on their HP. uses the heighest players.

    public int HP;

    public float speed;

    public float timeUntilHeal;
    public float timeLeftUntilHeal;

    public void Damage(int Damage)
    {
        HP -= Damage;
        timeLeftUntilHeal = timeUntilHeal;
    }

    public void InitStats()
    {
        HP = (int)defense;
    }

    public void UpdateStats()
    {
        versatillity = (attack + defense) / 2;
        ralley = (int)(attack + support) / 2;
        shield = (int)(defense + support) / 2;
        speed = versatillity / 30;
    }
}
using System.Collections;
using System;
using UnityEngine;

public enum tankTeam { blue, red };

[System.Serializable]
public class Unit
{
    public tankTeam team;

    public int attack; //How much damage you do
    public int defense; //Maximum health
    public int support; //how much health you regain passively

    public int versatillity; //speed
    public int ralley; //how much attack other players gain when around you
    public int shield; //how much all players get as an added sheild on their HP. uses the heighest players.

    public int HP;

    public int speed;

    public void Damage(int Damage)
    {
        HP -= Damage;
    }

    public void InitStats()
    {
        HP = defense;
    }

    public void UpdateStats()
    {
        versatillity = (attack + defense) / 2;
        ralley = (attack + support) / 2;
        shield = (defense + support) / 2;
        speed = versatillity * 1000000;
    }
}
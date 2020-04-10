using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour
{
    private Transform[] enemies;
    public GameObject masterEnemy;
    [SerializeField] private Window_Questpointer enemypointer;
    public float EnemyleastDist;
    public int closestEnemy;

    public Transform player;

    [SerializeField] private Window_Questpointer LootPointer;
    public GameObject masterLoot;
    private Transform[] loot;
    public float lootLeastDist;
    public int closestLoot;


    private void Update()
    {
        EnemyPoint();
        LootPoint();
    }

    void EnemyPoint()
    {
        enemies = masterEnemy.GetComponentsInChildren<Transform>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (i == 1)
            {
                EnemyleastDist = Vector2.Distance(player.position, enemies[1].position);
                closestEnemy = i;
            }
            else if (enemies.Length <= 1)
            {
                return;
            }
            else if (Vector2.Distance(player.position, enemies[i].position) < EnemyleastDist)
            {
                EnemyleastDist = Vector2.Distance(player.position, enemies[i].position);
                closestEnemy = i;
            }
        }
        enemypointer.show(enemies[closestEnemy].position);
        if (enemies.Length == 1)
        {
            enemypointer.hide();
        }
    }

    void LootPoint()
    {
        loot = masterLoot.GetComponentsInChildren<Transform>();
        for(int i = 0; i < loot.Length; i++)
        {
            if(i == 1)
            {
                lootLeastDist = Vector2.Distance(player.position, loot[1].position);
                closestLoot = i;
            }else if(loot.Length <= 1)
            {
                return;
            }else if(Vector2.Distance(player.position, loot[i].position) < lootLeastDist)
            {
                lootLeastDist = Vector2.Distance(player.position, loot[i].position);
                closestLoot = i;
            }
        }
        LootPointer.show(loot[closestLoot].position);
        if (loot.Length == 1)
        {
            LootPointer.hide();
        }
    }
}

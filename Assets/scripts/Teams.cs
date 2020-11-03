using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams : MonoBehaviour
{
    public GameObject[] tanks;
    public GameObject[] red;
    public GameObject[] blue;

    AllTanks[] them;


    private void Awake()
    {
        UpdateTanks();
    }

    private void Update()
    {
        UpdateTanks();
    }

    private void UpdateTanks()
    {
        them = FindObjectsOfType<AllTanks>();

        for (int i = 0; i < them.Length; i++)
        {
            tanks[i] = them[i].gameObject;
        }
        for(int i = 0; i < tanks.Length; i++)
        {
            if(tanks[i].GetComponent<BulletGo>() != null)
            {
                if (tanks[i].GetComponent<BulletGo>().unitInfo.team == tankTeam.red)
                {
                    red[i] = tanks[i];
                }
                if (tanks[i].GetComponent<BulletGo>().unitInfo.team == tankTeam.blue)
                {
                    blue[i] = tanks[i];
                }
            }
            if(tanks[i].GetComponent<TankAI>() != null)
            {
                if (tanks[i].GetComponent<TankAI>().unitinfo.team == tankTeam.red)
                {
                    red[i] = tanks[i];
                }
                if (tanks[i].GetComponent<TankAI>().unitinfo.team == tankTeam.blue)
                {
                    blue[i] = tanks[i];
                }
            }
        }
    }

}

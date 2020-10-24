using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams : MonoBehaviour
{
    public GameObject[] tanks;
    public GameObject[] red;
    public GameObject[] blue;

    private void Update()
    {
        for(int i = 0; i < tanks.Length; i++)
        {
            if(tanks[i].GetComponent<BulletGo>().unitInfo.team == tankTeam.red)
            {
                red[i] = tanks[i];
            }
            if(tanks[i].GetComponent<TankAI>().unitinfo.team == tankTeam.red) 
            {
                red[i] = tanks[i];
            }
            if(tanks[i].GetComponent<BulletGo>().unitInfo.team == tankTeam.blue)
            {
                blue[i] = tanks[i];
            }
            if (tanks[i].GetComponent<TankAI>().unitinfo.team == tankTeam.blue)
            {
                blue[i] = tanks[i];
            }
        }
    }
}

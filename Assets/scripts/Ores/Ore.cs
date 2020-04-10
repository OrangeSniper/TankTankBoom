using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public float money;
    public int hp;
    public Dissolve dissolve;

    public float mine(int damage)
    {
        hp -= damage;
        if(hp <= 0f)
        {
            dissolve.isDissolving = true;
            if(dissolve.isDissolving == true)
            {
                return 0;
            }
            gameObject.SetActive(false);
            return money;
        }else
        {
            return 0;
        }
    }
 
}

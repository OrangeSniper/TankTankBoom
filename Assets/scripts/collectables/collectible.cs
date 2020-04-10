using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectible : MonoBehaviour
{
    public int ammo;
    public int health;


    private void Awake()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<BulletGo>().AddAmmo(ammo);
        gameObject.SetActive(false);
    }
}

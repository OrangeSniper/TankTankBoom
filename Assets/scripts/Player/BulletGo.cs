using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGo : MonoBehaviour
{
    public Transform fp;
    public GameObject bullet;
    public GameObject deathEffect;

    public float bulletforce = 20f;

    private bool ableToShoot = true;
    public float shootTime = .01f;

    public int ammo;

    public int HP = 10;

    // Update is called once per frame
    void Update()
    {
        if( ableToShoot == true &&  Input.GetKey(KeyCode.Space) && ammo >0)
        {
            Shoot();
        }
        StartCoroutine(ShootTime());
        if(HP <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(deathEffect, 5f);
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        GameObject boolet =  Instantiate(bullet, fp.position, fp.rotation);
        Rigidbody2D rb =  boolet.GetComponent<Rigidbody2D>();
        rb.AddForce(fp.up * bulletforce, ForceMode2D.Impulse);
        ammo -= 1;
    }

    IEnumerator ShootTime()
    {
        if(ableToShoot == true && Input.GetKey(KeyCode.Space))
        {
            ableToShoot = false;
            yield return new WaitForSeconds(shootTime);
            ableToShoot = true;
        }
    }

    public void AddAmmo(int addedAmmo)
    {
        ammo += addedAmmo;
    }

    public void Damage(int damage)
    {
        HP -= damage;
    }
}

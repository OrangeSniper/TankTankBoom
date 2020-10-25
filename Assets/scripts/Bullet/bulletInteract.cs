using UnityEngine;
using Cinemachine;

public class bulletInteract : MonoBehaviour
{
    public GameObject hitEffect;

    public tankTeam team;

    public int damage;

    

    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        if(collision.collider.GetComponent<TankAI>() != null)
        {
            if (collision.collider.GetComponent<TankAI>().unitinfo.team != team)
            {
                collision.collider.GetComponent<EnemyHP>().Damage(damage);
                collision.collider.GetComponent<TankAI>().unitinfo.timeLeftUntilHeal = collision.collider.GetComponent<TankAI>().unitinfo.timeUntilHeal;
            }
        }
        if(collision.collider.GetComponent<BulletGo>() != null)
        {
            if (collision.collider.GetComponent<BulletGo>().unitInfo.team != team)
            {
                collision.collider.GetComponent<BulletGo>().unitInfo.HP -= damage;
                collision.collider.GetComponent<BulletGo>().unitInfo.timeLeftUntilHeal = collision.collider.GetComponent<BulletGo>().unitInfo.timeUntilHeal;
            }
        }
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
using UnityEngine;

public class EnemyBulletInteract : MonoBehaviour
{
    public GameObject hitEffect;

    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<BulletGo>().Damage(damage);
        }
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
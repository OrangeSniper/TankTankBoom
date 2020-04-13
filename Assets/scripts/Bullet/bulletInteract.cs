using UnityEngine;

public class bulletInteract : MonoBehaviour
{
    public GameObject hitEffect;

    public int damage;

    private PlayerMov player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMov>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        if (collision.collider.CompareTag("enemy"))
        {
            collision.collider.GetComponent<EnemyHP>().Damage(damage);
        }
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
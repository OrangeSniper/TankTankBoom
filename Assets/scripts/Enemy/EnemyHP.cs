using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int hp;
    public int damage = 1;
    public float wait;

    // Update is called once per frame
    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(int damage)
    {
        hp -= damage;
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<BulletGo>().unitInfo.Damage(damage);
            yield return new WaitForSeconds(wait);
        }
    }
}
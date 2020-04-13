using UnityEngine;

public class TankShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject firePoint;
    private float bulletforce = 20f;

    public void Fire()
    {
        GameObject boolet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D rb = boolet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.transform.up * bulletforce, ForceMode2D.Impulse);
    }
}
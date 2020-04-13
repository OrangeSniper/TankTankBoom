using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    private float currentSPD;
    public Rigidbody2D rb;
    public Vector2 move;
    public Camera cam;
    private Vector2 mousePos;
    public float angRad;
    private BulletGo player;

    public float money;

    public int maxEnemies;
    public int enemies;

    private void Awake()
    {
        maxEnemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        player = GetComponent<BulletGo>();
    }

    // Update is called once per frame
    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        angRad = rb.rotation * Mathf.Deg2Rad;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSPD = -player.unitInfo.speed;
        }
        else
        {
            currentSPD = player.unitInfo.speed;
        }
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        move = AngMag(angRad, currentSPD);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            rb.MovePosition(move + rb.position);
        }
    }

    public Vector2 AngMag(float angle, float magnitude)
    {
        float x;
        float y;
        y = Mathf.Sin(angle) / magnitude;
        x = Mathf.Cos(angle) / magnitude;
        return new Vector2(x, y);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float moveSPD = 5;
    private float currentSPD;
    public Rigidbody2D rb;
    public Vector2 move;
    public Camera cam;
    Vector2 mousePos;
    public float angRad;


    public float money;

    public int maxEnemies;
    public int enemies;

    private void Start()
    {
        maxEnemies = GameObject.FindGameObjectsWithTag("enemy").Length;
    }



    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        angRad = rb.rotation * Mathf.Deg2Rad;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentSPD = -moveSPD;
        }else
        {
            currentSPD = moveSPD;
        }
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        move = AngMag(angRad, currentSPD);
        if(Input.GetKey(KeyCode.Mouse0))
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

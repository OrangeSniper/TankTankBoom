using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramov : MonoBehaviour
{
    public Transform player;
    public float time;

    private void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.position.x, time), Mathf.Lerp(transform.position.y, player.position.y, time), -10);
    }
}

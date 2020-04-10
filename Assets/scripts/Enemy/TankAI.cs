using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TankAI : MonoBehaviour
{

    public GameObject target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    Path path;
    int currentWayPoint = 0;
    bool reachedEnd = false;

    Seeker seeker;
    Rigidbody2D rb;

    public float dist;
    public float seekDis;

    public float rotationSPD;

    public bool targetInRange = false;
    public bool nullifier = false;
    public bool hasBeenInRange = false;
    public bool seek;

    private AudioSource source;
    public bool sourcePlayed = false;

    public TankShoot shoot;

    public float fireSPD = 5f;

    public float radius = 20f;
    public float firingRadius = 5f;

    public bool isFiring = false;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        dist = Vector2.Distance(transform.position, target.transform.position);
        targetInRange = dist <= seekDis;
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (targetInRange || target == null)
            {
                if (reachedEnd)
                {
                    path = null;
                }
                seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
                if (!sourcePlayed)
                {
                    source.Play();
                    sourcePlayed = true;
                }

            }
            else if (nullifier == false)
            {
                seeker.StartPath(rb.position, PickRandomPoint(), OnPathComplete);
                nullifier = true;
            }
            if (reachedEnd)
            {
                nullifier = false;
            }
            if (!targetInRange)
            {
                sourcePlayed = false;
            }

        }
    }

    Vector2 PickRandomPoint()
    {
        var point = UnityEngine.Random.insideUnitCircle * radius;
        point += rb.position;
        return point;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Chase();
    }

    void Chase()
    {
        if (path == null)
        {
            return;
        }
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }
        else
        {
            reachedEnd = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.fixedDeltaTime;
        Vector2 dir = (rb.position - (Vector2)path.vectorPath[currentWayPoint]);
        Vector2 dir2 = (rb.position - (Vector2)target.transform.position);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (dist > firingRadius)
        {
            rb.AddForce(force);
        }else
        {
            rb.rotation = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
            if(!isFiring)
            {
                StartCoroutine(fire());
            }
        }



        if (distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }
        if(dist > firingRadius)
        {
            rb.rotation = Mathf.Lerp(rb.rotation, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, rotationSPD);
        }
    }

    IEnumerator fire()
    {


        shoot.Fire();
        isFiring = true;


        yield return new WaitForSeconds(fireSPD);
        isFiring = false;
    }
}

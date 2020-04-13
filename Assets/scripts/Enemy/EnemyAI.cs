using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEnd = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    public float dist;
    public float seekDis;

    public float rotationSPD;

    public bool targetInRange = false;
    public bool nullifier = false;
    public bool hasBeenInRange = false;
    public bool seek;

    private AudioSource source;
    public bool sourcePlayed = false;

    public float radius = 20f;

    // Start is called before the first frame update
    private void Start()
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

    private void UpdatePath()
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

    private Vector2 PickRandomPoint()
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
    private void FixedUpdate()
    {
        Chase();
    }

    private void Chase()
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
        Vector2 force = direction * speed * Time.deltaTime;
        Vector2 dir = rb.position - (Vector2)path.vectorPath[currentWayPoint];

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }
        rb.rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}
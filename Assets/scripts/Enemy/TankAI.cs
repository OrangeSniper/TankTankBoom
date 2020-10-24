using Pathfinding;
using System.Collections;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    public Teams teams;
    public GameObject target;

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

    public TankShoot shoot;

    public float fireSPD = 5f;

    public float radius = 20f;
    public float firingRadius = 5f;

    public bool isFiring = false;

    public Unit unitinfo;

    // Start is called before the first frame update
    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        source = GetComponent<AudioSource>();
        unitinfo.InitStats();
        target = NearTarg(unitinfo.team);

    }

    public void Update()
    {
        target = NearTarg(unitinfo.team);
        dist = Vector2.Distance(transform.position, target.transform.position);
        targetInRange = dist <= seekDis;
        unitinfo.UpdateStats();
    }

    public GameObject NearTarg(tankTeam team)
    {
        GameObject currentClosest = null;
        if(team == tankTeam.red)
        {
            for(int i = 0; i < teams.blue.Length; i++)
            {
                if (i == 0)
                {
                    currentClosest = teams.blue[i];
                }else if (Vector2.Distance(teams.blue[i].transform.position, transform.position) < Vector2.Distance(teams.blue[i-1].transform.position, transform.position))
                {
                    currentClosest = teams.blue[i];
                }
            }
        }
        if (team == tankTeam.blue)
        {
            for (int i = 0; i < teams.red.Length; i++)
            {
                if (i == 0)
                {
                    currentClosest = teams.red[i];
                }else if (Vector2.Distance(teams.red[i].transform.position, transform.position) < Vector2.Distance(teams.red[i-1].transform.position, transform.position))
                {
                    currentClosest = teams.red[i];
                }
            }
        }
        return currentClosest;
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
        Vector2 force = direction * unitinfo.speed * Time.deltaTime * 10;
        Vector2 dir = (rb.position - (Vector2)path.vectorPath[currentWayPoint]);
        Vector2 dir2 = (rb.position - (Vector2)target.transform.position);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (dist > firingRadius)
        {
            rb.MovePosition(force + rb.position);
        }
        else
        {
            rb.rotation = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
            if (!isFiring)
            {
                StartCoroutine(fire());
            }
        }

        if (distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }
        if (dist > firingRadius)
        {
            rb.rotation = Mathf.Lerp(rb.rotation, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, rotationSPD);
        }
    }

    private IEnumerator fire()
    {
        shoot.Fire(unitinfo.attack, unitinfo.team);
        isFiring = true;

        yield return new WaitForSeconds(fireSPD);
        isFiring = false;
    }
}
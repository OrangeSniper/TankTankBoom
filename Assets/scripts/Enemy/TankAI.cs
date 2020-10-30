using Pathfinding;
using System.Collections;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    public Teams teams;
    public GameObject target = null;

    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEnd = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    public float dist;

    public float rotationSPD;

    public bool targetInRange = false;
    public bool nullifier = false;
    public bool hasBeenInRange = false;
    public bool seek;

    public bool sourcePlayed = false;

    public TankShoot shoot;

    public float fireSPD = 5f;

    public float radius = 10f;
    public float firingRadius = 5f;

    public bool isFiring = false;

    public Unit unitinfo;

    // Start is called before the first frame update
    private void Start()
    {
        target = NearTarg(unitinfo.team);
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating(nameof(UpdatePath), 0f, .5f);
        unitinfo.InitStats();
    }

    public void Update()
    {
        dist = Vector2.Distance(transform.position, target.transform.position);
        unitinfo.UpdateStats();
    }

    public GameObject NearTarg(tankTeam team)
    {
        GameObject currentClosest = null;
        if (team == tankTeam.red)
        {
            for (int i = 0; i < teams.blue.Length; i++)
            {
                if (teams.blue[i] != null)
                {
                    if (i == 0)
                    {
                        currentClosest = teams.blue[0];
                    }
                    else if (Vector2.Distance(teams.blue[i].transform.position, transform.position) < Vector2.Distance(teams.blue[i - 1].transform.position, transform.position))
                    {
                        currentClosest = teams.blue[i];
                    }
                }
            }
        }
        else if (team == tankTeam.blue)
        {
            for (int i = 0; i < teams.red.Length; i++)
            {
                if (teams.red[i] != null)
                {
                    if (i == 0)
                    {
                        currentClosest = teams.red[0];
                    }
                    else if (Vector2.Distance(teams.red[i].transform.position, transform.position) < Vector2.Distance(teams.red[i - 1].transform.position, transform.position))
                    {
                        currentClosest = teams.red[i];
                    }
                }
            }
        }
        Debug.Log(gameObject.name + ", " + currentClosest.name);
        return currentClosest;
    }

    private void UpdatePath()
    {
        target = NearTarg(unitinfo.team);
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
            if (reachedEnd)
            {
                path = null;
            }
            if (reachedEnd)
            {
                nullifier = false;
            }
        }
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
        rb.velocity = new Vector2(0, 0);
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
                StartCoroutine(Fire());
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

    private IEnumerator Fire()
    {
        shoot.Fire(unitinfo.attack, unitinfo.team);
        isFiring = true;

        yield return new WaitForSeconds(fireSPD);
        isFiring = false;
    }
}
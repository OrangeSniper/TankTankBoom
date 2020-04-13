using Pathfinding;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float radius = 10f;
    public float scanDist = 10f;

    public AIDestinationSetter destSet;
    public Seeker seeker;
    public AIPath aiPath;
    private IAstarAI ai;

    public bool Nullifier = false;
    public bool Null2 = false;

    // Start is called before the first frame update
    private void Start()
    {
        ai = GetComponent<IAstarAI>();
    }

    private Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitCircle * radius;

        point += (Vector2)ai.position;
        return point;
    }

    // Update is called once per frame
    private void Update()
    {
        destSet.target = GameObject.Find("Player").transform;
        if (Vector2.Distance(ai.position, GameObject.Find("Player").transform.position) > scanDist)
        {
            destSet.target = null;

            destSet.enabled = false;
            if (destSet.enabled == false && Nullifier == false)
            {
                ai.SetPath(null);
                Nullifier = true;
            }
            if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
            {
                ai.destination = PickRandomPoint();
                ai.SearchPath();
            }
            Null2 = false;
        }
        else
        {
            if (Null2 == false)
            {
                ai.SetPath(null);
            }
            destSet.enabled = true;
            destSet.target = GameObject.Find("Player").transform;
            Null2 = true;
            Nullifier = false;
        }
    }
}
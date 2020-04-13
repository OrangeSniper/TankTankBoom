using Cinemachine;
using UnityEngine;

public class EnemyFollowCam : MonoBehaviour
{
    public Transform player;
    public CinemachineTargetGroup group;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < allEnemies.Length; i++)
        {
            if (Vector2.Distance(allEnemies[i].transform.position, player.position) <= Vector2.Distance(group.m_Targets[0].target.position, player.transform.position) || group.m_Targets[0].target == null)
            {
                group.m_Targets[0].target = allEnemies[i].transform;
            }
        }
        group.m_Targets[1].target = player.transform;
    }
}
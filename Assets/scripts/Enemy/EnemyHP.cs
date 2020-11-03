using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public TankAI ai;
    public float wait;

    // Update is called once per frame
    private void Update()
    {
        if (ai.unitinfo.HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(int damage)
    {
        ai.unitinfo.HP -= damage;
    }
}
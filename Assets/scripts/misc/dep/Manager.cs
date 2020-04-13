using System.Collections;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject ammo;
    public Transform instantiator;
    public float offset;

    public GameObject enemy;
    public Transform enemyLOC;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(ammoSpawn());
    }

    private IEnumerator ammoSpawn()
    {
        instantiator.position = new Vector2(Random.Range(-7, 7), Random.Range(-5, 5));
        Instantiate(ammo, instantiator.position, instantiator.rotation);
        yield return new WaitForSeconds(offset);
        StartCoroutine(ammoSpawn());
    }
}
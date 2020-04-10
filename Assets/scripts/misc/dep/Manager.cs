using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject ammo;
    public Transform instantiator;
    public float offset;

    public GameObject enemy;
    public Transform enemyLOC;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ammoSpawn());
    }

    IEnumerator ammoSpawn()
    {
        instantiator.position = new Vector2(Random.Range(-7, 7), Random.Range(-5, 5));
        Instantiate(ammo, instantiator.position, instantiator.rotation);
        yield return new WaitForSeconds(offset);
        StartCoroutine(ammoSpawn());
    }

}

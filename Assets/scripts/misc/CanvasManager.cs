using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject player;

    public Text hpAmmo;
    public Text enemies;

    public int maxEnemies;
    public int currentEnemies;

    private void Start()
    {
        maxEnemies = GameObject.FindGameObjectsWithTag("enemy").Length;
    }

    // Update is called once per frame
    private void Update()
    {
        hpAmmo.text = "HP:" + player.GetComponent<BulletGo>().unitInfo.HP + " Ammo:" + player.GetComponent<BulletGo>().ammo;
        currentEnemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        enemies.text = "Enemies left: " + currentEnemies + "/" + maxEnemies;
    }
}
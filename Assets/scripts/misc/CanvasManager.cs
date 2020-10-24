using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject player;

    public Text hpAmmo;
    public Text enemies;

    public int maxEnemies;
    public int currentEnemies;

    public Teams teams;

    private void Start()
    {
        maxEnemies = teams.red.Length;
    }

    // Update is called once per frame
    private void Update()
    {
        hpAmmo.text = "HP:" + player.GetComponent<BulletGo>().unitInfo.HP + " Ammo:" + player.GetComponent<BulletGo>().ammo;
        currentEnemies = EnemiesLeft();
        enemies.text = "Enemies left: " + currentEnemies + "/" + maxEnemies;
    }

    private int EnemiesLeft()
    {
        int enemiesLeft = teams.red.Length;
        for (int i = 0; i < teams.red.Length; i++)
        {
            if(teams.red[i] == null)
            {
                enemiesLeft -= 1;
            }
        }
        return enemiesLeft;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class confiner : MonoBehaviour
{
    public CinemachineVirtualCamera[] brains;
    public CinemachineVirtualCamera cam;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        brains = FindObjectsOfType<CinemachineVirtualCamera>();
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "cursorObject")
        {
            for (int i = 0; i < brains.Length; i++)
            {
                brains[i].Priority = 5;
            }
            cam.Priority = 10;
        }
    }
    

}

using UnityEngine;

public class cursor : MonoBehaviour
{
    public Camera cam;

    // Update is called once per frame
    private void Update()
    {
        transform.position = cam.ScreenToWorldPoint(Input.mousePosition); ;
    }
}
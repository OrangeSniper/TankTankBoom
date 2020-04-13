using UnityEngine;

public class Dissolve : MonoBehaviour
{
    private Material material;

    public bool isDissolving = false;
    private float fade = 1f;

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (isDissolving)
        {
            fade -= Time.deltaTime;
            if (fade <= 0f)
            {
                fade = 0f;
                isDissolving = false;
            }
            material.SetFloat("_Fade", fade);
        }
    }
}
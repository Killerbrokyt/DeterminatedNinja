using UnityEngine;

public class ImageScroller : MonoBehaviour
{
    public float velocidad = 30f;
    public float ancho = 64f;

    private Vector3 posicionInicial;

    private void Start()
    {
        posicionInicial= transform.position;
    }

    void Update()
    {
        transform.position +=
            Vector3.left * velocidad * Time.deltaTime;

        if (transform.position.x <= -ancho)
        {
            transform.position = posicionInicial;
        }
    }
}

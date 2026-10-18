using UnityEngine;

public class ObjectScroller : MonoBehaviour
{
    public float velocidad = 30f;

    private float posicionFinalX;

    // Lo llama el spawner justo después de instanciar el objeto
    public void ObtenerPosicionFinal(float posicionFinalX)
    {
        this.posicionFinalX = posicionFinalX;
    }

    private void Update()
    {
        transform.position += Vector3.left * velocidad * Time.deltaTime;

        if (transform.position.x <= posicionFinalX)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject [] objetosSpawneables;
    public float tiempoEntreSpawn = 1f;

    private float cronometroDeSpawn = 0f;
    private float posicionFinalX = -60f;
    private Vector3 posicionInicial;

    private void Start()
    {
        posicionInicial = transform.position;
    }

    private void Update()
    {
        cronometroDeSpawn += Time.deltaTime;

        if(cronometroDeSpawn >= tiempoEntreSpawn)
        {
            GenerarObjeto();
            cronometroDeSpawn = 0f;
        }
    }

    private void GenerarObjeto()
    {
        int indiceAleatorio = Random.Range(0,objetosSpawneables.Length);
        GameObject objeto_A_Spawnear = objetosSpawneables[indiceAleatorio];

        GameObject objeto = Instantiate(objeto_A_Spawnear,posicionInicial,Quaternion.identity);

        ObjectScroller scroller = objeto.GetComponent<ObjectScroller>();
        scroller.ObtenerPosicionFinal(posicionFinalX);

    }

}

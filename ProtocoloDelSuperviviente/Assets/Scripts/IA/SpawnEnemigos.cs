using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public GameObject prefabEnemigo;
    public int cantidadEnemigos = 5;
    public Transform[] puntosSpawn;

    void Start()
    {
        for (int i = 0; i < cantidadEnemigos; i++)
        {
            int numeroRandom = Random.Range(0, puntosSpawn.Length);
            Instantiate(prefabEnemigo, puntosSpawn[numeroRandom].position, Quaternion.identity);
        }
    }
}
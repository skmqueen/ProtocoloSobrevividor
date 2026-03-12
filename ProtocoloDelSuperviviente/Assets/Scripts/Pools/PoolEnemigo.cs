using UnityEngine;
using System.Collections.Generic;

public class PoolEnemigo : MonoBehaviour
{
    public static PoolEnemigo Instance;
    public int cantidadEnemigos = 5;
    public Transform[] puntosSpawn;
    public GameObject prefabEnemigo;

    private Stack <GameObject> pool = new Stack <GameObject>();
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        for (int i = 0; i < cantidadEnemigos; i++)
        {
            CrearEnemigo();
        }

    }
  
    void Start()
    {
        // Barajar puntosSpawn directamente (Fisher-Yates)
        for (int i = puntosSpawn.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Transform temp = puntosSpawn[i];
            puntosSpawn[i] = puntosSpawn[j];
            puntosSpawn[j] = temp;
        }

        for (int i = 0; i < cantidadEnemigos && i < puntosSpawn.Length; i++)
        {
            GameObject enemigo = PopObj();
            enemigo.transform.position = puntosSpawn[i].position;
        }
    }

    public void CrearEnemigo()
    {
        GameObject enemigo = Instantiate(prefabEnemigo, transform.position, Quaternion.identity);
        enemigo.SetActive(false);
        pool.Push(enemigo);

    }

    public GameObject PopObj()
    {
        if (pool.Count == 0)
        {
            CrearEnemigo();
        }
        GameObject enemigo = pool.Pop();
        enemigo.SetActive(true);
        Rigidbody rb = enemigo.GetComponent<Rigidbody>();
        return enemigo;

    }

    public void PushObj (GameObject elenemigo)
    {
        elenemigo.gameObject.SetActive(false);
        elenemigo.transform.position = transform.position;
        elenemigo.transform.rotation = Quaternion.identity;
        pool.Push(elenemigo);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

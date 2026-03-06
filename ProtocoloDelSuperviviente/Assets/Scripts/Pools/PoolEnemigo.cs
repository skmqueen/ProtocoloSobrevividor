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

        for (int i = 0; i > cantidadEnemigos; i++)
        {
            CrearEnemigo();
        }

    }
  
    void Start()
    {
        
    }

    public void CrearEnemigo()
    {
        int posicionAleatoria = Random.Range(0, puntosSpawn.Length);
        GameObject enemigo = Instantiate(prefabEnemigo, puntosSpawn[posicionAleatoria].position, Quaternion.identity);
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

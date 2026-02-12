using UnityEngine;
using System.Collections.Generic;

public class PoolBalas : MonoBehaviour
{
    public static PoolBalas Instance;
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public int cantidadInicial = 10;
    public float tiempoReposicion = 3f;

    private Stack<GameObject> pool = new Stack <GameObject>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

         for (int i = 0; i < cantidadInicial; i++)
        {
            CrearBala();
        }
    }

    void Start()
    {

    }

    public void CrearBala()
    {
        GameObject bala = Instantiate(prefabBala, puntoDisparo.position, Quaternion.identity);
        bala.SetActive(false);
        pool.Push(bala);
    }

    public GameObject PopObj()
    {
        if (pool.Count == 0)
        {
            CrearBala();
        }

        GameObject bala  = pool.Pop();
        bala.SetActive(true);
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        return bala;
    }





    public void PushObj(GameObject balaEnemiga)
        {
            balaEnemiga.gameObject.SetActive(false);
        balaEnemiga.transform.position = transform.position;
        balaEnemiga.transform.rotation = Quaternion.identity;
        pool.Push(balaEnemiga);
        }
}
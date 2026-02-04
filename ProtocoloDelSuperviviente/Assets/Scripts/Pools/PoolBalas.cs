using UnityEngine;
using System.Collections.Generic;

public class PoolBalas : MonoBehaviour
{
    public static PoolBalas Instance;


    public GameObject prefabBala;
    public int cantidadInicial = 10;
    public float tiempoReposicion = 3f;

    private Stack<BalaEnemiga> pool = new Stack <BalaEnemiga>();

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

    void CrearBala()
    {
        GameObject obj = Instantiate(prefabBala, transform);
        BalaEnemiga bala = obj.GetComponent<BalaEnemiga>();
        obj.SetActive(false);
        pool.Push(bala);
    }

    public BalaEnemiga Pop()
    {
         if (pool.Count == 0)
        {
            CrearBala();
        }

        BalaEnemiga bala  = pool.Pop();
        bala.gameObject.SetActive(true);
        return bala;
    }





    public void Push(BalaEnemiga bala)
        {
            bala.gameObject.SetActive(false);
            pool.Push(bala);
        }
}
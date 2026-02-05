using UnityEngine;
using System.Collections.Generic;

public class ProyectilPool : MonoBehaviour
{
    public static ProyectilPool Instance;
    public GameObject prefabProyectil;
    public int cantidadInicial = 15;
    
    private Stack<GameObject> pool = new Stack<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        for (int i = 0; i < cantidadInicial; i++)
        {
            CrearNuevoProyectil();
        }
    }

    private void Start()
    {
        
    }

    private void CrearNuevoProyectil()
    {
        GameObject proyectil = Instantiate(prefabProyectil, transform);
        proyectil.SetActive(false);
        pool.Push(proyectil);
    }

    public GameObject PopObj()
    {
        GameObject retornoProyectil = null;

        if (pool.Count != 0)
        {
            retornoProyectil.SetActive(true);
            retornoProyectil = pool.Pop();
            retornoProyectil.transform.position = Vector3.zero;
            retornoProyectil.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
        else
        {
            retornoProyectil.SetActive(false);
        }
        return retornoProyectil;
    }

    public void PushObj(GameObject obj)
    {
        obj.SetActive(false);
        pool.Push(obj);
    }

}
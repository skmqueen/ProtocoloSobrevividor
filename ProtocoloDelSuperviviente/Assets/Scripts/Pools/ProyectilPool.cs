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

           if (pool.Count == 0)
        {
            CrearNuevoProyectil();
        }

        GameObject proyectil = pool.Pop();
        proyectil.SetActive(true);
        Rigidbody rb = proyectil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        return proyectil;
    }

    public void PushObj(GameObject obj)
    {
        obj.SetActive(false);
        
        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.identity;
        
        pool.Push(obj);
    }

}
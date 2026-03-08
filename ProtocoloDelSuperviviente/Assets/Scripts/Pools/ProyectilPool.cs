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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        for (int i = 0; i < cantidadInicial; i++)
            CrearNuevoProyectil();
    }

    private void CrearNuevoProyectil()
    {
        GameObject proyectil = Instantiate(prefabProyectil, transform);
        proyectil.SetActive(false);
        pool.Push(proyectil);
    }

    public GameObject PopObj()
    {
        if (pool.Count == 0)
            CrearNuevoProyectil();

        GameObject proyectil = pool.Pop();
        // Reset física
        Rigidbody rb = proyectil.GetComponent<Rigidbody>();

        proyectil.SetActive(true);
        return proyectil;
    }

    public void PushObj(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        pool.Push(obj);
    }
}

using UnityEngine;
using System.Collections.Generic;

public class ProyectilPool : MonoBehaviour
{
    [Header("Configuración Pool")]
    public GameObject prefabProyectil;
    public int cantidadInicial = 15;
    
    private Stack<Proyectil> pool = new Stack<Proyectil>();

    private void Start()
    {
        for (int i = 0; i < cantidadInicial; i++)
        {
            CrearNuevoProyectil();
        }
    }

    private void CrearNuevoProyectil()
    {
        GameObject obj = Instantiate(prefabProyectil, transform);
        Proyectil proyectil = obj.GetComponent<Proyectil>();
        obj.SetActive(false);
        pool.Push(proyectil);
    }

    public Proyectil Pop()
    {

        if (pool.Count == 0)
        {
            CrearNuevoProyectil();
        }

        Proyectil proyectil = pool.Pop();
        proyectil.gameObject.SetActive(true);
        return proyectil;
    }

    public void Push(Proyectil proyectil)
    {
        proyectil.gameObject.SetActive(false);
        pool.Push(proyectil);
    }

    public int ProyectilesDisponibles()
    {
        return pool.Count;
    }
}
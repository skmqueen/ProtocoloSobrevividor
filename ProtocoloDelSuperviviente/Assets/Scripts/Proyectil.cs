using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Proyectil : MonoBehaviour
{
    public float velocidad = 20f;
    public float danio = 1f;
    
    private Rigidbody rb;
    private ProyectilPool pool;
    private float tiempoVida = 3f;
    private float tiempoSpawn;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void VamosAlla(ProyectilPool poolRef, Vector3 direccion)
    {
        pool = poolRef;
        tiempoSpawn = Time.deltaTime;
        
        rb.linearVelocity = direccion.normalized * velocidad;
        transform.forward = direccion;
    }

    private void Update()
    {
        if (Time.deltaTime - tiempoSpawn >= tiempoVida)
        {
            DevolverAPool();
        }
    }


    private void DevolverAPool()
    {
        if (pool != null)
        {
            rb.linearVelocity = Vector3.zero;
            pool.Push(this);
        }
    }
}

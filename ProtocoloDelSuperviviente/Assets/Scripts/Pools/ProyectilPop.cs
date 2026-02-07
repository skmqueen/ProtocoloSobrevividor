using UnityEngine;

public class ProyectilPop : MonoBehaviour
{
    public float tiempoVida = 3f;
    public int daño = 10;
    public bool desactivarAlImpactar = true;

    private float tiempoActivacion;

    void OnEnable()
    {
        tiempoActivacion = Time.time; // Guarda el momento en que se activó
    }

    void Update()
    {
        if (Time.time >= tiempoActivacion + tiempoVida)
        {
           DevolverAPool();
        }
    }

     void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Bala impactó enemigo");
        }

        if (desactivarAlImpactar)
        {
            DevolverAPool();
        }
    }

    private void DevolverAPool()
    {
        ProyectilPool.Instance.PushObj(gameObject);
    }
}

using UnityEngine;

public class BalasPop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float tiempoVida = 3f;
    public int daño = 10;
    public bool desactivarAlImpactar = true;

    private float tiempoActivacion;

    void OnEnable()
    {
        tiempoActivacion = Time.time;
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

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Bala impactó player");
        }

        if (desactivarAlImpactar)
        {
            DevolverAPool();
        }
    }

    private void DevolverAPool()
    {
        PoolBalas.Instance.PushObj(gameObject);
    }
}

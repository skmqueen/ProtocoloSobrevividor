using UnityEngine;

public class BalaEnemiga : MonoBehaviour
{

    public float velocidad = 20f;
    public float danio = 10f;

    private float tiempoActivacion;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        tiempoActivacion = Time.time;
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * velocidad;
        }
    }

    public float TiempoActivo()
    {
        return Time.deltaTime - tiempoActivacion;
    }

    void OnTriggerEnter(Collider other)
    {
 
        if (other.CompareTag("Player"))
        {
            
            PoolBalas.Instance.Pop();
        }
        else if (!other.CompareTag("Enemy"))
        {
            PoolBalas.Instance.Pop();
        }
    }
}

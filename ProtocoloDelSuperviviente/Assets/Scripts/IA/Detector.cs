using UnityEngine;

public class Detector : MonoBehaviour
{
    private IAEnemigo enemigo;
    
    void Start()
    {
        enemigo = GetComponentInParent<IAEnemigo>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemigo.OnJugadorDetectado();
        }
    }
}
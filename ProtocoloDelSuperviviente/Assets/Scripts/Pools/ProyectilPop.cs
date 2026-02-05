using UnityEngine;

public class ProyectilPop : MonoBehaviour
{
    public float tiempoBala = 2f;
    public float tiempoTranscurrido;
    public GameObject proyectil;

    void Update()
    {
        tiempoTranscurrido += Time.deltaTime;
        if ( tiempoTranscurrido >= tiempoBala)
        {
            tiempoTranscurrido = 0f;
            ProyectilPool.Instance.PushObj(proyectil);
        }
        
    }
}

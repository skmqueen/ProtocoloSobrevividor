using UnityEngine;
using UnityEngine.AI;

public class IAEnemigo : MonoBehaviour
{
   
    public Transform[] puntosPatrulla;
    public GameObject enemigo;
    public Transform jugador;
    public NavMeshAgent agent;
    public GameObject balaEnemiga;
    public Transform puntoDisparo;
    private Player player;
    public int vida = 10;
    public int puntos = 50;
    
    
    public float rangoVision = 15f;
    public float rangoAtaque = 2f;
    public LayerMask capaJugador;
    
    private IEstado estadoActual;
    private int indicePuntoActual = 0;
    
 
    private EstadoPatrulla estadoPatrulla;
    private EstadoPersecucion estadoPersecucion;
    private EstadoAtaque estadoAtaque;
    
    void Start()
    {
      
        estadoPatrulla = new EstadoPatrulla(this);
        estadoPersecucion = new EstadoPersecucion(this);
        estadoAtaque = new EstadoAtaque(this, puntoDisparo, balaEnemiga, agent);
        
        CambiarEstado(estadoPatrulla);
    }
    
    void Update()
    {
        estadoActual?.Ejecutar();
    }
    
    public void CambiarEstado(IEstado nuevoEstado)
    {
        if (estadoActual != null)
        {
        estadoActual.Salir();
        }
        estadoActual = nuevoEstado;
        estadoActual.Entrar();
    }
    
    public Transform ObtenerSiguientePunto()
    {
        Transform punto = puntosPatrulla[indicePuntoActual];
        indicePuntoActual = (indicePuntoActual + 1) % puntosPatrulla.Length;
        return punto;
    }
    
    public bool JugadorEnRango()
    {
        return Vector3.Distance(transform.position, jugador.position) <= rangoVision;
    }
    
    public bool JugadorEnRangoAtaque()
    {
        return Vector3.Distance(transform.position, jugador.position) <= rangoAtaque;
    }
    
    public bool TieneLineaVision()
    {
        Vector3 direccion = (jugador.position - transform.position).normalized;
        return Physics.Raycast(transform.position, direccion, out RaycastHit hit, rangoVision, capaJugador) 
        && hit.transform == jugador;
    }
    
    public void OnJugadorDetectado()
    {
        if (TieneLineaVision())
        {
            CambiarEstado(estadoPersecucion);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Proyectil"))
        {
            RecibirDanio(2);
        }

    }

    public void RecibirDanio(int danio)
    {
        vida -= danio;
        Score.Instance.SumarPuntos(puntos);
        if ( vida <= 0)
        {
            Morir();
        }
    }

    public void Morir()
    {
        PoolEnemigo.Instance.PushObj(enemigo);
    }

    public IEstado EstadoPatrulla => estadoPatrulla;
    public IEstado EstadoPersecucion => estadoPersecucion;
    public IEstado EstadoAtaque => estadoAtaque;
}
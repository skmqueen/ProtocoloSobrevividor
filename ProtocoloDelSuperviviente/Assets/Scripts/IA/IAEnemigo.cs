using UnityEngine;
using UnityEngine.AI;

public class IAEnemigo : MonoBehaviour
{
   
    public Transform[] puntosPatrulla;
    public Transform jugador;
    public NavMeshAgent agent;
    public GameObject balaEnemiga;
    public Transform puntoDisparo;
    
    
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
        estadoAtaque = new EstadoAtaque(this, puntoDisparo, balaEnemiga);
        
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
    
    public IEstado EstadoPatrulla => estadoPatrulla;
    public IEstado EstadoPersecucion => estadoPersecucion;
    public IEstado EstadoAtaque => estadoAtaque;
}
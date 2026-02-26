using UnityEngine;
using UnityEngine.AI;
public class EstadoAtaque : IEstado
{
    private IAEnemigo enemigo;
    private float tiempoUltimoAtaque;
    private float cooldownAtaque = 1.5f;
    public Transform puntoDisparo;
    public GameObject balaEnemiga;
    private Player vidaPlayer;
    public NavMeshAgent agent;
    
    public EstadoAtaque(IAEnemigo enemigo, Transform puntoDisparo, GameObject balaEnemiga, NavMeshAgent agent)
    {
        this.enemigo = enemigo;
        this.puntoDisparo = puntoDisparo;
        this.balaEnemiga = balaEnemiga;
        this.agent = agent;
        vidaPlayer = enemigo.jugador.GetComponent<Player>();
    }
    
    public void Entrar()
    {
        enemigo.agent.isStopped = true;
    }
    
    public void Ejecutar()
    {
        Vector3 direccion = enemigo.jugador.position - enemigo.transform.position;
        direccion.y = 0;
        enemigo.transform.rotation = Quaternion.Slerp(
            enemigo.transform.rotation, 
            Quaternion.LookRotation(direccion), 
            Time.deltaTime * 5f
        );
        
       
        if (Time.time >= tiempoUltimoAtaque + cooldownAtaque)
        {
            Atacar();
            tiempoUltimoAtaque = Time.time;
        }
        
        
        if (!enemigo.JugadorEnRangoAtaque())
        {
            enemigo.CambiarEstado(enemigo.EstadoPersecucion);
        }

        if (vidaPlayer != null && vidaPlayer.vida <= 0)
        {
        enemigo.agent.isStopped = true;
        agent.velocity = Vector3.zero;
        enemigo.CambiarEstado(enemigo.EstadoPatrulla);
        }
}
    
   private void Atacar()
{
    GameObject bala = PoolBalas.Instance.PopObj();
    
    
    bala.transform.position = puntoDisparo.position;
    bala.transform.rotation = puntoDisparo.rotation;
    

    Rigidbody rb = bala.GetComponent<Rigidbody>();
    if (rb != null)
    {
        Vector3 direccion = (enemigo.jugador.position - puntoDisparo.position).normalized;
        rb.linearVelocity = direccion * 10f;
    }
    
    Debug.Log("VEN ACÁ WACHO");
}
    
    public void Salir()
    {
        enemigo.agent.isStopped = false;
    }
}
using UnityEngine;

public class EstadoAtaque : IEstado
{
    private IAEnemigo enemigo;
    private float tiempoUltimoAtaque;
    private float cooldownAtaque = 1.5f;
    public Transform puntoDisparo;
    public GameObject balaEnemiga;
    
    public EstadoAtaque(IAEnemigo enemigo, Transform puntoDisparo, GameObject balaEnemiga)
    {
        this.enemigo = enemigo;
        this.puntoDisparo = puntoDisparo;
        this.balaEnemiga = balaEnemiga;
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
    }
    
    private void Atacar()
    {
        PoolBalas.Instance.PopObj();
        Debug.Log("VEN ACÁ WACHO");

    }
    
    public void Salir()
    {
        enemigo.agent.isStopped = false;
    }
}
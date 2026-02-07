using UnityEngine;

public class EstadoPatrulla : IEstado
{
    private IAEnemigo enemigo;
    
    public EstadoPatrulla(IAEnemigo enemigo)
    {
        this.enemigo = enemigo;
    }
    
    public void Entrar()
    {
        enemigo.agent.speed = 2f;
        //POR ESTO SON MEJORES LAS VARIABLES PÚBLICAS JEJEJE
        enemigo.agent.SetDestination(enemigo.ObtenerSiguientePunto().position);
    }
    
    public void Ejecutar()
    {
        if (!enemigo.agent.pathPending && enemigo.agent.remainingDistance <= 0.5f)
        {
            Debug.Log("PATRULLANDO");
            enemigo.agent.SetDestination(enemigo.ObtenerSiguientePunto().position);
        }
        
        if (enemigo.JugadorEnRango() && enemigo.TieneLineaVision())
        {
            enemigo.CambiarEstado(enemigo.EstadoPersecucion);
        }
    }
    
    public void Salir()
    {

    }
}
using UnityEngine;

public class EstadoPersecucion : IEstado
{
    private IAEnemigo enemigo;
    
    public EstadoPersecucion(IAEnemigo enemigo)
    {
        //Guardamos la referencia enemigo. ESTE ENEMIGO (variable guardada) es igual a enemigo DENTRO DEL SCRIPT. Se usa debido a que se llaman igual
        this.enemigo = enemigo;
    }
    
    public void Entrar()
    {
        enemigo.agent.speed = 4f;
    }
    
    public void Ejecutar()
    {
        enemigo.agent.SetDestination(enemigo.jugador.position);
        if (enemigo.JugadorEnRangoAtaque())
        {
            enemigo.CambiarEstado(enemigo.EstadoAtaque);
        }
        else if (!enemigo.JugadorEnRango() || !enemigo.TieneLineaVision())
        {
            enemigo.CambiarEstado(enemigo.EstadoPatrulla);
        }
    }

    public void Salir()
    {
    }
}
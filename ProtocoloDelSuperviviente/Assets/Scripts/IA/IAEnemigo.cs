using UnityEngine;
using UnityEngine.AI;

public class IAEnemigo : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] destinos;
    private int i = 0;
    public bool seguir;
    private GameObject player;
    private float distanciaPlayer;
    public float distanciaAlPlayer = 10;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent.destination = destinos[0].transform.position;
        player = FindAnyObjectByType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        distanciaPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanciaAlPlayer <= distanciaPlayer && seguir)
        {
            SeguirJugador();
        }
        else
        {
            Ruta();
        }
        
    }

    public void Ruta()
    {
        navMeshAgent.destination = destinos[i].transform.position;
        if ( Vector3.Distance(transform.position, destinos[i].position) <= distanciaAlPlayer)
        {
            if(destinos[i] != destinos[destinos.Length - 1])
            {
                i++;
            }
            else
            {
                i = 0;
            }
        }

    }

    public void SeguirJugador()
    {
        navMeshAgent.destination = player.transform.position;
    }
}

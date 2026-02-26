using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    public Camera cameraPrincipal;

    public Transform puntoDisparo;
    public float fuerzaDisparo = 20f;
    public float cadenciaDisparo = 0.2f;

    public float velocidadCaminar = 3.5f;
    public float velocidadCorrer = 6f;
    public float sensibilidadRotacion = 200f;
    public float vida;
    public int vidaMaxima = 5;

    private NavMeshAgent agent;
    private Animator animator;


    private bool estaMuerto;
    private float tiempoUltimoDisparo;
    public int danio = 1;

    private float anguloVertCamara;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        vida = vidaMaxima;
    }

    void Update()
    {
        if (!estaMuerto)
        {
            Movimiento();
            Mirar();
            Disparar();
        }

        ControlarAnimaciones();
    }

    private void Movimiento()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direccion = new Vector3(h, 0, v);
        direccion.Normalize();

       bool corriendo = Input.GetButton("Run");

      if(corriendo)
        {
            agent.speed = velocidadCorrer;
        }
      else
        {
            agent.speed = velocidadCaminar;
        }

        Vector3 movimiento = transform.TransformDirection(direccion);
        agent.Move(movimiento * agent.speed * Time.deltaTime);
    }

    private void Mirar()
    {
        float rotX = Input.GetAxis("Mouse X") * sensibilidadRotacion * Time.deltaTime;
        transform.Rotate(Vector3.up * rotX);
    }

  private void ControlarAnimaciones()
{
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    Vector3 direccionInput = new Vector3(h, 0, v);
    float velocidadActual = Mathf.Clamp01(direccionInput.magnitude) * agent.speed;
    
    bool corriendo = Input.GetButton("Run");
    bool disparando = Input.GetButton("Fire1");

    animator.SetFloat("Speed", velocidadActual);
    animator.SetBool("IsRunning", corriendo);
    animator.SetBool("IsShooting", disparando);
    animator.SetBool("IsDead", estaMuerto);

}

private void Disparar()
{
   if (Input.GetButton("Fire1") && Time.time >= tiempoUltimoDisparo + cadenciaDisparo)
        {
            tiempoUltimoDisparo = Time.time;

            GameObject bala = ProyectilPool.Instance.PopObj();
            bala.transform.position = puntoDisparo.position;
            bala.transform.rotation = puntoDisparo.rotation;

            Rigidbody rb = bala.GetComponent<Rigidbody>();
            rb.linearVelocity = puntoDisparo.forward * fuerzaDisparo;
        }
    
}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BalaEnemiga"))
        {
        RecibirDanio(1);
        }
     
    }

    public void RecibirDanio(int danio)
    {
        vida -= danio;

        if (vida == 0)
        {
            Morir();
            //Menus.Instance.GameOver(GameOver);
        }
    }


    private void Morir()
    {
        estaMuerto = true;

        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}

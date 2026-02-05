using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    public Camera cameraPrincipal;

    public float velocidadCaminar = 3.5f;
    public float velocidadCorrer = 6f;
    public float sensibilidadRotacion = 200f;
    public float vida;

    private NavMeshAgent agent;
    private Animator animator;

    private float anguloVertCamara;
    private bool estaMuerto;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
    }

    void Update()
    {
        if (!estaMuerto)
        {
            Movimiento();
            Mirar();
        }

        ControlarAnimaciones();
    }

    private void Movimiento()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direccion = new Vector3(h, 0, v);
        float magnitud = Mathf.Clamp01(direccion.magnitude);
        direccion.Normalize();

        bool corriendo = Input.GetButton("Run");

        agent.speed = corriendo ? velocidadCorrer : velocidadCaminar;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BalaEnemiga"))
        {
            Destroy(other.gameObject);
        }
    }


    private void Morir()
    {
        estaMuerto = true;

        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}

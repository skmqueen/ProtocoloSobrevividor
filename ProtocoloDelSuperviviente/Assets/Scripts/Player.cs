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

    public int vidasMaximas = 5;
    private int vidasActuales;
    public ControladorVidas controladorVidas;

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
        vidasActuales = vidasMaximas;

        if (controladorVidas == null)
            controladorVidas = FindFirstObjectByType<ControladorVidas>();

        if (controladorVidas != null)
            controladorVidas.ActualizarVidas(vidasActuales, vidasMaximas);
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
        float rotY = Input.GetAxis("Mouse Y") * sensibilidadRotacion * Time.deltaTime;

        anguloVertCamara += rotY;
        anguloVertCamara = Mathf.Clamp(anguloVertCamara, -70f, 70f);

        transform.Rotate(Vector3.up * rotX);
        cameraPrincipal.transform.localRotation = Quaternion.Euler(-anguloVertCamara, 0f, 0f);
    }

  private void ControlarAnimaciones()
{
    // Calcula la velocidad basada en el input, NO en agent.velocity
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
    
    // DEBUG - borra después
    Debug.Log($"Speed enviado al Animator: {velocidadActual}");
}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ProyectilEnemigo"))
        {
            RecibirDanio(1);
            Destroy(other.gameObject);
        }
    }

    public void RecibirDanio(int cantidad)
    {
        if (!estaMuerto)
        {
            vidasActuales -= cantidad;

            if (controladorVidas != null)
                controladorVidas.ActualizarVidas(vidasActuales, vidasMaximas);

            if (vidasActuales <= 0)
                Morir();
        }
    }

    private void Morir()
    {
        estaMuerto = true;

        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}

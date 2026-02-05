using UnityEngine;
using System.Collections;

public class ArmasController : MonoBehaviour
{

    public Transform pitorro;
    public float disparoVelocidad = 0.1f;

    public GameObject proyectil;
    public int maxBalas = 8;
    private int balasActuales;
    public float tiempoRecarga = 2f;
    private bool recargando = false;
    private float ultimoDisparo = Mathf.NegativeInfinity;
    private float tiempoPool = 2f;
    private Animator animator;

    private void Awake()
    {
        balasActuales = maxBalas;
        animator = GetComponentInParent<Animator>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        bool disparando = Input.GetButton("Fire1");
        
        if (animator != null)
        {
            animator.SetBool("IsShooting", disparando);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            IntentoDisparo();
        }
        
        if (Input.GetKeyDown(KeyCode.R) && balasActuales < maxBalas && !recargando)
        {
            StartCoroutine(Recarga());
        }
    }

    private void IntentoDisparo()
    {
        if (recargando)
        {
 
        }
        else
        {
            Disparo();
        }
    }

    private void Disparo()
    {

        ProyectilPool.Instance.PopObj();
        proyectil.transform.position = pitorro.position;
        balasActuales--;
        ultimoDisparo = Time.deltaTime;
    }

    IEnumerator Recarga()
    {
        recargando = true;
        yield return new WaitForSeconds(tiempoRecarga);
        balasActuales = maxBalas;
        recargando = false;
    }
}
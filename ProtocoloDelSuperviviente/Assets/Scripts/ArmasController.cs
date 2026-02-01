using UnityEngine;
using System.Collections;

public class ArmasController : MonoBehaviour
{
    [Header("Disparo")]
    public Transform pitorro;
    public float disparoVelocidad = 0.1f;
    public GameObject flash;
    
    [Header("Munición")]
    public int maxBalas = 8;
    private int balasActuales;
    public float tiempoRecarga = 2f;
    private bool recargando = false;
    private float ultimoDisparo = Mathf.NegativeInfinity;
    
    [Header("Referencias")]
    public ProyectilPool poolProyectiles;
    private Animator animator;

    private void Awake()
    {
        balasActuales = maxBalas;
        animator = GetComponentInParent<Animator>();
    }

    private void Start()
    {
        if (poolProyectiles == null)
            poolProyectiles = FindFirstObjectByType<ProyectilPool>();
    }

    private void Update()
    {
        bool disparando = Input.GetButton("Fire1");
        
        if (animator != null)
        {
            animator.SetBool("IsShooting", disparando);
        }
        
        // Cambiado a GetButtonDown para un solo disparo por click
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
            // No hacer nada
        }
        else if (balasActuales <= 0)
        {
            // No hacer nada
        }
        else
        {
            Disparo();
        }
    }

    private void Disparo()
    {
        if (flash != null)
        {
            GameObject flashObj = Instantiate(flash, pitorro.position, pitorro.rotation, pitorro);
            Destroy(flashObj, 0.1f);
        }

        if (poolProyectiles != null)
        {
            Proyectil proyectil = poolProyectiles.Pop();
            proyectil.transform.position = pitorro.position;
            proyectil.Inicializar(poolProyectiles, pitorro.forward);
        }

        balasActuales--;
        ultimoDisparo = Time.time;
    }

    IEnumerator Recarga()
    {
        recargando = true;
        yield return new WaitForSeconds(tiempoRecarga);
        balasActuales = maxBalas;
        recargando = false;
    }
}
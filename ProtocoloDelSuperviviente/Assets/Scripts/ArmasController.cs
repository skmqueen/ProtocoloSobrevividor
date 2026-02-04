using UnityEngine;
using System.Collections;

public class ArmasController : MonoBehaviour
{

    public Transform pitorro;
    public float disparoVelocidad = 0.1f;
    

    public int maxBalas = 8;
    private int balasActuales;
    public float tiempoRecarga = 2f;
    private bool recargando = false;
    private float ultimoDisparo = Mathf.NegativeInfinity;
    

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
        if (poolProyectiles != null)
        {
            Proyectil proyectil = poolProyectiles.Pop();
            proyectil.transform.position = pitorro.position;
            proyectil.VamosAlla(poolProyectiles, pitorro.forward);
        }

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
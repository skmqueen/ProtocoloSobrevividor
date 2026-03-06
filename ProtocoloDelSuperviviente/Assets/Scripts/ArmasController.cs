using UnityEngine;
using System.Collections;
using TMPro;


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
    private Animator animator;
    public TMPro.TextMeshProUGUI textoBalas;

    private void Awake()
    {
        balasActuales = maxBalas;
        animator = GetComponentInParent<Animator>();
    }

    private void Start()
    {
        int balasActuales = maxBalas;
        //textoBalas.text = balasActuales.ToString();
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

        if (balasActuales > 0)
        {
           // textoBalas.text = balasActuales.ToString();
        }
        else
        {
            textoBalas.text = "Recarga";
        }

        if (balasActuales <= 0)
        {
            textoBalas.text = "Recarga";
        }
    }

    IEnumerator Recarga()
    {
        recargando = true;
        yield return new WaitForSeconds(tiempoRecarga);
        balasActuales = maxBalas;
        textoBalas.text = balasActuales.ToString();
        recargando = false;
    }
}
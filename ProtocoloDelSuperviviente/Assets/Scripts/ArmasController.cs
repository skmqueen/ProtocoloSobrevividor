using UnityEngine;
using System.Collections;
using TMPro;

public class ArmasController : MonoBehaviour
{
    [Header("Disparo")]
    public Transform pitorro;
    public float disparoVelocidad = 50f;
    public float cadenciaDisparo = 0.2f;

    [Header("Balas")]
    public float maxBalas = 8f;
    public float balasActuales;
    public float tiempoRecarga = 2f;
    private bool recargando = false;

    [Header("UI")]
    public TextMeshProUGUI textoBalas;
    private float tiempoEnergia = 4f;

    private float tiempoUltimoDisparo;

    private Energia energia;

    private void Awake()
    {
        balasActuales = maxBalas;
        energia = GameObject.Find("UI").GetComponent<Energia>();
    }

    private void Start()
    {
        ActualizarTextoBalas();
        balasActuales = maxBalas;
    }

    private void Update()
    {
        // Recargar manualmente
        if (Input.GetKeyDown(KeyCode.R) && balasActuales < maxBalas && !recargando)
        {
            StartCoroutine(Recarga());
        }

        // Disparar
        if (Input.GetButtonDown("Fire1") && Time.time >= tiempoUltimoDisparo + cadenciaDisparo)
        {
            IntentarDisparar();
        }
    }

    private void IntentarDisparar()
    {
        if (energia.barraEnergia.fillAmount == 1)
        {
        if (!recargando && balasActuales > 0 )
        {
        tiempoUltimoDisparo = Time.time;

        // Reproducir sonido
        AudioManager.instance.ReproducirDisparo();

        // Sacar proyectil del pool
        GameObject bala = ProyectilPool.Instance.PopObj();
        bala.transform.position = pitorro.position;
        bala.transform.rotation = pitorro.rotation;

        Energia.Instance.energiaFuera();

        // Aplicar fuerza
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        rb.linearVelocity = pitorro.forward * disparoVelocidad;

        // Una bala menos jejejejejejejejejeje
        balasActuales--;
        ActualizarTextoBalas();
        }

        if(energia.barraEnergia.fillAmount <= 0)
        {
            tiempoEnergia -= Time.deltaTime;

            if (tiempoEnergia <= 0)
            {
            energia.balasEnergia = 0f;
            energia.barraEnergia.fillAmount = 1;
            tiempoEnergia = 4f;
            }



        }

        if (balasActuales <= 0)
        {
            textoBalas.text = "Recarga";
            energia.balasEnergia = 4f;
        }

        if (recargando)
        {
            textoBalas.text = "Recargando";
        }
        }
    }

    IEnumerator Recarga()
    {
        recargando = true;

        yield return new WaitForSeconds(tiempoRecarga);

        balasActuales = maxBalas;
        ActualizarTextoBalas();

        recargando = false;
    }

    private void ActualizarTextoBalas()
    {
        if (textoBalas != null)
            textoBalas.text = balasActuales.ToString();
    }
}
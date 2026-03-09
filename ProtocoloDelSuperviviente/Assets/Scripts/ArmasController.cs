using UnityEngine;
using System.Collections;
using TMPro;

public class ArmasController : MonoBehaviour
{
    [Header("Disparo")]
    public Transform pitorro;
    public float disparoVelocidad = 20f;
    public float cadenciaDisparo = 20f;

    [Header("Balas")]
    public int maxBalas = 8;
    private int balasActuales;
    public float tiempoRecarga = 2f;
    private bool recargando = false;

    [Header("UI")]
    public TextMeshProUGUI textoBalas;

    private float tiempoUltimoDisparo;

    private void Awake()
    {
        balasActuales = maxBalas;
    }

    private void Start()
    {
        ActualizarTextoBalas();
    }

    private void Update()
    {
        // Recargar manualmente
        if (Input.GetKeyDown(KeyCode.R) && balasActuales < maxBalas && !recargando)
        {
            StartCoroutine(Recarga());
        }

        // Disparar
        if (Input.GetButton("Fire1") && Time.time >= tiempoUltimoDisparo + cadenciaDisparo)
        {
            IntentarDisparar();
        }
    }

    private void IntentarDisparar()
    {
        if (balasActuales >= 0)
        {
        tiempoUltimoDisparo = Time.time;

        // Reproducir sonido
        AudioManager.instance.ReproducirDisparo();

        // Sacar proyectil del pool
        GameObject bala = ProyectilPool.Instance.PopObj();
        bala.transform.position = pitorro.position;
        bala.transform.rotation = pitorro.rotation;

        // Aplicar fuerza
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        rb.linearVelocity = pitorro.forward * disparoVelocidad;

        // Una bala menos jejejejejejejejejeje
        balasActuales--;
        ActualizarTextoBalas();
        }

        if (balasActuales <= 0)
        {
            textoBalas.text = "Recarga";
        }

        if (recargando)
        {
            textoBalas.text = "Recargando";
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

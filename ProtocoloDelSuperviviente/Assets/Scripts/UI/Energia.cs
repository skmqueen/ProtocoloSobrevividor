// Energia.cs
using UnityEngine;
using UnityEngine.UI;

public class Energia : MonoBehaviour
{
    public static Energia Instance;
    public Image barraEnergia;
    public float tiempoRecargaEnergia = 4f;

    private bool recargando = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { 
            Destroy(gameObject);
            }
    }

    private void Update()
    {
        if (recargando)
        {
            tiempoRecargaEnergia -= Time.deltaTime;

            if (tiempoRecargaEnergia <= 0)
            {
                barraEnergia.fillAmount = 1f;
                tiempoRecargaEnergia = 4f;

            }
        }
    }

    public void GastarEnergia()
    {
        barraEnergia.fillAmount -= 1f / 4f;

        if (barraEnergia.fillAmount <= 0f)
        {
            barraEnergia.fillAmount = 0f;
            IniciarRecarga();
        }
    }


    public void IniciarRecarga()
    {
        recargando = true;
    }

    public bool TieneEnergia()
    {
        return barraEnergia.fillAmount > 0f && !recargando;
    }
}
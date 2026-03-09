using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimacionesUI : MonoBehaviour
{

    public float duracionEntrada = 0.35f;
    public float duracionClick = 0.08f;
    public float duracionTitulo = 0.5f;


    public LeanTweenType easeEntrada = LeanTweenType.easeOutBack;
    public LeanTweenType easeClick = LeanTweenType.easeInOutSine;
    public LeanTweenType easeTitulo = LeanTweenType.easeOutElastic;


    public void AnimarEntradaBoton(GameObject boton)
    {
        LeanTween.cancel(boton);
        boton.SetActive(true);

        Vector3 escalaReal = boton.transform.localScale;
        boton.transform.localScale = escalaReal * 0.1f; // pequeño pero proporcional

        LeanTween.scale(boton, escalaReal, duracionEntrada)
            .setEase(easeEntrada);
    }


    public void AnimarClickBoton(GameObject boton)
    {
        LeanTween.cancel(boton);

        Vector3 escalaReal = boton.transform.localScale;

        LeanTween.scale(boton, escalaReal * 0.9f, duracionClick)
            .setEase(easeClick)
            .setOnComplete(() =>
            {
                LeanTween.scale(boton, escalaReal, duracionClick)
                    .setEase(easeClick);
            });
    }


    public void AnimarEntradaTitulo(GameObject titulo)
    {
        LeanTween.cancel(titulo);
        titulo.SetActive(true);

        Vector3 escalaReal = titulo.transform.localScale;
        titulo.transform.localScale = escalaReal * 0.1f;

        LeanTween.scale(titulo, escalaReal, duracionTitulo)
            .setEase(easeTitulo);
    }

    private string escenaPendiente;

    public void BotonCambiarEscena(GameObject boton, string nombreEscena)
    {
        AnimarClickBoton(boton);
        escenaPendiente = nombreEscena;

        Invoke(nameof(CargarEscenaInterna), duracionClick * 2);
    }

    private void CargarEscenaInterna()
    {
        SceneManager.LoadScene(escenaPendiente);
    }
}

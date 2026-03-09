using UnityEngine;
using UnityEngine.SceneManagement;

public class DerrotaUI : MonoBehaviour
{
    public AnimacionesUI anim;
    public GameObject titulo;
    public GameObject puntos;
    public GameObject tiempo;
    public GameObject botonJugar;
    public GameObject botonSalir;

    void Start()
    {
        anim.AnimarEntradaTitulo(titulo);
        anim.AnimarEntradaBoton(botonJugar);
        anim.AnimarEntradaBoton(botonSalir);
        anim.AnimarEntradaTitulo(puntos);
        anim.AnimarEntradaTitulo(tiempo);
    }
}

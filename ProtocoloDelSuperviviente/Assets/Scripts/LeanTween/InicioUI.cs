using UnityEngine;
using UnityEngine.SceneManagement;

public class InicioUI : MonoBehaviour
{
    public AnimacionesUI anim;
    public GameObject titulo;
    public GameObject botonJugar;
    public GameObject botonSalir;

    void Start()
    {
        anim.AnimarEntradaTitulo(titulo);

        anim.AnimarEntradaBoton(botonJugar);
        anim.AnimarEntradaBoton(botonSalir);
    }
}

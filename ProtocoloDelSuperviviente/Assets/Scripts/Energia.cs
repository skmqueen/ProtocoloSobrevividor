using UnityEngine;
using UnityEngine.UI;

public class Energia : MonoBehaviour
{
    public static Energia Instance;
    public Image barraEnergia;
    private ArmasController armascontroller;
    private float balasPlayer;
    public float balasEnergia = 4f;
    public float tiempoEnergia = 4f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }

        armascontroller = GameObject.Find("Player").GetComponent<ArmasController>();
    }

    void Start()
    {
        balasPlayer = armascontroller.maxBalas;
    }

    public void energiaFuera()
    {
        balasPlayer = armascontroller.balasActuales;

        barraEnergia.fillAmount = 1;

        if (balasPlayer <= balasEnergia)
        {
        barraEnergia.fillAmount = 0; 
        balasEnergia = 0f;
        }

}

public void ReposicionEnergia()

{
    if (barraEnergia.fillAmount <= 0)
    {
    barraEnergia.fillAmount = 1;
    balasEnergia = 0f;
    }
}
}
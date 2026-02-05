using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image barraVida;
    private Player player;
    private float vidaPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        vidaPlayer = player.vida;
    }

    // Update is called once per frame
    void Update()
    {
        barraVida.fillAmount = player.vida / vidaPlayer;  
    }
}

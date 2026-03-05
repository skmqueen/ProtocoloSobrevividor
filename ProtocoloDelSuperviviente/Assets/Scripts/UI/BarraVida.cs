using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public static BarraVida Instance;
    public Image barraVida;
    private Player player;
    public float vidaPlayer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);        }
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        vidaPlayer = player.vidaPlayer;
    }

    public void Danio()
{
   
    barraVida.fillAmount = player.vida / vidaPlayer;
}

  }

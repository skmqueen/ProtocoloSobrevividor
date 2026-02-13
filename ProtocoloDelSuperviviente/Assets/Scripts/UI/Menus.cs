using UnityEngine;
using UnityEngine.SceneManagement;
public class Menus : MonoBehaviour
{
    public static Menus Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Jugar(string Juego)
    {
        SceneManager.LoadScene(Juego);
    }
    public void Salir()
    {
        Debug.Log("Salir ... ");
        Application.Quit();
    }

    public void Restart(string Juego)
    {
        SceneManager.LoadScene(Juego);
    }

    public void GameOver(string GameOver)
    {
        SceneManager.LoadScene(GameOver);
    }

    public void Victoria(string Victoria)
    {
        SceneManager.LoadScene(Victoria);
    }
}

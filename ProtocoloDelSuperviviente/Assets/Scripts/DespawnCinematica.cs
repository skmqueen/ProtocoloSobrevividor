using UnityEngine;
using UnityEngine.Playables;

public class DespawnCinematica : MonoBehaviour
{

    //Con este script se controlará la cinemática y el gameplay
    
    public PlayableDirector Cine;
    public GameObject CineObj;
    public GameObject GameplayObj;

    private float duration;
    private float t;

    void Start()
    {
        duration = (float)Cine.duration;
        t = 0f;
    }

    void Update()
    {
        t += Time.deltaTime;

        if (t >= duration)
        {
            CineObj.SetActive(false);
            GameplayObj.SetActive(true);
            Destroy(this);
        }
    }
}

using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public bool hover_enlarge = false;
    public bool shake_on_clear = false;

    public bool bgm = false;

    public bool sfx = false;

    public bool color = false;
    public bool background = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hover_enlarge = false;
        sfx = false;
        background = false;
        shake_on_clear = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

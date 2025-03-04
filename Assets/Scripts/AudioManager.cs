using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private EffectManager effects;
    private AudioSource bgm;
    private AudioSource painting;
    private AudioSource dip;

    private AudioSource erase;

    private bool currently_painting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effects = GameObject.Find("Effect Manager").GetComponent<EffectManager>();
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        painting = GameObject.Find("Painting").GetComponent<AudioSource>();
        dip = GameObject.Find("Brush Dip").GetComponent<AudioSource>();
        erase = GameObject.Find("Erase").GetComponent<AudioSource>();
    }

    public void Paint(){
        if(!currently_painting){
            painting.Play(0);
            currently_painting = true;
        }
    }

    public void Dip(){
        dip.Play(0);
    }

    public void StopPainting(){
        if(currently_painting){
            currently_painting = false;
            painting.Pause();
        }
    }

    public void Erase(){
        Debug.Log("erasing");
        erase.Play(0);
    }
    // Update is called once per frame
    void Update()
    {
        if(!effects.bgm){
            bgm.mute = true;
        }
        else{
            bgm.mute = false;
        }

        if(!effects.sfx){
            painting.mute = true;
            dip.mute = true;
            erase.mute = true;
        }
        else{
            painting.mute = false;
            dip.mute = false;
            erase.mute = true;
        }
    }
}

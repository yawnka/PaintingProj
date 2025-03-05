using UnityEngine;
using UnityEngine.UI;
public class BackgroundManager : MonoBehaviour
{
    public Image[] images;

    private EffectManager effects;

    public Button prev_button;
    public Button next_button;

    int current_background = 0;

    bool local_enabled = false;

    void Start(){
        images[0].gameObject.SetActive(false);
        effects = GameObject.Find("Effect Manager").GetComponent<EffectManager>();
        prev_button.GetComponent<Button>().onClick.AddListener(() => prevBackground());
        next_button.GetComponent<Button>().onClick.AddListener(() => nextBackground());
    }
    void prevBackground(){
        images[current_background].gameObject.SetActive(false);
        current_background--;
        if (current_background<0){
            current_background = images.Length-1;
        }
        images[current_background].gameObject.SetActive(true);
    }

    void nextBackground(){
        images[current_background].gameObject.SetActive(false);
        current_background++;
        if (current_background>images.Length-1){
            current_background = 0;
        }
        images[current_background].gameObject.SetActive(true);
    }

    void Update(){
        if (effects.background && local_enabled == false){
            images[0].gameObject.SetActive(true);
            local_enabled = true;
        }
        else if (!effects.background){
            local_enabled = false;
            foreach(Image image in images){
                image.gameObject.SetActive(false);
            }
        }
    }
}

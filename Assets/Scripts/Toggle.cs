using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour
{
    string toggle_type = "BGM";

    public GameObject effectManager;

    private EffectManager effects;

    private Image button_image;

    private ColorModeToggle color_mode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        color_mode = GameObject.Find("ToggleColorButton").GetComponent<ColorModeToggle>();
        effects = effectManager.GetComponent<EffectManager>();
        GetComponent<Button>().onClick.AddListener(() => toggleValue());
        getToggleType();
        button_image = GetComponent<Image>();
        button_image.color = Color.white;
    }

    void getToggleType(){
        string name = this.gameObject.name;
        toggle_type = name.Substring(0, name.IndexOf('_'));
        Debug.Log(toggle_type);
    }

    void toggleValue(){

        if (button_image.color == Color.white){
            button_image.color = Color.green;
        }
        else{
            button_image.color = Color.white;
        }


        switch(toggle_type) 
        {
        case "BGM":
            effects.bgm = !effects.bgm;
            break;
        case "SFX":
            effects.sfx = !effects.sfx;
            break;
        case "Shake":
            effects.shake_on_clear = !effects.shake_on_clear;
            break;
        case "Background":
            effects.background = !effects.background;
            break;
        case "HoverEnlarge":
            effects.hover_enlarge = !effects.hover_enlarge;
            break;
        case "Color":
            effects.color = !effects.color;

            color_mode.ToggleBlackAndWhite(effects.color);
            break;
        default:
            break;
        }
    }
}

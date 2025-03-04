using UnityEngine;
using UnityEngine.UI;
public class BackgroundManager : MonoBehaviour
{
    public Image[] images;

    public Button prev_button;
    public Button next_button;

    int current_background = 0;

    void Start(){
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
}

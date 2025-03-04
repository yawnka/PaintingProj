using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ColorButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Painter painter;
    public Color color;
    public GameObject effect_obj;
    public EffectManager effects;
    
    private Vector3 init_size;

    void Start()
    {
        effect_obj = GameObject.Find("Effect Manager");
        Debug.Log(effect_obj);
        effects = effect_obj.GetComponent<EffectManager>();
        init_size = transform.localScale;
        GetComponent<Button>().onClick.AddListener(() => painter.SetColor(color));
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (effects.hover_enlarge){
            transform.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 1.1f, transform.localScale.z * 1.1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.localScale = init_size;
    }
}
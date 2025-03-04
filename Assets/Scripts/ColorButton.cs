using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ColorButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Painter painter;
    public Color color;
    public GameObject effect_obj;
    public EffectManager effects;
    
    private Color originalColor;
    private bool isBlackAndWhite = true;
    private Vector3 init_size;

    void Start()
    {
        effect_obj = GameObject.Find("Effect Manager");
        effects = effect_obj.GetComponent<EffectManager>();
        init_size = transform.localScale;
        originalColor = color;
        UpdateButtonColor();
        GetComponent<Button>().onClick.AddListener(() => painter.SetColor(originalColor));
    }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (effects.hover_enlarge){
            transform.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 1.1f, transform.localScale.z * 1.1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.localScale = init_size;
    }

    public void ToggleBlackAndWhite(bool isBW)
    {
        isBlackAndWhite = isBW;
        UpdateButtonColor();
    }

    private void UpdateButtonColor()
    {
        Image buttonImage = GetComponent<Image>();
        buttonImage.color = isBlackAndWhite ? ConvertToGrayscale(originalColor) : originalColor;
    }

    private Color ConvertToGrayscale(Color color)
    {
        float gray = (color.r + color.g + color.b) / 3f;
        return new Color(gray, gray, gray);
    }
}

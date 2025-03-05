using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    public Painter painter;
    public Color color;

    private Color originalColor;
    private bool isBlackAndWhite = true;
    private bool hasSound = false;

    void Start()
    {
        originalColor = color;
        UpdateButtonColor();
        GetComponent<Button>().onClick.AddListener(() => painter.SetColor(originalColor));
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

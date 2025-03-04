using UnityEngine;
using UnityEngine.UI;

public class ColorModeToggle : MonoBehaviour
{
    public Painter painter;
    public Button toggleButton;
    private bool isBlackAndWhite = true;
    public ColorButton[] colorButtons;

    void Awake()
    {
        toggleButton.GetComponent<Image>().color = Color.gray;
    }

    void Start()
    {
        if (painter == null)
            painter = FindFirstObjectByType<Painter>();

        painter.ToggleBlackAndWhiteMode(true);

        foreach (var button in colorButtons)
            button.ToggleBlackAndWhite(true);

        toggleButton.onClick.AddListener(ToggleBlackAndWhite);
    }

    void ToggleBlackAndWhite()
    {
        isBlackAndWhite = !isBlackAndWhite;
        toggleButton.GetComponent<Image>().color = isBlackAndWhite ? Color.gray : Color.green;

        painter.ToggleBlackAndWhiteMode(isBlackAndWhite);

        foreach (var button in colorButtons)
            button.ToggleBlackAndWhite(isBlackAndWhite);
    }
}

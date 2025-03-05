using UnityEngine;
using UnityEngine.UI;

public class ColorModeToggle : MonoBehaviour
{
    public ColorButton[] colorButtons;
    public void ToggleBlackAndWhite(bool color)
    {
        foreach (var button in colorButtons)
            button.ToggleBlackAndWhite(color);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    public Painter painter;
    public Color color;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => painter.SetColor(color));
    }
}
using UnityEngine;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour
{
    public GameObject canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Painter painter = canvas.GetComponent<Painter>();
        GetComponent<Button>().onClick.AddListener(() => painter.ClearCanvas());
    }
}

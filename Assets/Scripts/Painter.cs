using UnityEngine;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{
    public RawImage canvasImage;
    public Color selectedColor = Color.black;
    public AudioSource colorSelect;
    public AudioSource brushSound;

    private Texture2D texture;
    private Color[,] pixelColorMap;
    private bool isBlackAndWhite = false;
    private bool hasSound = false;
    private bool isDrawing = false;

    void Awake()
    {
        texture = new Texture2D(512, 512);
        pixelColorMap = new Color[texture.width, texture.height];
        canvasImage.texture = texture;
    }

    void Start()
    {
        ClearCanvas();
        ToggleBlackAndWhiteMode(true);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasImage.rectTransform,
                Input.mousePosition,
                null,
                out localPoint
            );

            Vector2Int pixelUV = ConvertToPixel(localPoint);
            DrawPixel(pixelUV.x, pixelUV.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            brushSound.Stop();
            isDrawing = false;
        }
    }

    Vector2Int ConvertToPixel(Vector2 localPoint)
    {
        Rect rect = canvasImage.rectTransform.rect;
        float x = (localPoint.x - rect.x) / rect.width * texture.width;
        float y = (localPoint.y - rect.y) / rect.height * texture.height;
        return new Vector2Int((int)x, (int)y);
    }

    public void ToggleBlackAndWhiteMode(bool state)
    {
        isBlackAndWhite = state;

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, isBlackAndWhite ? ConvertToGrayscale(pixelColorMap[x, y]) : pixelColorMap[x, y]);
            }
        }

        texture.Apply();
    }

    void DrawPixel(int x, int y)
    {
        int brushSize = 10;
        Color paintColor = selectedColor;

        for (int i = -brushSize / 2; i <= brushSize / 2; i++)
        {
            for (int j = -brushSize / 2; j <= brushSize / 2; j++)
            {
                int newX = Mathf.Clamp(x + i, 0, texture.width - 1);
                int newY = Mathf.Clamp(y + j, 0, texture.height - 1);

                pixelColorMap[newX, newY] = selectedColor;
                texture.SetPixel(newX, newY, isBlackAndWhite ? ConvertToGrayscale(selectedColor) : selectedColor);
            }
        }

        if (hasSound && !(isDrawing))
        {
            isDrawing = true;
            brushSound.Play();
        }

        texture.Apply();
    }

    public void SetColor(Color color)
    {
        if (hasSound)
        {
            colorSelect.Play();
        }
        selectedColor = color;
    }

    public void ClearCanvas()
    {
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, Color.white);
                pixelColorMap[x, y] = Color.white;
            }
        }

        texture.Apply();
    }

    private Color ConvertToGrayscale(Color color)
    {
        float gray = (color.r + color.g + color.b) / 3f;
        return new Color(gray, gray, gray);
    }

    public void ToggleSFX()
    {
        hasSound = !hasSound;
    }
}
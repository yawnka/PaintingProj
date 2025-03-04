using UnityEngine;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{
    public RawImage canvasImage;
    public Color selectedColor = Color.black;

    private Texture2D texture;

    public GameObject cam;
    private CameraShake cam_shake;
    public EffectManager effects;

    public AudioManager audioManager;


    void Start()
    {
        texture = new Texture2D(512, 512);
        canvasImage.texture = texture;

        effects = GameObject.Find("Effect Manager").GetComponent<EffectManager>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        cam_shake = cam.GetComponent<CameraShake>();

        ClearCanvas();
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
        else{
            audioManager.StopPainting();
        }
    }

    Vector2Int ConvertToPixel(Vector2 localPoint)
    {
        Rect rect = canvasImage.rectTransform.rect;
        float x = (localPoint.x - rect.x) / rect.width * texture.width;
        float y = (localPoint.y - rect.y) / rect.height * texture.height;
        return new Vector2Int((int)x, (int)y);
    }

    void DrawPixel(int x, int y)
    {
        if( x < 500 && x > 0 && y < 500 && y > 0){
            audioManager.Paint();
        }
        else{
            audioManager.StopPainting();
        }

        int brushSize = 5;

        for (int i = -brushSize / 2; i <= brushSize / 2; i++)
        {
            for (int j = -brushSize / 2; j <= brushSize / 2; j++)
            {
                int newX = Mathf.Clamp(x + i, 0, texture.width - 1);
                int newY = Mathf.Clamp(y + j, 0, texture.height - 1);
                texture.SetPixel(newX, newY, selectedColor);
            }
        }

        texture.Apply();
    }


    public void SetColor(Color color)
    {
        audioManager.Dip();
        selectedColor = color;
    }

    public void ClearCanvas()
    {
        audioManager.Erase();
        if (effects.shake_on_clear){
            cam_shake.shake_dur = 0.5f;
        }
        for (int x = 0; x < texture.width; x++)
            for (int y = 0; y < texture.height; y++)
                texture.SetPixel(x, y, Color.white);

        texture.Apply();
    }
}

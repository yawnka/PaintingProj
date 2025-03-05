using UnityEngine;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{
    public RawImage canvasImage;
    public Color selectedColor = Color.black;

    public GameObject cam;
    private CameraShake cam_shake;
    public EffectManager effects;

    public AudioManager audioManager;
    public AudioSource colorSelect;
    public AudioSource brushSound;
    public GameObject easel;
    public GameObject color_palette;
    public Sprite palette;
    public Sprite empty;
    public GameObject background;
    
   

    private Texture2D texture;
    private Color[,] pixelColorMap;
    private bool isBlackAndWhite = false;
    private bool hasSound = false;
    private bool isDrawing = false;
    private bool isMoved = false;
    private bool active_bg = false;


    private Vector3 moved_pos;
    private Vector3 begin_pos = Vector3.zero;
    private Vector3 begin_scale = Vector3.one;
    private Vector3 moved_scale;

    private Vector3 easel_moved_pos;
    private Vector3 easel_moved_scale;
    private Vector3 easel_begin_pos;
    private Vector3 easel_begin_scale;

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


        begin_pos = transform.position;
        begin_scale = transform.localScale;
        moved_scale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y + 0.4f, transform.localScale.z);
        moved_pos = new Vector3(1400, 600, 0);


        easel_begin_pos = color_palette.transform.position;
        easel_begin_scale = color_palette.transform.localScale;
        easel_moved_scale = new Vector3(color_palette.transform.localScale.x - 0.5f, color_palette.transform.localScale.y + 1f, color_palette.transform.localScale.z);
        easel_moved_pos = new Vector3(500, 300, 0);

        texture = new Texture2D(512, 512);
        canvasImage.texture = texture;

        effects = GameObject.Find("Effect Manager").GetComponent<EffectManager>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        cam_shake = cam.GetComponent<CameraShake>();

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
        if( x < 500 && x > 0 && y < 500 && y > 0){
            audioManager.Paint();
        }
        else{
            audioManager.StopPainting();
        }

        int brushSize = 5;
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
        audioManager.Dip();
        if (hasSound)
        {
            colorSelect.Play();
        }
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

    private Color ConvertToGrayscale(Color color)
    {
        float gray = (color.r + color.g + color.b) / 3f;
        return new Color(gray, gray, gray);
    }

    public void ToggleSFX()
    {
        hasSound = !hasSound;
    }

    public void ToggleBackground()
    {
        if (active_bg)
        {
            active_bg = false;
            background.SetActive(false);
        }
        else
        {
            background.SetActive(true);
            active_bg = true;
        }
    }

    public void ToggleSize()
    {
        if (!(isMoved))
        {
            transform.position = moved_pos;
            transform.localScale = moved_scale;
            color_palette.transform.position = easel_moved_pos;
            color_palette.transform.localScale = easel_moved_scale;
            easel.SetActive(true);
            color_palette.GetComponent<Image>().sprite = palette;
            isMoved = true;
            print("true");
            print(transform.position);
        }
        else
        {
            transform.position = begin_pos;
            transform.localScale = begin_scale;
            color_palette.transform.position = easel_begin_pos;
            color_palette.transform.localScale = easel_begin_scale;
            easel.SetActive(false);
            color_palette.GetComponent<Image>().sprite = empty;
            isMoved = false;
            print("false");
            print(transform.position);
        }
    }

        
}

using UnityEngine;

public class OutlineSystem : MonoBehaviour
{
    [Header("Outline Settings")]
    [Tooltip("Should the outline be solid or fade out")]
    public bool SolidOutline = false;

    [Tooltip("Strength override multiplier")]
    [Range(0, 10)]
    public float OutlineStrength = 1f;

    [Tooltip("Which layers should this outline system display on")]
    public LayerMask OutlineLayer;

    [Tooltip("What color should the outline be")]
    public Color OutlineColor;

    [Tooltip("How many times should the render be downsampled")]
    [Range(0, 4)]
    public int DownsampleAmount = 2;

    [Tooltip("How big should the outline be")]
    [Range(0.0f, 10.0f)]
    public float OutlineSize = 1.5f;

    [Tooltip("How many times should the blur be performed")]
    [Range(1, 10)]
    public int OutlineIterations = 2;


    [Tooltip("Upscaling of outline texture")]
    [Range(0.1f, 5)]
    public float OutlineUpscale = 1f;

    public Camera MainCamera;

    [Space(10)]
    [Header("Component References - Do not change")]
    private RenderTexture _renTexInput;
    private RenderTexture _renTexRecolor;
    private RenderTexture _renTexDownsample;
    private RenderTexture _renTexBlur;
    private RenderTexture _renTexOut;
    
    public Material BlurMaterial;
    public Material OutlineMaterial;

    //Used to check if the screen size has been changed
    private Vector2 _prevSize;

    void Awake()
    {
        if(MainCamera == null)
        {
            MainCamera = Camera.main;
        }
        UpdateRenderTextureSizes();
    }
    

    void UpdateRenderTextureSizes()
    {
        Vector2 screenDims = ScreenDimension();
        int x = Mathf.FloorToInt(Mathf.FloorToInt(screenDims.x) * OutlineUpscale);
        int y = Mathf.FloorToInt(Mathf.FloorToInt(screenDims.y) * OutlineUpscale);
        _renTexInput = new RenderTexture(x, y, 1);
        _renTexDownsample = new RenderTexture(x, y, 1);
        _renTexRecolor = new RenderTexture(x, y, 1);
        _renTexOut = new RenderTexture(x, y, 1);
        _renTexBlur = new RenderTexture(x, y, 1);
    }

    public Vector2 ScreenDimension()
    {
        Vector2 size = Vector2.one;
        size = new Vector2(Screen.width, Screen.height);
        return size;
    }

    void RunCalcs()
    {

        OutlineMaterial.SetColor("_OutlineCol", OutlineColor);
        OutlineMaterial.SetFloat("_GradientStrengthModifier", OutlineStrength);

        RenderTexture prevRenTex = MainCamera.targetTexture;
        int prevCullGroup = MainCamera.cullingMask;
        CameraClearFlags prevClearFlags = MainCamera.clearFlags;
        Color prevColor = MainCamera.backgroundColor;
        
        MainCamera.cullingMask = OutlineLayer.value;
        MainCamera.targetTexture = _renTexInput;
        MainCamera.clearFlags = CameraClearFlags.SolidColor;
        MainCamera.backgroundColor = new Color(1f, 0f, 1f, 1f);
        
        MainCamera.Render();

        MainCamera.backgroundColor = prevColor;
        MainCamera.clearFlags = prevClearFlags;
        MainCamera.targetTexture = prevRenTex;
        MainCamera.cullingMask = prevCullGroup;


        float widthMod = 1.0f / (1.0f * (1 << DownsampleAmount));
        BlurMaterial.SetVector("_Parameter", new Vector4(OutlineSize * widthMod, -OutlineSize * widthMod, 0.0f, 0.0f));

        Graphics.Blit(_renTexInput, _renTexRecolor, OutlineMaterial, 0);
        Graphics.Blit(_renTexRecolor, _renTexDownsample, BlurMaterial, 0);

        for (int i = 0; i < OutlineIterations; i++)
        {
            float iterationOffs = (i * 1.0f);
            BlurMaterial.SetVector("_Parameter", new Vector4(OutlineSize * widthMod + iterationOffs, -OutlineSize * widthMod - iterationOffs, 0.0f, 0.0f));

            Graphics.Blit(_renTexDownsample, _renTexBlur, BlurMaterial, 1);
            Graphics.Blit(_renTexBlur, _renTexDownsample, BlurMaterial, 2);
        }
        OutlineMaterial.SetFloat("_Solid", SolidOutline ? 1f : 0f);
        OutlineMaterial.SetTexture("_BlurTex", _renTexDownsample);
        Graphics.Blit(_renTexRecolor, _renTexOut, OutlineMaterial, 1);
    }

    void LateUpdate()
    {
        Vector2 currentSize = new Vector2(Screen.width, Screen.height);
        if (_prevSize != currentSize)
        {
            UpdateRenderTextureSizes();
        }
        _prevSize = currentSize;
        RunCalcs();
    }

    void OnGui()
    {
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, Screen.width, Screen.height, 0);
        Graphics.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _renTexOut);
        GL.PopMatrix();
    }
}

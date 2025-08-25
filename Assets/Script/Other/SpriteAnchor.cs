using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[ExecuteAlways]
public class SpriteAnchor : MonoBehaviour
{
    [Header("Включить автоподгонку под экран")]
    public bool useAutoFit = false;
    private Camera cam;
    [HideInInspector]
    public  SpriteRenderer sr;
    private Vector2 vpBL, vpTR;
    private float savedDepth;
    private float initialZ;
    private float savedYPosition;
    private bool inited = false;
    void Awake()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        var sam = GameObject.FindGameObjectWithTag("SpriteAnchorManager").GetComponent<SpriteAnchorManager>();
        sam.Register(this);

    }
    void OnEnable()
    {
        if (useAutoFit)
            InitializeFit();
    }
    float lastScreenWidth;
    float lastScreenHeight;
    void Update()
    {
#if UNITY_EDITOR
        // В редакторе пересчитываем всегда, если включен автоподгон
        if (useAutoFit && !Application.isPlaying)
        {
            InitializeFit();
            return;
        }
#endif
        if (useAutoFit && !inited)
        {
            if(Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
                InitializeFit();
        }
        else if (!useAutoFit)
        {
            savedYPosition = transform.position.y;
            inited = false;
        }
    }
    public void Recalculate()
    {
        if (!useAutoFit || !inited || cam == null || sr == null || sr.sprite == null)
            return;
        Vector3 wBL = cam.ViewportToWorldPoint(new Vector3(vpBL.x, vpBL.y, savedDepth));
        Vector3 wTR = cam.ViewportToWorldPoint(new Vector3(vpTR.x, vpTR.y, savedDepth));
        Vector2 targetSize = new Vector2(wTR.x - wBL.x, wTR.y - wBL.y);
        Vector2 spriteSize = sr.sprite.bounds.size;
        if (spriteSize.x <= float.Epsilon || spriteSize.y <= float.Epsilon)
            return;
        Vector3 ls = transform.localScale;
        ls.x = RoundToPrecision(targetSize.x / spriteSize.x, 5);
        ls.y = RoundToPrecision(targetSize.y / spriteSize.y, 5);
        transform.localScale = ls;
        Vector3 center = (wBL + wTR) * 0.5f;
        transform.position = new Vector3(
            RoundToPrecision(center.x, 5),
            savedYPosition,
            initialZ
        );
    }
    private void InitializeFit()
    {
        if (cam == null || sr == null || sr.sprite == null)
            return;

        initialZ = transform.position.z;

        Bounds b = sr.bounds;
        Vector3 worldMin = new Vector3(b.min.x, b.min.y, initialZ);
        Vector3 worldMax = new Vector3(b.max.x, b.max.y, initialZ);

        Vector3 vMin = cam.WorldToViewportPoint(worldMin);
        Vector3 vMax = cam.WorldToViewportPoint(worldMax);
        vpBL = new Vector2(RoundToPrecision(vMin.x, 5), RoundToPrecision(vMin.y, 5));
        vpTR = new Vector2(RoundToPrecision(vMax.x, 5), RoundToPrecision(vMax.y, 5));

        savedDepth = RoundToPrecision(cam.WorldToViewportPoint(transform.position).z, 5);

        savedYPosition = transform.position.y;

        inited = true;

        Recalculate();
    }
    private float RoundToPrecision(float value, int decimals)
    {
        float multiplier = Mathf.Pow(10f, decimals);
        return Mathf.Round(value * multiplier) / multiplier;
    }
}
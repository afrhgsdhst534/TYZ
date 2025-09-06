using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class SpritePixelSize : MonoBehaviour
{
    public int widthPx = 100;
    public int heightPx = 150;
    public SizingMode sizing = SizingMode.Stretch;
    public Camera cam;
    private SpriteRenderer sr;
    private Sprite lastSprite;
    private int lw, lh;
    public enum SizingMode { Stretch, PreserveAspectFit, PreserveAspectFill }
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void LateUpdate()
    {
            Apply();
    }
    public void SetSprite(Sprite sprite)
    {
        sr.sprite = sprite;
        Apply(); // сразу пересчитаем
    }

   public void Apply()
    {
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        if (cam == null) cam = Camera.main;
        var s = sr.sprite;

        float depth = Vector3.Dot(transform.position - cam.transform.position, cam.transform.forward);
        float worldW, worldH;

        if (cam.orthographic)
        {
            float upp = 2f * cam.orthographicSize / cam.pixelHeight;
            worldW = widthPx * upp;
            worldH = heightPx * upp;
        }
        else
        {
            Vector3 a = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight * 0.5f, depth));
            Vector3 b = cam.ScreenToWorldPoint(new Vector3(widthPx, cam.pixelHeight * 0.5f, depth));
            Vector3 c = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth * 0.5f, 0, depth));
            Vector3 d = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth * 0.5f, heightPx, depth));
            worldW = Vector3.Distance(a, b);
            worldH = Vector3.Distance(c, d);
        }

        Vector2 sp = s.bounds.size;
        if (sp.x <= 0f || sp.y <= 0f) return;

        float sx, sy;

        if (sizing == SizingMode.Stretch)
        {
            if (widthPx == 0 && heightPx == 0) return;
            if (widthPx == 0)
            {
                float k = worldH / sp.y;
                transform.localScale = new Vector3(k, k, 1);
                SyncCollider();
                return;
            }
            if (heightPx == 0)
            {
                float k = worldW / sp.x;
                transform.localScale = new Vector3(k, k, 1);
                SyncCollider();
                return;
            }
            sx = worldW / sp.x;
            sy = worldH / sp.y;
        }
        else
        {
            float kx = widthPx > 0 ? worldW / sp.x : float.PositiveInfinity;
            float ky = heightPx > 0 ? worldH / sp.y : float.PositiveInfinity;
            float k = sizing == SizingMode.PreserveAspectFit ? Mathf.Min(kx, ky) : Mathf.Max(kx, ky);
            if (float.IsInfinity(k))
            {
                k = float.IsInfinity(kx) ? ky : kx;
                if (float.IsInfinity(k)) return;
            }
            sx = sy = k;
        }

        transform.localScale = new Vector3(sx, sy, 1);
        lastSprite = s;
        lw = widthPx;
        lh = heightPx;
        SyncCollider();
    }

    void SyncCollider()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col == null || sr.sprite == null) return;

        if (col is BoxCollider2D box)
            box.size = sr.sprite.bounds.size;
        else if (col is CircleCollider2D circle)
            circle.radius = sr.sprite.bounds.extents.magnitude;
        else if (col is PolygonCollider2D poly)
        {
            poly.pathCount = sr.sprite.GetPhysicsShapeCount();
            var shape = new System.Collections.Generic.List<Vector2>();
            for (int i = 0; i < poly.pathCount; i++)
            {
                shape.Clear();
                sr.sprite.GetPhysicsShape(i, shape);
                poly.SetPath(i, shape);
            }
        }
    }
}

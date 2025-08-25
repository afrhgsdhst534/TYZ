using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteScale : MonoBehaviour
{
    public float targetWidth = 1f;
    public float targetHeight = 1f;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr.sprite == null) return;

        float spriteWidth = sr.sprite.rect.width;
        float spriteHeight = sr.sprite.rect.height;
        float pixelsPerUnit = sr.sprite.pixelsPerUnit;

        float widthUnits = spriteWidth / pixelsPerUnit;
        float heightUnits = spriteHeight / pixelsPerUnit;

        float scaleX = targetWidth / widthUnits;
        float scaleY = targetHeight / heightUnits;

        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
}

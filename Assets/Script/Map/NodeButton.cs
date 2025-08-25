using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class NodeButton : MonoBehaviour, IPointerClickHandler
{
    public int row, col;
    private MapGenerator map;
    public MapGenerator.NodeType type;
    private SpriteRenderer sr;
    private bool
        interactable;
    public bool isPassed { get; private set; } = false;
    public List<NodeButton> connectedNodes = new List<NodeButton>();
    [HideInInspector] public bool isVisible;
    void Awake() { if (sr == null) sr = GetComponent<SpriteRenderer>(); }
    public void SetVisible(bool state) { isVisible = state; sr.enabled = state; }
    public void Setup(int r, int c, MapGenerator.NodeType nodeType, MapGenerator mapRef, Sprite sprite)
    {
        row = r; col = c; type = nodeType; map = mapRef;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprite; SetInteractable(false);
    }
    public void SetInteractable(bool value)
    {
        interactable = value;
        if (isPassed) sr.color = Color.green;
        else sr.color = value ? Color.white : new Color(0.5f, 0.5f, 0.5f, 1f);
    }
    public void MarkAsPassed()
    {
        isPassed = true; sr.color = Color.green; interactable = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable ) return;
        map.SelectNode(row, col);
    }
}
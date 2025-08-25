using UnityEngine;
public class MapGenerator : MonoBehaviour
{
    [Header("Настройки карты")]
    public int rows = 8;
    public int columns = 5;
    public GameObject nodePrefab;
    public float xSpacing = 2f;
    public float ySpacing = 2f;
    [Header("Спрайты узлов")]
    public Sprite barSprite, monsterSprite, superMonsterSprite, shopSprite, chestSprite, bossSprite;
    [Header("Шансы выпадения (сумма = 100)")]
    [Range(0, 100)] public int barChance = 10;
    [Range(0, 100)] public int monsterChance = 40;
    [Range(0, 100)] public int superMonsterChance = 15;
    [Range(0, 100)] public int shopChance = 15;
    [Range(0, 100)] public int chestChance = 20;
    private NodeButton[,] nodes;
    public enum NodeType { Bar, Monster, SuperMonster, Shop, Chest, Boss }
    private NodeButton currentNode;
    public float x;
    public float y;
    public int extraVision = 1; void Start()
    {
        GenerateMap();
        MoveAllNodes(new Vector2(x, y));
    }
    void GenerateMap()
    {
        nodes = new NodeButton[rows, columns];
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                GameObject nodeObj = Instantiate(nodePrefab, transform);
                nodeObj.transform.position = new Vector2(c * xSpacing, r * ySpacing);
                NodeButton node = nodeObj.GetComponent<NodeButton>();
                NodeType type = (r == rows - 1) ? NodeType.Boss : RollNodeType();
                node.Setup(r, c, type, this, GetSprite(type));
                nodes[r, c] = node;
            }
        }
        for (int c = 0; c < columns; c++)
        {
            nodes[0, c].gameObject.SetActive(true);
            nodes[0, c].SetInteractable(true);
        }
    }
    public void MoveAllNodes(Vector2 offset)
    {
        foreach (var node in nodes)
        {
            if (node != null) node.transform.position += (Vector3)offset;
        }
    }
    public void UpdateVisibility(NodeButton currentNode)
    {
        foreach (var node in nodes) node.SetVisible(false);
        currentNode.SetVisible(true);
        foreach (var neighbor in currentNode.connectedNodes)
        {
            neighbor.SetVisible(true);
            ShowExtraVision(neighbor, extraVision - 1);
        }
    }
    private void ShowExtraVision(NodeButton node, int depth)
    {
        if (depth <= 0) return;
        foreach (var neighbor in node.connectedNodes)
        {
            if (!neighbor.isVisible)
            {
                neighbor.SetVisible(true);
                ShowExtraVision(neighbor, depth - 1);
            }
        }
    }
    NodeType RollNodeType()
    {
        int roll = Random.Range(0, 100);
        if (roll < barChance) return NodeType.Bar;
        roll -= barChance;
        if (roll < monsterChance) return NodeType.Monster;
        roll -= monsterChance;
        if (roll < superMonsterChance) return NodeType.SuperMonster;
        roll -= superMonsterChance;
        if (roll < shopChance) return NodeType.Shop;
        roll -= shopChance;
        return NodeType.Chest;
    }
    Sprite GetSprite(NodeType type)
    {
        switch (type)
        {
            case NodeType.Bar: return barSprite;
            case NodeType.Monster: return monsterSprite;
            case NodeType.SuperMonster: return superMonsterSprite;
            case NodeType.Shop: return shopSprite;
            case NodeType.Chest: return chestSprite;
            case NodeType.Boss: return bossSprite;
        }
        return null;
    }
    public void SelectNode(int row, int col)
    {
        currentNode = nodes[row, col];

        // перекрашиваем всю строку
        for (int c = 0; c < nodes.GetLength(1); c++)
        {
            nodes[row, c].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }

        currentNode.MarkAsPassed();
        currentNode.gameObject.SetActive(true);

        if (row + 1 < rows)
        {
            for (int offset = -1; offset <= 1; offset++)
            {
                int nextCol = col + offset;
                if (nextCol >= 0 && nextCol < columns)
                {
                    nodes[row + 1, nextCol].SetInteractable(true);
                }
            }
        }
    }
}
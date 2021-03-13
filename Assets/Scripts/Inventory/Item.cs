using UnityEngine;
using System.Collections;
using UnityEditor;

public class Item : MonoBehaviour, IInventoryItem, ISellableItem
{

    [SerializeField] private Vector2Int originPos;
    [SerializeField] private Vector2Int size;
    private Vector2Int[,] reletivePositions;

    [TextArea][SerializeField]
    private string info;

    [SerializeField]
    private float cost;

    public Inventory inventory;

    public Vector2Int OriginPos { get => originPos; set => originPos = value; }
    public Vector2Int Size { get => size; set => size = value; }
    public Vector2Int[,] ReletivePositions { get => reletivePositions; set => reletivePositions = value; }

    [HideInInspector]
    public SpriteRenderer spriteRenderer;


    public SoundManager soundManager;

    public Vector2Int[,] GetGloablPositions()
    {
        Vector2Int[,] res = new Vector2Int[Size.x, Size.y];

        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                res[x, y] = OriginPos + ReletivePositions[x, y];
            }
        }

        return res;
    }

    private void SetReletivePositions()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                ReletivePositions[x, y] = new Vector2Int(x, y);
            }
        }
    }

    public void RotateItem(int deg)
    {
        deg *= -1;
        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                Vector2Int pos = ReletivePositions[i, j];
                int x = Mathf.RoundToInt(Mathf.Cos(deg) * pos.x + Mathf.Sin(deg) * pos.y);
                int y = Mathf.RoundToInt(-Mathf.Sin(deg) * pos.x + Mathf.Cos(deg) * pos.y);
                pos.Set(x, y);

                ReletivePositions[i, j] = pos;
            }
        }
    }

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ReletivePositions = new Vector2Int[Size.x, Size.y];
        SetReletivePositions();

       
        if(inventory != null)
        {
            originPos = inventory.WorldToCell(transform.position);
            transform.position = inventory.CellCenter(originPos);
        }
        else
        {
            Debug.LogError("Inventory of item is not assinged" + name);
        }
        
    }

    public void Start()
    {
        soundManager = GameStateController.gameStateController.soundManager;
    }

    public void SetGlobalPos(Vector2Int pos)
    {
        transform.position = inventory.CellCenter(pos);
    }
    public void SetGlobalPos(Vector2Int pos, float pickUpAmmount)
    {
        transform.position = inventory.CellCenter(pos);
        transform.position += new Vector3(0, pickUpAmmount);
    }


    public void Translate(Vector2Int pos)
    {
        OriginPos += pos;
    }

    public string GetInfo()
    {
        return info;
    }

    public float GetCost()
    {
        return cost;
    }

    public void OnItemSell(Player p)
    {
        p.AddGold(GetCost());
    }

    
}

//[CustomEditor(typeof(Item))]
//public class ExampleEditor : UnityEditor.Editor
//{
//    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
//    {
//        Item item = (Item)target;

//        if (item == null || item.icon == null)
//            return null;

//        // example.PreviewIcon must be a supported format: ARGB32, RGBA32, RGB24,
//        // Alpha8 or one of float formats
//        Texture2D tex = new Texture2D(width, height);
//        EditorUtility.CopySerialized(item.icon, tex);

//        return tex;
//    }
//}

using UnityEngine;

public class TileGridGenerator : MonoBehaviour
{
    [Header("Tile")]
    [SerializeField] private GameObject tilePrefab;

    [Header("Grid Size")]
    [Min(1)]
    [SerializeField] private int width = 10;

    [Min(1)]
    [SerializeField] private int length = 10;

    [Header("Position")]
    [SerializeField] private Vector3 startPosition = Vector3.zero;

    [Header("Spacing")]
    [SerializeField] private Vector2 tileSpacing = Vector2.one;

    [Header("Grid Container")]
    [SerializeField] private Transform generatedParent;

    [ContextMenu("Generate Grid")]
    public void GenerateGrid()
    {
        if (tilePrefab == null)
        {
            Debug.LogWarning("Tile Prefab chưa được gán!");
            return;
        }

        // Nếu chưa có parent thì dùng object hiện tại
        Transform parent = generatedParent != null
            ? generatedParent
            : transform;

        // Xóa tile cũ
        ClearGrid();

        // Tạo grid
        for (int z = 0; z < length; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 position = new Vector3(
                    startPosition.x + x * tileSpacing.x,
                    startPosition.y + z * tileSpacing.y

                );

                GameObject tile = Instantiate(
                    tilePrefab,
                    position,
                    Quaternion.identity,
                    parent
                );

                tile.name = $"Tile_{x}_{z}";
            }
        }
    }

    [ContextMenu("Clear Grid")]
    public void ClearGrid()
    {
        Transform parent = generatedParent != null
            ? generatedParent
            : transform;

        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(parent.GetChild(i).gameObject);
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class TilesManager : MonoBehaviour
{
    public Tile[] tiles;
    private void OnEnable()
    {
        EventBus.ResetGame += SetUpGrid;
    }

    private void OnDisable()
    {
        EventBus.ResetGame -= SetUpGrid;
    }
    public void SetUpGrid()
    {
        foreach (Tile tile in tiles)
        {
            tile.gameObject.SetActive(true);
            tile.OnSetUp();
        }
    }
}

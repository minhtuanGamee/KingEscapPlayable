using UnityEngine;

public class TilesManager : MonoBehaviour
{
    public Tile[] tiles; 



    public void SetUpGrid()
    {
        foreach (Tile tile in tiles)
        {
            tile.OnSetUp();
        }
    }
}

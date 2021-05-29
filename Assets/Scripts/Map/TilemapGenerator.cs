using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    [SerializeField] private int _currentMapWidth;
    [SerializeField] private TileBase _ground;
    
    private Tilemap _map;
    private Tilemap Map
    {
        get
        {
            if (_map == null) _map = GetComponent<Tilemap>();
            return _map;
        }
    }

    public void Generate(int mapWidth)
    {
        _currentMapWidth = mapWidth;
        
        for (var x = 0; x < _currentMapWidth; x++)
        for (var y = -3; y < 0; y++)
        {
            Map.SetTile(new Vector3Int(x, y, 0), _ground);
        }
    }

    public void Clear()
    {
        for (var x = 0; x < _currentMapWidth; x++)
        for (var y = -3; y < 0; y++)
        {
            Map.SetTile(new Vector3Int(x, y, 0), null);
        }
    }
}
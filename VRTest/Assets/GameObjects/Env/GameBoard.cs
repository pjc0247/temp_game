using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left, Right, Down, Up, X
}

public class GameBoard : MonoBehaviour {
    public static GameBoard instance { get; set; }

    public string mapname = "map1";
    public int width = 10;
    public int height = 10;

    public GameObject boardMesh;

    public Direction[,] board;
    public Tile[,] tiles;

    private Tile highligtedTile;

    public GameBoard()
    {
        instance = this;
    }

    void LoadMapfile(string name)
    {
        var text = Resources.Load<TextAsset>("Map/" + name).text;

        var lines = text.Split('\n');
        var idx = 0;
        board = new Direction[width, height];
        tiles = new Tile[width, height];
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line) || line.Length < width)
                break;
            for (int i = 0; i < width; i++)
            {
                if (line[i] == 'R') board[idx, i] = Direction.Right;
                else if (line[i] == 'L') board[idx, i] = Direction.Left;
                else if (line[i] == 'U') board[idx, i] = Direction.Up;
                else if (line[i] == 'D') board[idx, i] = Direction.Down;
                else board[idx, i] = Direction.X;
            }
            idx ++;
        }
    }

    void Awake()
    {
        LoadMapfile(mapname);

        var tilePrefab = Resources.Load<GameObject>("Env/Tileset/Prefabs/TilePrefab");
        var pivot = new Vector3(0.1f * width / 2 - 0.05f, 0, 0.1f * height / 2 - 0.05f);
        var y = 0.51f;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var tile = Instantiate<GameObject>(tilePrefab);
                var tileComp = tile.AddComponent<Tile>();
                tile.transform.SetParent(boardMesh.transform);
                tile.transform.localPosition = new Vector3(i * 0.1f, y, j * 0.1f) - pivot;
                tileComp.x = i; tileComp.y = j;

                if (board[j, i] == Direction.X)
                    tileComp.type = TileType.Normal;
                else
                    tileComp.type = TileType.Path;

                tiles[j, i] = tileComp;
            }
        }
        
    }
    
    public GameObject AddObject(GameObject prefab)
    {
        var obj = Instantiate(prefab);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = new Vector3(0, 0.0f, 0);
        return obj;
    }

    public void HighlightTile(Tile tile)
    {
        if (highligtedTile != tile)
        {
            foreach (var t in tiles)
            {
                if (t == tile)
                    t.AnimateOpacity(1.0f);
                else
                    t.AnimateOpacity(0.5f);
            }
        }

        highligtedTile = tile;
    }
    public void UnhighlightTile()
    {
        highligtedTile = null;

        foreach (var t in tiles)
            t.AnimateOpacity(1.0f);
    }
    public Tile GetTileFromPosition(Vector3 position)
    {
        var relative = position - transform.position;

        return tiles[(int)Mathf.Floor(relative.z + 5), (int)Mathf.Floor(relative.x + 5)];
    }
}

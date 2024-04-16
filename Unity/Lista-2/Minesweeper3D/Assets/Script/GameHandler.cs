using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private SelectSystem selectSystem;
    [SerializeField] private TileManager tileManager;
    
    public int mineCount = 5;

    [SerializeField] private Material Tile1;
    [SerializeField] private Material Tile2;
    [SerializeField] private Material Tile3;
    [SerializeField] private Material Tile4;
    [SerializeField] private Material Tile5;
    [SerializeField] private Material Tile6;
    [SerializeField] private Material Tile7;
    [SerializeField] private Material Tile8;
    [SerializeField] private Material TileEmpty;
    [SerializeField] private Material TileExploded;
    [SerializeField] private Material TileFlag;
    [SerializeField] private Material TileMine;
    [SerializeField] private Material TileDefault;

    private GameObject[,] tiles;
    private bool flaggingMode = false;
    
    private bool gameOver = false;
    private bool win = false;

    [SerializeField]
    private Canvas canvas;

    [SerializeField] private TextMeshProUGUI textMeshPro;
    
    
    // Debug
    public Vector2Int selectedTile;

    void Start()
    {
        tiles = tileManager.tiles;

        GenerateMines();
        GenerateNumbers();
    }

    void Update()
    {
        checkWinCondition();
            
        selectedTile = selectSystem.selectedPosition;
        if (gameOver)
        {
            GameOver();
        }
        toggleFlagging();
        if (selectedTile != new Vector2Int(-1, -1) && selectedTile != null)
        {
            HandleTileSelection(selectedTile);

        }
        MaterialUpdate();
    }

    public void toggleFlagging()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flaggingMode = !flaggingMode;
        }
    }
    public void HandleTileSelection(Vector2Int selectedTile)
    {
        if (gameOver)
        {
            return; // Gra już zakończona, nie reaguj na wybór kafelka.
        }
        
        int x = selectedTile.x;
        int y = selectedTile.y;

        if (flaggingMode)
        {
            Flag(x, y);
        }
        else
        {
            Reveal(x, y);
        }

        selectSystem.selectedPosition = new Vector2Int(-1, -1);
    }
    
    private void Explode()
    {
        Debug.Log("Game Over");
        gameOver = true;
        
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                GameObject tileObj = tiles[x, y];
                Tile tile = tileObj.GetComponent<Tile>();

                if (tile.type == Tile.Type.Mine)
                {
                    tile.exploded = true;
                    tile.revealed = true;
                }
            }
        }
    }

    
    private void checkWinCondition()
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                GameObject tileObj = tiles[x, y];
                Tile tile = tileObj.GetComponent<Tile>();

                if (tile.type != Tile.Type.Mine && !tile.revealed)
                {
                    return;
                }
            }
        }

        Debug.Log("You won");
        gameOver = true;
        win = true;

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                GameObject tileObj = tiles[x, y];
                Tile tile = tileObj.GetComponent<Tile>();

                tile.flagged = false;
                tile.revealed = true;
            }
        }
    }

    private void GameOver()
    {
        if (win)
        {
            textMeshPro.text = "You WON!";
        }
        else
        {
            textMeshPro.text  = "You Lost!";
        }
        
        canvas.enabled = true;
    }
    private void GenerateMines()
    {
        for (int i = 0; i < mineCount; i++)
        {
            int x = Random.Range(0, 9);
            int y = Random.Range(0, 9);

            GameObject tile = tileManager.tiles[x, y];
            Tile tileComponent = tile.GetComponent<Tile>();

            if (tileComponent != null)
            {
                tileComponent.type = Tile.Type.Mine;
            }
        }
    }

    private void GenerateNumbers()
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                GameObject tileObj = tiles[x, y];
                Tile tile = tileObj.GetComponent<Tile>();

                if (tile.type == Tile.Type.Mine)
                {
                    continue;
                }

                tile.number = CountMines(x, y);

                if (tile.number > 0)
                {
                    tile.type = Tile.Type.Number;
                }
            }
        }
    }

    private int CountMines(int tileX, int tileY)
    {
        int count = 0;

        for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
        {
            for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
            {
                if (adjacentX == 0 && adjacentY == 0)
                {
                    continue;
                }

                int x = tileX + adjacentX;
                int y = tileY + adjacentY;

                if (x < 0 || x >= 10 || y < 0 || y >= 10)
                {
                    continue;
                }

                GameObject tileObj = tiles[x, y];
                Tile tile = tileObj.GetComponent<Tile>();

                if (tile.type == Tile.Type.Mine)
                {
                    count++;
                }
            }
        }

        return count;
    }

    private void Flag(int x, int y)
    {
        GameObject tileObj = tiles[x, y];
        Tile tile = tileObj.GetComponent<Tile>();

        if (tile.revealed)
        {
            return;
        }

        flaggingMode = false;
        tile.flagged = !tile.flagged;
    }

    private void Reveal(int x, int y)
    {
        GameObject tileObj = tiles[x, y];
        Tile tile = tileObj.GetComponent<Tile>();

        if (tile.revealed || tile.flagged)
        {
            return;
        }

        if (tile.type == Tile.Type.Empty)
        {
            Flood(x, y);
        }
        
        if (tile.type == Tile.Type.Mine)
        {
            Explode();
        }

        tile.revealed = true;
    }

    private void Flood(int x, int y)
    {
        if (x < 0 || x >= 10 || y < 0 || y >= 10)
        {
            return;
        }

        GameObject tileObj = tiles[x, y];
        Tile tile = tileObj.GetComponent<Tile>();

        if (tile.revealed) return;
        if (tile.type == Tile.Type.Mine) return;

        tile.revealed = true;
        if (tile.type == Tile.Type.Empty)
        {
            Flood(x - 1, y);
            Flood(x + 1, y);
            Flood(x, y - 1);
            Flood(x, y + 1);
        }
    }

    private void MaterialUpdate()
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                GameObject tileObj = tiles[x, y];
                Tile tile = tileObj.GetComponent<Tile>();
                
                Renderer rend = tileObj.GetComponent<Renderer>();
                
                if (tile.flagged)
                {
                    rend.material = TileFlag;
                    continue;
                }

                if (tile.revealed)
                {
                    switch (tile.type)
                    {
                        case Tile.Type.Number:
                            rend.material = GetNumberMaterial(tile);
                            break;
                        case Tile.Type.Empty:
                            rend.material = TileEmpty;
                            break;
                        case Tile.Type.Mine:
                            if (tile.exploded) rend.material = TileExploded;
                            else rend.material = TileMine;
                            break;
                        default:
                            rend.material = TileDefault;
                            break;
                    }
                    continue;
                }
                
                rend.material = TileDefault;
            }
        }
    }

    private Material GetNumberMaterial(Tile tile)
    {
        switch (tile.number)
        {
            case 1: return Tile1;
            case 2: return Tile2;
            case 3: return Tile3;
            case 4: return Tile4;
            case 5: return Tile5;
            case 6: return Tile6;
            case 7: return Tile7;
            case 8: return Tile8;
            default: return TileDefault;
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [Header("Controler Components")]
    [SerializeField] private SelectSystem selectSystem;
    [SerializeField] private TileManager tileManager;
    [SerializeField] private FlagManager flagManager;

    [Header("Game Settings")]
    public int mineCount = 5;

    [Header("Tile Settings")]
    [SerializeField] private Material[] numberMaterials;
    [SerializeField] private Material[] specialTileMaterials;
    [SerializeField] private Material TileDefault;


    [Header("Canvas Settings")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    [Header("Sound")]
    [SerializeField] private AudioSource selectTileSoundEffect;
    [SerializeField] private AudioSource explosionSoundEffect;
    [SerializeField] private AudioSource destroyFlagSoundEffect;
    [SerializeField] private AudioSource musicSound;

    private GameObject[,] tiles;
    private bool gameOver = false;
    private bool win = false;
    private bool firstMove = true;

    [SerializeField] private GameObject explosionParticlePrefab;

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
        CheckWinCondition();
            
        selectedTile = selectSystem.selectedPosition;
        selectSystem.selectedPosition = new Vector2Int(-1, -1);

        if (gameOver)
        {
            GameOver();
        }
        
        if (selectedTile != new Vector2Int(-1, -1) && !gameOver && selectedTile != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                HandleTileSelection(selectedTile);
            }

            if(Input.GetMouseButtonDown(1))
            {
                Flag(selectedTile);
            }
        }
        
        MaterialUpdate();
    }

    
    public void HandleTileSelection(Vector2Int selectedTile)
    {
        int x = selectedTile.x;
        int y = selectedTile.y;

        Reveal(x, y);
    }
    
    private void LoseHandler()
    {
        Debug.Log("Game Over");
        gameOver = true;
    }

    private IEnumerator ExplodeWave(int x, int y)
    {
        GameObject tileObj = tiles[x, y];
        Tile tile = tileObj.GetComponent<Tile>();

        explosionSoundEffect.Play();
        Instantiate(explosionParticlePrefab, tileObj.transform.position, Quaternion.identity);
        
        tile.exploded = true;
        tile.flagged = false;
        tile.revealed = true;
        yield return new WaitForSeconds(1f);
        
        for (int i = tiles.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if(j == x && i == y)
                {
                    continue;
                }

                tileObj = tiles[j, i];
                tile = tileObj.GetComponent<Tile>();

                if (tile.type == Tile.Type.Mine)
                {
                    Instantiate(explosionParticlePrefab, tileObj.transform.position, Quaternion.identity);

                    explosionSoundEffect.Play();
                    flagManager.DestroyFlag(j, i);
                    
                    tile.exploded = true;
                    tile.flagged = false;
                    tile.revealed = true;
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }

    // Wywołanie funkcji ExplodeWithDelay zamiast Explode
    private void Explode(int x, int y)
    {
        musicSound.Stop();
        StartCoroutine(ExplodeWave(x, y));
    }
    
    private void CheckWinCondition()
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                GameObject tileObj = tiles[x, y];
                Tile tile = tileObj.GetComponent<Tile>();

                if (tile.type == Tile.Type.Mine && !tile.flagged)
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
        textMeshPro.text = win ? "You WON!" : "You Lost!";
        canvas.gameObject.SetActive(true);
    }

    private void ClearMap()
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                GameObject tileObj = tiles[x, y];
                Tile tile = tileObj.GetComponent<Tile>();

                tile.type = Tile.Type.Empty;
            }
        }
    }
    
    private void GenerateMines()
    {
        HashSet<Vector2Int> usedCoordinates = new HashSet<Vector2Int>(); 
        float[,] noiseMap = GenerateNoiseMap(); // Funkcja generująca Perlin Noise

        for (int i = 0; i < mineCount; i++)
        {
            Vector2Int newCoordinates = GetRandomCoordinatesWithNoise(noiseMap);

            while (usedCoordinates.Contains(newCoordinates))
            {
                newCoordinates = GetRandomCoordinatesWithNoise(noiseMap);
            }

            usedCoordinates.Add(newCoordinates);

            GameObject tile = tileManager.tiles[newCoordinates.x, newCoordinates.y];
            Tile tileComponent = tile.GetComponent<Tile>();
            tileComponent.type = Tile.Type.Mine;
        }
    }

    private float[,] GenerateNoiseMap()
    {
        int width =  tiles.GetLength(0);
        int height = tiles.GetLength(1); 
        float scale = 0.1f;

        float[,] noiseMap = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float sampleX = x * scale;
                float sampleY = y * scale;

                // Use Mathf.PerlinNoise to generate noise values between 0 and 1
                noiseMap[x, y] = Mathf.PerlinNoise(sampleX, sampleY);
            }
        }

        return noiseMap;
    }

    private Vector2Int GetRandomCoordinatesWithNoise(float[,] noiseMap)
    {

        float someScaleFactor = 1.0f;

        int x = Random.Range(0, 9);
        int y = Random.Range(0, 9);

        // Dodaj wpływ Perlin Noise na współrzędne
        x = Mathf.FloorToInt(x + noiseMap[x, y] * someScaleFactor);
        y = Mathf.FloorToInt(y + noiseMap[x, y] * someScaleFactor);

        // Upewnij się, że współrzędne są w zakresie planszy
        x = Mathf.Clamp(x, 0, 8);
        y = Mathf.Clamp(y, 0, 8);

        return new Vector2Int(x, y);
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

    private void Flag(Vector2Int selectedTile)
    {
        int x = selectedTile.x;
        int y = selectedTile.y;
        
        GameObject tileObj = tiles[x, y];
        Tile tile = tileObj.GetComponent<Tile>();

        if (tile.revealed)
        {
            return;
        }

        if(tile.flagged)
        {
            flagManager.DestroyFlag(x, y);
            destroyFlagSoundEffect.Play();
        }
        else
        {
            flagManager.SpawnFlag(x, y);
        }
        
        tile.flagged = !tile.flagged;
    }

    private void Reveal(int x, int y)
    {
        GameObject tileObj = tiles[x, y];
        Tile tile = tileObj.GetComponent<Tile>();

        while(firstMove && tile.type != Tile.Type.Empty)
        {
            ClearMap();
            GenerateMines();
            GenerateNumbers();
            
            tileObj = tiles[x, y];
            tile = tileObj.GetComponent<Tile>();
        }

        firstMove = false;

        if (tile.revealed || tile.flagged)
        {
            return;
        }
        
        if (tile.type == Tile.Type.Mine)
        {
            Explode(x, y);
            LoseHandler();
        }

        if (tile.type == Tile.Type.Empty || tile.type == Tile.Type.Number)
        {
            selectTileSoundEffect.Play();
            Flood(x, y);
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

        if (tile.revealed || tile.flagged) return;
        if (tile.type == Tile.Type.Mine) return;

        tile.revealed = true;
        if (tile.type == Tile.Type.Empty)
        {
            Flood(x - 1, y);
            Flood(x + 1, y);
            Flood(x, y - 1);
            Flood(x, y + 1);

            Flood(x - 1, y + 1);
            Flood(x - 1, y - 1);
            Flood(x + 1, y + 1);
            Flood(x + 1, y - 1);
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
                    rend.material = TileDefault;  // Assuming TileFlag is at index 0
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
                            rend.material = specialTileMaterials[1];  // Assuming TileEmpty is at index 1
                            break;
                        case Tile.Type.Mine:
                            rend.material = tile.exploded ? specialTileMaterials[2] : specialTileMaterials[3];  // Assuming TileExploded is at index 2 and TileMine is at index 3
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
        if (tile.number >= 1 && tile.number <= 8)
        {
            return numberMaterials[tile.number - 1];
        }
        
        return TileDefault;
    }

    public bool isLose(){
        return gameOver && !win;
    }

    public bool isWin()
    {
        return win;
    }
}

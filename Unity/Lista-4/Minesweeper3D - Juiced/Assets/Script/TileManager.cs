using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Vector2 gridSize = new Vector2(-4f, 5f);
    [SerializeField] private float spacing = 1;

    public GameObject[,] tiles; // Tablica dwuwymiarowa do przechowywania kafelków

    void Start()
    {
        GenerateObjects();
    }

    void GenerateObjects()
    {
        int rows = Mathf.CeilToInt((gridSize.y - gridSize.x) / spacing) + 1;
        int columns = Mathf.CeilToInt((gridSize.y - gridSize.x) / spacing) + 1;
        
        tiles = new GameObject[rows, columns];

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                Vector3 spawnPosition = new Vector3(gridSize.x + row * spacing, 0.005f, gridSize.x + column * spacing);
                GameObject newTile = Instantiate(tilePrefab, spawnPosition, tilePrefab.transform.rotation);

                newTile.transform.SetParent(transform); 
                newTile.name = "Tile (" + row + ", " + column + ")";
                
                tiles[row, column] = newTile; // Przypisanie kafelka do tablicy
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSystem : MonoBehaviour
{
    [SerializeField] 
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField] 
    private InputManager inputManager;
    [SerializeField] 
    private Grid grid;
    [SerializeField] 
    private CameraRotation sceneCamera;

    public Vector2Int selectedPosition;

    void Start()
    {
       selectedPosition = new Vector2Int(-1,-1);
    }
    
    void Update()
    {
        Vector3 mousePosition;
        Vector3Int gridPosition;

        if(inputManager.isMouseOverMap()){
            mousePosition = inputManager.GetSelectedMapPostion();
            gridPosition = grid.WorldToCell(mousePosition);
            
            mouseIndicator.transform.position = mousePosition;
            cellIndicator.transform.position = grid.CellToWorld(gridPosition);
            
            selectedPosition = ConvertGridPosition(gridPosition);
        } 
        else 
        {
            mouseIndicator.transform.position = new Vector3(-1000, -1000, -1000);
            cellIndicator.transform.position = new Vector3(-1000, -1000, -1000);
        }
    }

    
    private Vector2Int ConvertGridPosition(Vector3Int vector3)
    {
        return new Vector2Int(vector3.x + 5, vector3.z + 5);
    }
}

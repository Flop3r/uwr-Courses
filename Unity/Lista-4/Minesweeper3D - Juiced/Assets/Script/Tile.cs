using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Type
    {
        Empty,
        Number,
        Mine
    }

    public Type type;
    public int number;
    public bool revealed;
    public bool flagged;
    public bool exploded;
    
    public Tile()
    {
        type = Type.Empty;
    }
    
}
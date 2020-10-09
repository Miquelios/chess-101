using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{

    public GameObject piece;
    public Vector2 originalPosition;
    public Vector2 deployPosition;

    public abstract List<Tile> getPosibleTiles(List<Tile> tiles);

}

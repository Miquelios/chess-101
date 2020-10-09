using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Knight : Piece
{
    public override List<Tile> getPosibleTiles(List<Tile> tiles)
    {
        List<Tile> possibleTiles = new List<Tile>();

        foreach (Tile tile in tiles)
        {
            if (checkTiles(this.originalPosition, tile.position))
            {
                possibleTiles.Add(tile);
            }
        }

        return possibleTiles;
    }

    private bool checkTiles(Vector2 original, Vector2 destination)
    {
        float positionX = destination.x - original.x;
        float positionY = destination.y - original.y;
        float position = Mathf.Sqrt(positionX * positionX + positionY * positionY);

        return Math.Abs(position - Mathf.Sqrt(5)) < 0.05;
    }
}

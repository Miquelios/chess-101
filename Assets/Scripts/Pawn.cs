using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override List<Tile> getPosibleTiles(List<Tile> tiles)
    {
        List<Tile> possibleTiles = new List<Tile>();

        foreach (Tile tile in tiles)
        {
            if (isFirstMove && checkTilesFirstMove(this.originalPosition, tile.position))
            {
                possibleTiles.Add(tile);
            }
            else if (!isFirstMove && checkTilesNotFirstMove(this.originalPosition, tile.position))
            {
                possibleTiles.Add(tile);
            }
        }

        return possibleTiles;
    }

    private bool checkTilesNotFirstMove(Vector2 original, Vector2 destination)
    {
        return destination.x == original.x && destination.y == original.y + 1
            || destination.x == original.x + 1 && destination.y == original.y + 1
            || destination.x == original.x - 1 && destination.y == original.y + 1;
    }

    private bool checkTilesFirstMove(Vector2 original, Vector2 destination)
    {
        return destination.x == original.x && destination.y == original.y + 2
            || destination.x == original.x && destination.y == original.y + 1
            || destination.x == original.x + 1 && destination.y == original.y + 1
            || destination.x == original.x - 1 && destination.y == original.y + 1;
    }
}

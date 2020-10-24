using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
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

        return checkOccupiedTiles(possibleTiles);
    }

    private bool checkTiles(Vector2 original, Vector2 destination)
    {
        return destination.x >= original.x + 1 && destination.y == original.y
            || destination.x <= original.x - 1 && destination.y == original.y
            || destination.x == original.x && destination.y >= original.y + 1
            || destination.x == original.x && destination.y <= original.y - 1;
    }


    private List<Tile> checkOccupiedTiles(List<Tile> possibleTiles)
    {
        // validarem si de la llista de posibles tiles alguna esta ocupada i talla el camí
        List<Tile> removableTiles = new List<Tile>();
        Tile removedTileXPlus = null;
        Tile removedTileXMinus = null;
        Tile removedTileYPlus = null;
        Tile removedTileYMinus = null;

        foreach (Tile tile in possibleTiles)
        {
            if (tile.occupant != null)
            {
                removableTiles.Add(tile);
            }
        }

        foreach (Tile tile in removableTiles)
        {
            if (tile.position.x > this.originalPosition.x
                && (removedTileXPlus == null
                    || removedTileXPlus != null && removedTileXPlus.position.x > tile.position.x))
            {
                removedTileXPlus = tile;
            }
            else if (tile.position.x < this.originalPosition.x
                && (removedTileXMinus == null
                    || removedTileXMinus != null && removedTileXMinus.position.x < tile.position.x))
            {
                removedTileXMinus = tile;
            }
            else if (tile.position.y > this.originalPosition.y
                && (removedTileYPlus == null
                    || removedTileYPlus != null && removedTileYPlus.position.y > tile.position.y))
            {
                removedTileYPlus = tile;
            }
            else if (tile.position.y < this.originalPosition.y
                && (removedTileYMinus == null
                    || removedTileYMinus != null && removedTileYMinus.position.y < tile.position.y))
            {
                removedTileYMinus = tile;
            }
        }

        foreach (Tile tile in possibleTiles)
        {
            if (removedTileXPlus != null && tile.position.x > removedTileXPlus.position.x
            || removedTileXMinus != null && tile.position.x < removedTileXMinus.position.x
            || removedTileYPlus != null && tile.position.y > removedTileYPlus.position.y
            || removedTileYMinus != null && tile.position.y < removedTileYMinus.position.y)
            {
                removableTiles.Add(tile);
            }
        }

        if (removedTileXPlus != null && removedTileXPlus.occupant.color != this.color)
        {
            removableTiles.Remove(removedTileXPlus);
        }
        if (removedTileXMinus != null && removedTileXMinus.occupant.color != this.color)
        {
            removableTiles.Remove(removedTileXMinus);
        }
        if (removedTileYPlus != null && removedTileYPlus.occupant.color != this.color)
        {
            removableTiles.Remove(removedTileYPlus);
        }
        if (removedTileYMinus != null && removedTileYMinus.occupant.color != this.color)
        {
            removableTiles.Remove(removedTileYMinus);
        }

        foreach (Tile tile in removableTiles)
        {
            possibleTiles.Remove(tile);
        }

        return possibleTiles;
    }

}

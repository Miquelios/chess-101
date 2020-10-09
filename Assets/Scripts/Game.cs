using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private List<Tile> tiles = new List<Tile>();
    private List<Tile> selectableTiles = new List<Tile>();
    public List<Piece> pieces = new List<Piece>();
    private Piece selectedPiece;
    private Tile originalTile;

    public GameObject prefabTile;

    void Start()
    {
        createTiles();
        deployPieces();
    }

    void createTiles()
    {
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                Tile tile = new Tile();
                tile.position = new Vector2(i, j);
                tiles.Add(tile);
            }
        }
    }

    void deployPieces()
    {
        foreach (Tile tile in tiles)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                Piece piece = pieces[i];
                if (tile.position == piece.deployPosition)
                {
                    tile.occupant = piece;
                }
            }
        }

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedPiece = selectPiece();
            if (selectedPiece != null)
            {
                selectableTiles = selectedPiece.getPosibleTiles(tiles);
                foreach (Tile tile in selectableTiles)
                {
                    Instantiate(prefabTile, tile.position, Quaternion.identity);
                }
            }
            /*if (selectedPiece != null)
            {
                Instantiate(Tile, new Vector2(selectedPiece.transform.position.x + 1, selectedPiece.transform.position.y), Quaternion.identity);
            }*/
        }

        if (selectedPiece != null)
        {
            if (Input.GetMouseButton(0))
            {
                dragPiece(selectedPiece);
            }

            if (Input.GetMouseButtonUp(0))
            {
                movePiece(selectedPiece);

                var gam = GameObject.FindGameObjectsWithTag("selectedTile");
                foreach (GameObject game in gam)
                    GameObject.Destroy(game);


            }
        }

    }

    private Piece selectPiece()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        foreach (Tile tile in tiles)
        {
            if (compareVector2(mousePosition, tile.position))
            {
                originalTile = tile;
                return tile.occupant;
            }
        }

        originalTile = null;
        return null;
    }

    private void dragPiece(Piece selectedPiece)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectedPiece.transform.position = mousePosition;
    }

    private void movePiece(Piece selectePiece)
    {
        //List<Tile> destinationTiles = selectedPiece.getPosibleTiles(tiles);
        //foreach (Tile destinationTile in destinationTiles)
        foreach (Tile destinationTile in selectableTiles)
        {

            if (selectedPiece != null && compareVector2(selectedPiece.transform.position, destinationTile.position))
            {
                selectedPiece.transform.position = destinationTile.position;
                selectedPiece.originalPosition = destinationTile.position;
                destinationTile.occupant = selectedPiece;
                selectedPiece = null;
                originalTile.occupant = null;
                originalTile = null;


            }
        }

        if (selectedPiece != null)
        {
            selectedPiece.transform.position = originalTile.position;
        }
    }

    private bool compareVector2(Vector2 original, Vector2 destination)
    {
        return (destination.x <= original.x + 0.5) && (destination.x >= original.x - 0.5)
                    && (destination.y <= original.y + 0.5) && (destination.y >= original.y - 0.5);
    }

}
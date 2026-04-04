using System;
using System.Collections.Generic;
using System.Globalization;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Board : MonoBehaviour
{
    public static Board Instance;

    private const int numRows = 8;
    private const int numColumns = 8;

    private Coordinate[,] coordinates = new Coordinate[numColumns, numRows];
    private Piece[,] pieceGrid = new Piece[numColumns, numRows];
    [SerializeField] private Transform piecesParent;



    [Header("Board / Tile stuff")]
    [SerializeField] private BoardTile tilePrefab;
    [SerializeField] private Transform tilesParent;
    [SerializeField] private float tileSize = 1.0f;
    [SerializeField] private Vector3 boardOrigin = Vector3.zero;
    private BoardTile[,] tiles = new BoardTile[numColumns, numRows];



    private List<Piece> pieces;

    [Header("Piece Prefabs")]
    [SerializeField] private Piece whitePawnPrefab;
    [SerializeField] private Piece blackPawnPrefab;
    [SerializeField] private Piece whiteKnightPrefab;
    [SerializeField] private Piece blackKnightPrefab;
    [SerializeField] private Piece whiteRookPrefab;
    [SerializeField] private Piece blackRookPrefab;
    [SerializeField] private Piece whiteBishopPrefab;
    [SerializeField] private Piece blackBishopPrefab;
    [SerializeField] private Piece whiteQueenPrefab;
    [SerializeField] private Piece blackQueenPrefab;
    [SerializeField] private Piece whiteKingPrefab;
    [SerializeField] private Piece blackKingPrefab;



    void Awake()
    {
        if (Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
    }

    void Start()
    {
        for (int i = 0; i < numColumns; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                Column column = (Column)i;
                Row row = (Row)j;
                coordinates[i, j] = new Coordinate(column, row);
                
                Debug.Log($"Coordinate generated on board: {column}{row}");
            }
        }

        Debug.Log("Generating pieces...");
        pieces = new List<Piece>();
        GeneratePieces(pieces);
        Debug.Log("Pieces genered!");

        Debug.Log("Generating board visuals...");
        GenerateBoardVisuals();
        Debug.Log("Board visuals generated!");

        // Center Camera on board
        Vector3 boardCenter = boardOrigin + new Vector3((numColumns * tileSize) / 2, (numRows * tileSize) / 2, 0);
        Camera.main.transform.position = new Vector3(boardCenter.x, boardCenter.y, Camera.main.transform.position.z);
    }

    public void Reset()
    {
        // Destroy all pieces
        foreach (Piece piece in pieces)
        {
            this.RemovePiece(piece);
        }
        pieces.Clear();

        // Destroy all coordinates
        for (int i = 0; i < numColumns; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                Column column = (Column)i;
                Row row = (Row)j;
                coordinates[i, j] = null;
                
                Debug.Log($"Destroyed coordinate {column}{row}");
            }
        }

        // Destroy all BoardTiles

        // Do initialization
        Start();
    }

    private void GenerateBoardVisuals()
    {
        for (int col = 0; col < numColumns; col++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Coordinate coord = new Coordinate((Column)col, (Row)row);
                Vector3 worldPosition = GetWorldPosition(coord);

                BoardTile newTile = Instantiate(tilePrefab, worldPosition, Quaternion.identity, tilesParent);
                newTile.Initialize(coord);

                tiles[col, row] = newTile;

                bool isLightTile = (col + row) % 2 == 0;
                SpriteRenderer sr = newTile.GetComponent<SpriteRenderer>();
                sr.color = isLightTile ? UnityEngine.Color.white : UnityEngine.Color.gray;
            }
        }
    }

    private void GeneratePieces(List<Piece> pieces)
    {
        // Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnPiece(whitePawnPrefab, (Column)i, Row.Two, Color.White);
        }
        for (int i = 0; i < 8; i++)
        {
            SpawnPiece(blackPawnPrefab, (Column)i, Row.Seven, Color.Black);
        }

        // Rooks
        SpawnPiece(whiteRookPrefab, Column.A, Row.One, Color.White);
        SpawnPiece(whiteRookPrefab, Column.H, Row.One, Color.White);
        SpawnPiece(blackRookPrefab, Column.A, Row.Eight, Color.Black);
        SpawnPiece(blackRookPrefab, Column.H, Row.Eight, Color.Black);

        // Knights
        SpawnPiece(whiteKnightPrefab, Column.B, Row.One, Color.White);
        SpawnPiece(whiteKnightPrefab, Column.G, Row.One, Color.White);
        SpawnPiece(blackKnightPrefab, Column.B, Row.Eight, Color.Black);
        SpawnPiece(blackKnightPrefab, Column.G, Row.Eight, Color.Black);

        // Bishops
        SpawnPiece(whiteBishopPrefab, Column.C, Row.One, Color.White);
        SpawnPiece(whiteBishopPrefab, Column.F, Row.One, Color.White);
        SpawnPiece(blackBishopPrefab, Column.C, Row.Eight, Color.Black);
        SpawnPiece(blackBishopPrefab, Column.F, Row.Eight, Color.Black);

        // Queens
        SpawnPiece(whiteQueenPrefab, Column.D, Row.One, Color.White);
        SpawnPiece(blackQueenPrefab, Column.D, Row.Eight, Color.Black);
        
        // Kings
        SpawnPiece(whiteKingPrefab, Column.E, Row.One, Color.White);
        SpawnPiece(blackKingPrefab, Column.E, Row.Eight, Color.Black);
    }

    private void SpawnPiece(Piece piecePrefab, Column column, Row row, Color color)
    {
        Coordinate coord = this.GetCoordinate(column, row);

        Piece newPiece = Instantiate(piecePrefab, piecesParent);
        newPiece.Initialize(color, coord);

        this.MovePiece(newPiece, coord);

        Vector3 worldPosition = this.GetWorldPosition(coord);
        newPiece.transform.position = worldPosition;

        pieces.Add(newPiece);
    }

    private Vector3 GetWorldPosition(Coordinate coord)
    {
        int x = (int)coord.GetColumn();
        int y = (int)coord.GetRow();

        return new Vector3(
            boardOrigin.x + (x + 0.5f) * tileSize,
            boardOrigin.y + (y + 0.5f) * tileSize,
            0f
        );
    }

    public Coordinate GetCoordinate(Column column, Row row)
    {
        int columnIndex = (int)column;
        int rowIndex = (int)row;
        return coordinates[columnIndex, rowIndex];
    }

    public Coordinate GetCoordinate(Coordinate coordinate)
    {
        if (coordinate == null) return null;

        int columnIndex = (int)coordinate.GetColumn();
        int rowIndex = (int)coordinate.GetRow();
        return coordinates[columnIndex, rowIndex];
    }

    public Piece GetPieceAt(Coordinate coordinate)
    {
        if (coordinate == null) return null;

        int col = (int)coordinate.GetColumn();
        int row = (int)coordinate.GetRow();

        if (pieceGrid[col, row] != null) Debug.Log($"Return piece: {pieceGrid[col, row]}");
        if (pieceGrid[col, row] == null) Debug.Log($"Return piece is null!");

        return pieceGrid[col, row];
    }

    public bool IsOccupied(Coordinate coordinate)
    {
        return GetPieceAt(coordinate) != null;
    }

    public bool IsOccupiedByFriendly(Coordinate coordinate, Color pieceColor)
    {
        Piece piece = GetPieceAt(coordinate);

        return (piece != null && piece.GetColor() == pieceColor);
    }

    public bool IsOccupiedByEnemy(Coordinate coordinate, Color pieceColor)
    {
        Piece piece = GetPieceAt(coordinate);
        return (piece!= null && piece.GetColor() != pieceColor);
    }

    // IsReachableByEnemy(): Used for king's movement, to see if enemy is
    // able to "take"/reach a coordinate.
    public bool IsReachableByEnemy(Coordinate coord, Color pieceColor)
    {
        bool isReachableByEnemy = false;

        foreach (Piece piece in pieces)
        {
            if (piece.IsInCaptureRange(coord) && piece.GetColor() != pieceColor)
                isReachableByEnemy = true;
        }

        return isReachableByEnemy;
    }

    public void MovePiece(Piece piece, Coordinate target)
    {
        Coordinate start = piece.GetCoordinate();

        // Remove from old position
        pieceGrid[(int)start.GetColumn(), (int)start.GetRow()] = null;

        // Capture if needed
        Piece captured = GetPieceAt(target);
        if (captured != null)
        {
            // Handle capture (destroy, disable, etc.)
            RemovePiece(captured);
        }

        // Place in new position
        pieceGrid[(int)target.GetColumn(), (int)target.GetRow()] = piece;

        // Update piece's internal coordinate
        piece.SetCoordinate(target);
        // piece.SetMoved();
    }

    public void MovePieceToTile(Piece piece, BoardTile tile)
    {
        Coordinate start = piece.GetCoordinate();
        Coordinate target = tile.GetCoordinate();

        // Remove from old position
        pieceGrid[(int)start.GetColumn(), (int)start.GetRow()] = null;

        // Capture if needed
        Piece captured = GetPieceAt(target);
        if (captured != null)
        {
            RemovePiece(captured);
        }

        // Place in new position
        pieceGrid[(int)target.GetColumn(), (int)target.GetRow()] = piece;

        // Update piece's internal coordinate
        // piece.SetCoordinate(target);
        piece.MoveToTile(tile);
        piece.SetMoved();
    }

    public void RemovePiece(Piece piece)
    {
        // For now: Just destroy the piece
        int pieceRow = (int)piece.GetCoordinate().GetRow();
        int pieceCol = (int)piece.GetCoordinate().GetColumn();

        // Remove from grid
        pieceGrid[pieceCol, pieceRow] = null;

        // TEMP: Destroy piece object
        Destroy(piece.gameObject);
    }

    // CanKingMove(): Checks if a king is able to move, and not be in check.
    public bool CanKingMove(Coordinate coordinate, Piece kingPiece)
    {
        Color kingColor = kingPiece.GetColor();

        bool isValidMove = true;

        foreach (Piece piece in pieces)
        {
            if (piece.GetColor() != kingColor)
            {
                if (piece.CanMove(coordinate)) isValidMove = false;
            }
        }

        if (isValidMove == false)
            Debug.Log("King cannot move!");

        return isValidMove;
    }

    // KingIsInCheck(): Checks if a given king is currently in check.
    public bool KingIsInCheck(Piece kingPiece)
    {
        Color kingColor = kingPiece.GetColor();

        Coordinate currentCoordinate = kingPiece.GetCoordinate();
        bool isInCheck = false;

        foreach(Piece piece in pieces)
        {
            if (piece.GetColor() != kingColor)
                if (piece.CanMove(currentCoordinate)) isInCheck = true;
        }

        Debug.Log("King is in check!");

        return isInCheck;
    }

    // KingIsInCheckMate(): Determines if a given king is currently in checkmate.
    //
    // kingPiece: The king to check
    // kingColor: The color of the king to check.
    public bool KingIsInCheckmate(Piece kingPiece)
    {
        // Idea:
        // Assume true
        // if king is not in check -> false
        // if king has a move that would make them not be in check -> false

        bool isInCheckMate = true;

        if (!KingIsInCheck(kingPiece)) isInCheckMate = false;
        
        // Possible moves
        Coordinate currentCoordinate = kingPiece.GetCoordinate();

        Coordinate N = currentCoordinate.GetNeighborCoordinate(Direction.North);
        Coordinate NE = currentCoordinate.GetNeighborCoordinate(Direction.NorthEast);
        Coordinate E = currentCoordinate.GetNeighborCoordinate(Direction.East);
        Coordinate SE = currentCoordinate.GetNeighborCoordinate(Direction.SouthEast);
        Coordinate S = currentCoordinate.GetNeighborCoordinate(Direction.South);
        Coordinate SW = currentCoordinate.GetNeighborCoordinate(Direction.SouthWest);
        Coordinate W = currentCoordinate.GetNeighborCoordinate(Direction.West);
        Coordinate NW = currentCoordinate.GetNeighborCoordinate(Direction.NorthWest);

        List<Coordinate> neighborCoordinates = new List<Coordinate>();
        neighborCoordinates.Add(N);
        neighborCoordinates.Add(NE);
        neighborCoordinates.Add(E);
        neighborCoordinates.Add(SE);
        neighborCoordinates.Add(S);
        neighborCoordinates.Add(SW);
        neighborCoordinates.Add(W);
        neighborCoordinates.Add(NW);

        foreach (Coordinate coordinate in neighborCoordinates)
        {
            if (coordinate != null)
                if (CanKingMove(coordinate, kingPiece)) isInCheckMate = false;
        }

        Debug.Log("King is in checkmate!");

        return isInCheckMate;
    }
    




}
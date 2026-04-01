using System.Collections.Generic;
using System.Globalization;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Board : MonoBehaviour
{
    public static Board Instance;
    private Coordinate[,] coordinates = new Coordinate[8,8];
    private Piece[,] pieceGrid = new Piece[8,8];
    private int numRows = 8;
    private int numColumns = 8;
    private List<Piece> pieces;

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

        // TODO: Generate pieces
        pieces = new List<Piece>();
    }

    private void GeneratePieces(List<Piece> pieces)
    {
        Coordinate coord = new Coordinate(Column.A, Row.One);

        // Pawns
        for (int i = 0; i < 8; i++)
        {
            Pawn pawn = new Pawn();
            pawn.SetColor(Color.White);
            coord = new Coordinate((Column)i, (Row)1);
            MovePiece(pawn, coord);
        }
        for (int i = 0; i < 8; i++)
        {
            Pawn pawn = new Pawn();
            pawn.SetColor(Color.Black);
            coord = new Coordinate((Column)i, (Row)6);
            MovePiece(pawn, coord);
        }

        // Rooks
        Rook rook = new Rook();
        rook.SetColor(Color.White);
        coord = new Coordinate(Column.A, Row.One);
        MovePiece(rook, coord);

        rook = new Rook();
        rook.SetColor(Color.White);
        coord = new Coordinate(Column.H, Row.One);
        MovePiece(rook, coord);


        // Knights


        // Bishops


        // Queens

        
        // Kings



    }

    public Coordinate GetCoordinate(Column column, Row row)
    {
        int columnIndex = (int)column;
        int rowIndex = (int)row;
        return coordinates[columnIndex, rowIndex];
    }

    public Coordinate GetCoordinate(Coordinate coordinate)
    {
        int columnIndex = (int)coordinate.GetColumn();
        int rowIndex = (int)coordinate.GetRow();
        return coordinates[columnIndex, rowIndex];
    }

    public Piece GetPieceAt(Coordinate coordinate)
    {
        if (coordinate == null) return null;

        int col = (int)coordinate.GetColumn();
        int row = (int)coordinate.GetRow();

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

    public bool CanKingMoveToThisCoordinate(Color kingColor, Coordinate coord)
    {
        bool canMove = true;

        foreach (Piece piece in pieces)
        {
            if (piece.GetColor() != kingColor && piece.CanReach(coord))
                canMove = false;
        }

        return canMove;
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
        }

        // Place in new position
        pieceGrid[(int)target.GetColumn(), (int)target.GetRow()] = piece;

        // Update piece's internal coordinate
        piece.SetCoordinate(target);
        piece.SetMoved();
    }




}
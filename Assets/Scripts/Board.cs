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
    private const int numRows = 8;
    private const int numColumns = 8;
    [SerializeField] private float tileSize = 1.0f;
    [SerializeField] private Vector3 boardOrigin = Vector3.zero;




    private List<Piece> pieces;
    [SerializeField] private GameObject whitePawnPrefab;
    [SerializeField] private GameObject blackPawnPrefab;
    [SerializeField] private GameObject whiteKnightPrefab;
    [SerializeField] private GameObject blackKnightPrefab;
    [SerializeField] private GameObject whiteRookPrefab;
    [SerializeField] private GameObject blackRookPrefab;
    [SerializeField] private GameObject whiteBishopPrefab;
    [SerializeField] private GameObject blackBishopPrefab;
    [SerializeField] private GameObject whiteQueenPrefab;
    [SerializeField] private GameObject blackQueenPrefab;
    [SerializeField] private GameObject whiteKingPrefab;
    [SerializeField] private GameObject blackKingPrefab;


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
        


        // Knights


        // Bishops


        // Queens

        
        // Kings



    }

    private Piece SpawnPiece(GameObject piecePrefab, Column column, Row row, Color color)
    {
        Coordinate coord = this.GetCoordinate(column, row);

        // TODO: Set coordinates and transform in world.
        Piece newPiece = Instantiate(piecePrefab, pieceParent);
        newPiece.Initialize(color, coord);

        this.MovePiece(newPiece, coord);

        Vector3 worldPosition = this.GetWorldPosition(coord);
        newPiece.transform.position = worldPosition;

        return newPiece;
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
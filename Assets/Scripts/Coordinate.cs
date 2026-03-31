using Unity.VisualScripting;
using UnityEngine;

public enum Column
{
    A, B, C, D, E, F, G, H
}

public enum Row
{
    One, Two, Three, Four, Five, Six, Seven, Eight
}

public enum Direction
{
    North,
    NorthEast,
    NorthWest,
    South,
    SouthEast,
    SouthWest,
    East,
    West,
}


public class Coordinate : MonoBehaviour
{
    private Row row;
    private Column column;
    private Piece piece;
    private const int NUM_ROWS = 8;
    private const int NUM_COLS = 8;
    



    public Coordinate(){
        this.column = Column.A;
        this.row = Row.One;
    }

    public Coordinate(Column column, Row row){
        this.column = column;
        this.row = row;
    }

    public Coordinate(Coordinate coord)
    {
        this.column = coord.getColumn();
        this.row = coord.getRow();
    }

    public bool IsOccupied() {return (this.piece != null);}
    public Color GetOccupiedColor() {return this.piece.GetColor();}

    public Piece getPieceAt()
    {
        return this.piece;
    }

    public Row getRow() {return this.row;}
    public Column getColumn() {return this.column;}
    public int RowToInt(Row row) {return (int)row - 1;}
    public Row IntToRow(int integer) {return (Row)(integer + 1);}
    public int ColumnToInt(Column column) {return (int)column - 1;}
    public Column IntToColumn(int integer) {return (Column)(integer + 1);}

    public Coordinate GetNeighborCoordinate(Direction dir)
    {
        int row = this.RowToInt(this.row);
        int column = this.ColumnToInt(this.column);

        Coordinate returnCoordinate = null;
        switch (dir)
        {
            case Direction.North:
                row++;
                break;
            case Direction.NorthEast:
                row++;
                column++;
                break;
            case Direction.NorthWest:
                row++;
                column--;
                break;
            case Direction.South:
                row--;
                break;
            case Direction.SouthEast:
                row--;
                column++;
                break;
            case Direction.SouthWest:
                row--;
                column--;
                break;
            case Direction.East:
                column++;
                break;
            case Direction.West:
                column--;
                break;
        }

        if (row > NUM_ROWS || row < 1 ||
            column > NUM_COLS || column < 1)
        {
            returnCoordinate = null;
        }
        else
        {
            Column returnColumn = IntToColumn(column);
            Row returnRow = IntToRow(row);
            returnCoordinate = new Coordinate(returnColumn, returnRow);
        }

        return returnCoordinate;
    }






}

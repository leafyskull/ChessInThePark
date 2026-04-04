using System.Collections.Generic;
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


public class Coordinate
{
    private Row row;
    private Column column;
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
        this.column = coord.GetColumn();
        this.row = coord.GetRow();
    }

    public Row GetRow() {return this.row;}
    public Column GetColumn() {return this.column;}

    public Coordinate GetNeighborCoordinate(Direction dir)
    {
        Board board = Board.Instance;
    

        int row = (int)(this.row);
        int column = (int)(this.column);

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

        if (row > NUM_ROWS - 1 || row < 0 ||
            column > NUM_COLS - 1 || column < 0)
        {
            returnCoordinate = null;
        }
        else
        {
            Column returnColumn = (Column)column;
            Row returnRow = (Row)row;
            returnCoordinate = board.GetCoordinate(returnColumn, returnRow);
        }

        if (returnCoordinate == null) Debug.Log("Return coord is null!");
        else Debug.Log($"Return coordinate: {returnCoordinate.GetColumn()}{returnCoordinate.GetRow()}");

        return returnCoordinate;
    }

    public bool isEqual(Coordinate coordinate)
    {
        if (coordinate == null) return false;

        int thisCol = (int)this.column;
        int thisRow = (int)this.row;

        int otherCol = (int)coordinate.GetColumn();
        int otherRow = (int)coordinate.GetRow();

        if (thisCol == otherCol && thisRow == otherRow) return true;
        else return false;
    }







}

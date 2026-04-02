using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;



public class Knight : Piece
{
    


    public override bool IsInCaptureRange(Coordinate coord)
    {
        if (coord == null) return false;

        Board board = Board.Instance;

        List<Coordinate> potentialCoordinates = new List<Coordinate>();
        List<Coordinate> validCoordinates = new List<Coordinate>();
        bool canMove = false;

        Coordinate currentCoordinate = this.GetCoordinate();
        int currentRow = (int)currentCoordinate.GetRow();
        int currentColumn = (int)currentCoordinate.GetColumn();

        int row;
        int column;

        row = currentRow + 1;
        column = currentColumn - 2;
        if (row > 0 && row < 8 && column > 0 && column < 8)
        {
            Coordinate OneNorthTwoWest = board.GetCoordinate(new Coordinate((Column)column, (Row)row));
            potentialCoordinates.Add(OneNorthTwoWest);
        }

        row = currentRow + 2;
        column = currentColumn - 1;
        if (row > 0 && row < 8 && column > 0 && column < 8)
        {
            Coordinate TwoNorthOneWest = board.GetCoordinate(new Coordinate((Column)column, (Row)row));
            potentialCoordinates.Add(TwoNorthOneWest);
        }

        row = currentRow + 2;
        column = currentColumn + 1;
        if (row > 0 && row < 8 && column > 0 && column < 8)
        {
            Coordinate TwoNorthOneEast = board.GetCoordinate(new Coordinate((Column)column, (Row)row));
            potentialCoordinates.Add(TwoNorthOneEast);
        }

        row = currentRow + 1;
        column = currentColumn + 2;
        if (row > 0 && row < 8 && column > 0 && column < 8)
        {
            Coordinate OneNorthTwoEast = board.GetCoordinate(new Coordinate((Column)column, (Row)row));
            potentialCoordinates.Add(OneNorthTwoEast);
        }

        row = currentRow - 1;
        column = currentColumn + 2;
        if (row > 0 && row < 8 && column > 0 && column < 8)
        {
            Coordinate OneSouthTwoEast = board.GetCoordinate(new Coordinate((Column)column, (Row)row));
            potentialCoordinates.Add(OneSouthTwoEast);   
        }

        row = currentRow - 2;
        column = currentColumn + 1;
        if (row > 0 && row < 8 && column > 0 && column < 8)
        {
            Coordinate TwoSouthOneEast = board.GetCoordinate(new Coordinate((Column)column, (Row)row));
            potentialCoordinates.Add(TwoSouthOneEast);
        }

        row = currentRow - 2;
        column = currentColumn - 1;
        if (row > 0 && row < 8 && column > 0 && column < 8)
        {
            Coordinate TwoSouthOneWest = board.GetCoordinate(new Coordinate((Column)column, (Row)row));
            potentialCoordinates.Add(TwoSouthOneWest);
        }

        row = currentRow - 1;
        column = currentColumn - 2;
        if (row > 0 && row < 8 && column > 0 && column < 8)
        {
            Coordinate OneSouthTwoWest = board.GetCoordinate(new Coordinate((Column)column, (Row)row));
            potentialCoordinates.Add(OneSouthTwoWest);
        }

        foreach (Coordinate coordinate in potentialCoordinates)
            if (coordinate.isEqual(coord)) canMove = true;

        return canMove;
    }

    public override bool CanMove(Coordinate coord)
    {
        Board board = Board.Instance;

        bool canMove = true;

        if (!CanReach(coord)) canMove = false;
        if (board.IsOccupiedByFriendly(coord, this.GetColor())) canMove = false;

        return canMove;
    }

    public override bool CanReach(Coordinate coord)
    {
        return IsInCaptureRange(coord);
    }

}
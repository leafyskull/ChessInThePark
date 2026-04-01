using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;



public class Knight : Piece
{
    


    public override bool CanReach(Coordinate coord)
    {
        if (coord == null) return false;

        Board board = Board.Instance;

        List<Coordinate> potentialCoordinates = new List<Coordinate>();
        List<Coordinate> validCoordinates = new List<Coordinate>();
        bool canMove = false;

        Coordinate currentCoordinate = this.GetCoordinate();
        Row currentRow = currentCoordinate.GetRow();
        Column currentColumn = currentCoordinate.GetColumn();

        Row row;
        Column column;

        row = (Row)(currentRow + 1);
        column = (Column)(currentColumn - 2);
        Coordinate OneNorthTwoWest = board.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(OneNorthTwoWest);

        row = (Row)(currentRow + 2);
        column = (Column)(currentColumn - 1);
        Coordinate TwoNorthOneWest = board.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(TwoNorthOneWest);

        row = (Row)(currentRow + 2);
        column = (Column)(currentColumn + 1);
        Coordinate TwoNorthOneEast = board.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(TwoNorthOneEast);

        row = (Row)(currentRow + 1);
        column = (Column)(currentColumn + 2);
        Coordinate OneNorthTwoEast = board.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(OneNorthTwoEast);

        row = (Row)(currentRow - 1);
        column = (Column)(currentColumn + 2);
        Coordinate OneSouthTwoEast = board.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(OneSouthTwoEast);

        row = (Row)(currentRow - 2);
        column = (Column)(currentColumn + 1);
        Coordinate TwoSouthOneEast = board.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(TwoSouthOneEast);

        row = (Row)(currentRow - 2);
        column = (Column)(currentColumn - 1);
        Coordinate TwoSouthOneWest = board.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(TwoSouthOneWest);

        row = (Row)(currentRow - 1);
        column = (Column)(currentColumn - 2);
        Coordinate OneSouthTwoWest = board.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(OneSouthTwoWest);

        if (validCoordinates.Contains(coord)) canMove = true;

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

}
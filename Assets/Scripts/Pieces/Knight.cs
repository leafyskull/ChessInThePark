using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;



public class Knight : Piece
{
    


    public override bool CanMove(Coordinate coord)
    {
        if (coord == null) return false;

        Board board = Board.Instance;

        List<Coordinate> potentialCoordinates = new List<Coordinate>();
        List<Coordinate> validCoordinates = new List<Coordinate>();
        bool canMove = false;

        Coordinate currentCoordinate = this.getCoordinate();
        Row currentRow = currentCoordinate.getRow();
        Column currentColumn = currentCoordinate.getColumn();

        Row row;
        Column column;

        row = (Row)(currentRow + 1);
        column = (Column)(currentColumn - 2);
        Coordinate OneNorthTwoWest = Board.Instance.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(OneNorthTwoWest);

        row = (Row)(currentRow + 2);
        column = (Column)(currentColumn - 1);
        Coordinate TwoNorthOneWest = Board.Instance.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(TwoNorthOneWest);

        row = (Row)(currentRow + 2);
        column = (Column)(currentColumn + 1);
        Coordinate TwoNorthOneEast = Board.Instance.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(TwoNorthOneEast);

        row = (Row)(currentRow + 1);
        column = (Column)(currentColumn + 2);
        Coordinate OneNorthTwoEast = Board.Instance.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(OneNorthTwoEast);

        row = (Row)(currentRow - 1);
        column = (Column)(currentColumn + 2);
        Coordinate OneSouthTwoEast = Board.Instance.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(OneSouthTwoEast);

        row = (Row)(currentRow - 2);
        column = (Column)(currentColumn + 1);
        Coordinate TwoSouthOneEast = Board.Instance.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(TwoSouthOneEast);

        row = (Row)(currentRow - 2);
        column = (Column)(currentColumn - 1);
        Coordinate TwoSouthOneWest = Board.Instance.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(TwoSouthOneWest);

        row = (Row)(currentRow - 1);
        column = (Column)(currentColumn - 2);
        Coordinate OneSouthTwoWest = Board.Instance.GetCoordinate(new Coordinate(column, row));
        potentialCoordinates.Add(OneSouthTwoWest);

        foreach (Coordinate coordinate in potentialCoordinates)
        {
            Coordinate boardCoordinate = board.GetCoordinate(coordinate);

            // If not occupied, valid move.
            if (!boardCoordinate.IsOccupied())
            {
                validCoordinates.Add(coordinate);
            }
            // If occupied by enemy piece, valid mode (capture)
            if (boardCoordinate.IsOccupied() && boardCoordinate.GetOccupiedColor() != this.GetColor())
            {
                validCoordinates.Add(coordinate);
            }
        }

        if (validCoordinates.Contains(coord)) canMove = true;

        return canMove;
    }

    public override bool CanReach(Coordinate coord)
    {
        // TODO: Implement
        return false;
    }

}
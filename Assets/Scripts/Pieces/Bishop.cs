using UnityEngine;
using System.Collections.Generic;



public class Bishop : Piece
{
    


    public override bool CanMove(Coordinate coord)
    {
        Coordinate currentCoordinate = this.getCoordinate();
        List<Coordinate> validCoordinates = new List<Coordinate>();
        bool canMove = false;

        // For all directions:
            // If empty, add to valid moves and move on
            // If occupied with enemy, add to valid moves and stop
            // If occupied with friendly, stop

        // NorthEast
        Row northEastRow = currentCoordinate.GetNeighborCoordinate(Direction.North).getRow();
        Column northEastColumn = currentCoordinate.GetNeighborCoordinate(Direction.East).getColumn();
        Coordinate northEastNeighbor = new Coordinate(northEastColumn, northEastRow);
        northEastNeighbor = Board.Instance.GetCoordinate(northEastNeighbor);
        while (northEastNeighbor != null)
        {
            // Occupied by friendly - stop
            if (northEastNeighbor.IsOccupied() && northEastNeighbor.GetOccupiedColor() == this.GetColor()){
                break;
            }
            // Occupied by enemy - add to valid moves and stop
            if (northEastNeighbor.IsOccupied() && northEastNeighbor.GetOccupiedColor() != this.GetColor())
            {
                validCoordinates.Add(northEastNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            if (!northEastNeighbor.IsOccupied())
            {
                validCoordinates.Add(northEastNeighbor);
                northEastRow = northEastNeighbor.GetNeighborCoordinate(Direction.North).getRow();
                northEastColumn = northEastNeighbor.GetNeighborCoordinate(Direction.East).getColumn();
                northEastNeighbor = new Coordinate(northEastColumn, northEastRow);
                northEastNeighbor = Board.Instance.GetCoordinate(northEastNeighbor);
            }
        }

        // NorthWest
        Row northWestRow = currentCoordinate.GetNeighborCoordinate(Direction.North).getRow();
        Column northWestColumn = currentCoordinate.GetNeighborCoordinate(Direction.West).getColumn();
        Coordinate northWestNeighbor = new Coordinate(northWestColumn, northWestRow);
        northWestNeighbor = Board.Instance.GetCoordinate(northWestNeighbor);
        while (northWestNeighbor != null)
        {
            // Occupied by friendly - stop
            if (northWestNeighbor.IsOccupied() && northWestNeighbor.GetOccupiedColor() == this.GetColor()){
                break;
            }
            // Occupied by enemy - add to valid moves and stop
            if (northWestNeighbor.IsOccupied() && northWestNeighbor.GetOccupiedColor() != this.GetColor())
            {
                validCoordinates.Add(northWestNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            if (!northWestNeighbor.IsOccupied())
            {
                validCoordinates.Add(northWestNeighbor);
                northEastRow = northWestNeighbor.GetNeighborCoordinate(Direction.North).getRow();
                northEastColumn = northWestNeighbor.GetNeighborCoordinate(Direction.West).getColumn();
                northWestNeighbor = new Coordinate(northEastColumn, northEastRow);
                northWestNeighbor = Board.Instance.GetCoordinate(northWestNeighbor);
            }
        }

        // SouthEast
        Row southEastRow = currentCoordinate.GetNeighborCoordinate(Direction.South).getRow();
        Column southEastColumn = currentCoordinate.GetNeighborCoordinate(Direction.East).getColumn();
        Coordinate southEastNeighbor = new Coordinate(southEastColumn, southEastRow);
        southEastNeighbor = Board.Instance.GetCoordinate(southEastNeighbor);
        while (southEastNeighbor != null)
        {
            // Occupied by friendly - stop
            if (southEastNeighbor.IsOccupied() && southEastNeighbor.GetOccupiedColor() == this.GetColor()){
                break;
            }
            // Occupied by enemy - add to valid moves and stop
            if (southEastNeighbor.IsOccupied() && southEastNeighbor.GetOccupiedColor() != this.GetColor())
            {
                validCoordinates.Add(southEastNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            if (!southEastNeighbor.IsOccupied())
            {
                validCoordinates.Add(southEastNeighbor);
                southEastRow = southEastNeighbor.GetNeighborCoordinate(Direction.South).getRow();
                southEastColumn = southEastNeighbor.GetNeighborCoordinate(Direction.East).getColumn();
                southEastNeighbor = new Coordinate(southEastColumn, southEastRow);
                southEastNeighbor = Board.Instance.GetCoordinate(southEastNeighbor);
            }
        }

        // SouthWest
        Row southWestRow = currentCoordinate.GetNeighborCoordinate(Direction.South).getRow();
        Column southWestColumn = currentCoordinate.GetNeighborCoordinate(Direction.West).getColumn();
        Coordinate southWestNeighbor = new Coordinate(southWestColumn, southWestRow);
        southWestNeighbor = Board.Instance.GetCoordinate(southWestNeighbor);
        while (southWestNeighbor != null)
        {
            // Occupied by friendly - stop
            if (southWestNeighbor.IsOccupied() && southWestNeighbor.GetOccupiedColor() == this.GetColor()){
                break;
            }
            // Occupied by enemy - add to valid moves and stop
            if (southWestNeighbor.IsOccupied() && southWestNeighbor.GetOccupiedColor() != this.GetColor())
            {
                validCoordinates.Add(southWestNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            if (!southWestNeighbor.IsOccupied())
            {
                validCoordinates.Add(southWestNeighbor);
                southWestRow = southWestNeighbor.GetNeighborCoordinate(Direction.South).getRow();
                southWestColumn = southWestNeighbor.GetNeighborCoordinate(Direction.East).getColumn();
                southWestNeighbor = new Coordinate(southWestColumn, southWestRow);
                southWestNeighbor = Board.Instance.GetCoordinate(southWestNeighbor);
            }
        }

        if (validCoordinates.Contains(coord)) canMove = true;

        return canMove;
    }

}
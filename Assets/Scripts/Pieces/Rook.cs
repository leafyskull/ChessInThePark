using UnityEngine;
using System.Collections.Generic;



public class Rook : Piece
{



    public override bool CanMove(Coordinate coord)
    {
        if (coord == null) return false;

        Coordinate currentCoordinate = this.getCoordinate();
        List<Coordinate> validCoordinates = new List<Coordinate>();
        bool canMove = false;

        // For all directions:
            // If empty, add to valid moves and move on
            // If occupied with enemy, add to valid moves and stop
            // If occupied with friendly, stop

        // North
        Row northRow = currentCoordinate.GetNeighborCoordinate(Direction.North).getRow();
        Coordinate northNeighbor = new Coordinate(currentCoordinate.getColumn(), northRow);
        northNeighbor = Board.Instance.GetCoordinate(northNeighbor);
        while (northNeighbor != null)
        {
            // Occupied by friendly - stop
            if (northNeighbor.IsOccupied() && northNeighbor.GetOccupiedColor() == this.GetColor()){
                break;
            }
            // Occupied by enemy - add to valid moves and stop
            if (northNeighbor.IsOccupied() && northNeighbor.GetOccupiedColor() != this.GetColor())
            {
                validCoordinates.Add(northNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            if (!northNeighbor.IsOccupied())
            {
                validCoordinates.Add(northNeighbor);
                northRow = northNeighbor.GetNeighborCoordinate(Direction.North).getRow();
                northNeighbor = new Coordinate(northNeighbor.getColumn(), northRow);
                northNeighbor = Board.Instance.GetCoordinate(northNeighbor);
            }
        }

        // South
        Row southRow = currentCoordinate.GetNeighborCoordinate(Direction.South).getRow();
        Coordinate southNeighbor = new Coordinate(currentCoordinate.getColumn(), southRow);
        southNeighbor = Board.Instance.GetCoordinate(southNeighbor);
        while(southNeighbor != null)
        {
            // Occupied by friendly - stop
            if (southNeighbor.IsOccupied() && southNeighbor.GetOccupiedColor() == this.GetColor()){
                break;
            }
            // Occupied by enemy - add to valid moves and stop
            if (southNeighbor.IsOccupied() && southNeighbor.GetOccupiedColor() != this.GetColor())
            {
                validCoordinates.Add(southNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            if (!southNeighbor.IsOccupied())
            {
                validCoordinates.Add(southNeighbor);
                southRow = northNeighbor.GetNeighborCoordinate(Direction.South).getRow();
                southNeighbor = new Coordinate(southNeighbor.getColumn(), southRow);
                southNeighbor = Board.Instance.GetCoordinate(southNeighbor);
            }
        }

        // East
        Column eastColumn = currentCoordinate.GetNeighborCoordinate(Direction.East).getColumn();
        Coordinate eastNeighbor = new Coordinate(eastColumn, currentCoordinate.getRow());
        eastNeighbor = Board.Instance.GetCoordinate(eastNeighbor);
        while(eastNeighbor != null)
        {
            // Occupied by friendly - stop
            if (eastNeighbor.IsOccupied() && eastNeighbor.GetOccupiedColor() == this.GetColor()){
                break;
            }
            // Occupied by enemy - add to valid moves and stop
            if (eastNeighbor.IsOccupied() && eastNeighbor.GetOccupiedColor() != this.GetColor())
            {
                validCoordinates.Add(eastNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            if (!eastNeighbor.IsOccupied())
            {
                validCoordinates.Add(eastNeighbor);
                eastColumn = eastNeighbor.GetNeighborCoordinate(Direction.East).getColumn();
                eastNeighbor = new Coordinate(eastColumn, eastNeighbor.getRow());
                eastNeighbor = Board.Instance.GetCoordinate(eastNeighbor);
            }
        }

        // West
        Column westColumn = currentCoordinate.GetNeighborCoordinate(Direction.West).getColumn();
        Coordinate westNeighbor = new Coordinate(westColumn, currentCoordinate.getRow());
        westNeighbor = Board.Instance.GetCoordinate(westNeighbor);
        while(westNeighbor != null)
        {
            // Occupied by friendly - stop
            if (westNeighbor.IsOccupied() && westNeighbor.GetOccupiedColor() == this.GetColor()){
                break;
            }
            // Occupied by enemy - add to valid moves and stop
            if (westNeighbor.IsOccupied() && westNeighbor.GetOccupiedColor() != this.GetColor())
            {
                validCoordinates.Add(westNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            if (!westNeighbor.IsOccupied())
            {
                validCoordinates.Add(westNeighbor);
                westColumn = westNeighbor.GetNeighborCoordinate(Direction.East).getColumn();
                westNeighbor = new Coordinate(westColumn, westNeighbor.getRow());
                westNeighbor = Board.Instance.GetCoordinate(westNeighbor);
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
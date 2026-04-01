using UnityEngine;
using System.Collections.Generic;



public class Rook : Piece
{



    public override bool CanReach(Coordinate coord)
    {
        if (coord == null) return false;

        Board board = Board.Instance;

        Coordinate currentCoordinate = this.GetCoordinate();
        List<Coordinate> validCoordinates = new List<Coordinate>();
        bool canMove = false;

        // For all directions:
            // If empty, add to valid moves and move on
            // If occupied with enemy, add to valid moves and stop
            // If occupied with friendly, stop

        // North
        Row northRow = currentCoordinate.GetNeighborCoordinate(Direction.North).GetRow();
        Coordinate northNeighbor = new Coordinate(currentCoordinate.GetColumn(), northRow);
        northNeighbor = Board.Instance.GetCoordinate(northNeighbor);
        while (northNeighbor != null)
        {
            // Occupied - add to valid reach and stop.
            if (board.IsOccupied(northNeighbor))
            {
                validCoordinates.Add(northNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            else
            {
                validCoordinates.Add(northNeighbor);
                northRow = northNeighbor.GetNeighborCoordinate(Direction.North).GetRow();
                northNeighbor = new Coordinate(northNeighbor.GetColumn(), northRow);
                northNeighbor = Board.Instance.GetCoordinate(northNeighbor);
            }
        }

        // South
        Row southRow = currentCoordinate.GetNeighborCoordinate(Direction.South).GetRow();
        Coordinate southNeighbor = new Coordinate(currentCoordinate.GetColumn(), southRow);
        southNeighbor = Board.Instance.GetCoordinate(southNeighbor);
        while(southNeighbor != null)
        {
            // Occupied - add to valid reach and stop.
            if (board.IsOccupied(southNeighbor))
            {
                validCoordinates.Add(southNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            else
            {
                validCoordinates.Add(southNeighbor);
                southRow = northNeighbor.GetNeighborCoordinate(Direction.South).GetRow();
                southNeighbor = new Coordinate(southNeighbor.GetColumn(), southRow);
                southNeighbor = Board.Instance.GetCoordinate(southNeighbor);
            }
        }

        // East
        Column eastColumn = currentCoordinate.GetNeighborCoordinate(Direction.East).GetColumn();
        Coordinate eastNeighbor = new Coordinate(eastColumn, currentCoordinate.GetRow());
        eastNeighbor = Board.Instance.GetCoordinate(eastNeighbor);
        while(eastNeighbor != null)
        {
            // Occupied - add to valid reach and stop.
            if (board.IsOccupied(eastNeighbor))
            {
                validCoordinates.Add(eastNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            else
            {
                validCoordinates.Add(eastNeighbor);
                eastColumn = eastNeighbor.GetNeighborCoordinate(Direction.East).GetColumn();
                eastNeighbor = new Coordinate(eastColumn, eastNeighbor.GetRow());
                eastNeighbor = Board.Instance.GetCoordinate(eastNeighbor);
            }
        }

        // West
        Column westColumn = currentCoordinate.GetNeighborCoordinate(Direction.West).GetColumn();
        Coordinate westNeighbor = new Coordinate(westColumn, currentCoordinate.GetRow());
        westNeighbor = Board.Instance.GetCoordinate(westNeighbor);
        while(westNeighbor != null)
        {
            // Occupied - add to valid reach and stop.
            if (board.IsOccupied(westNeighbor))
            {
                validCoordinates.Add(westNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            else
            {
                validCoordinates.Add(westNeighbor);
                westColumn = westNeighbor.GetNeighborCoordinate(Direction.East).GetColumn();
                westNeighbor = new Coordinate(westColumn, westNeighbor.GetRow());
                westNeighbor = Board.Instance.GetCoordinate(westNeighbor);
            }
        }

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
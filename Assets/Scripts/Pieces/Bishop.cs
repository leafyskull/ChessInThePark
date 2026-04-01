using UnityEngine;
using System.Collections.Generic;



public class Bishop : Piece
{
    


    public override bool CanReach(Coordinate coord)
    {
        Board board = Board.Instance;

        Coordinate currentCoordinate = this.GetCoordinate();
        List<Coordinate> validCoordinates = new List<Coordinate>();
        bool canMove = false;

        // For all directions:
            // If empty, add to valid moves and move on
            // If occupied with enemy, add to valid moves and stop
            // If occupied with friendly, stop

        // NorthEast
        Row northEastRow = currentCoordinate.GetNeighborCoordinate(Direction.North).GetRow();
        Column northEastColumn = currentCoordinate.GetNeighborCoordinate(Direction.East).GetColumn();
        Coordinate northEastNeighbor = new Coordinate(northEastColumn, northEastRow);
        northEastNeighbor = board.GetCoordinate(northEastNeighbor);
        while (northEastNeighbor != null)
        {
            // Occupied - add to valid reach and stop.
            if (board.IsOccupied(northEastNeighbor))
            {
                validCoordinates.Add(northEastNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            if (!board.IsOccupied(northEastNeighbor))
            {
                validCoordinates.Add(northEastNeighbor);
                northEastRow = northEastNeighbor.GetNeighborCoordinate(Direction.North).GetRow();
                northEastColumn = northEastNeighbor.GetNeighborCoordinate(Direction.East).GetColumn();
                northEastNeighbor = new Coordinate(northEastColumn, northEastRow);
                northEastNeighbor = board.GetCoordinate(northEastNeighbor);
            }
        }

        // NorthWest
        Row northWestRow = currentCoordinate.GetNeighborCoordinate(Direction.North).GetRow();
        Column northWestColumn = currentCoordinate.GetNeighborCoordinate(Direction.West).GetColumn();
        Coordinate northWestNeighbor = new Coordinate(northWestColumn, northWestRow);
        northWestNeighbor = board.GetCoordinate(northWestNeighbor);
        while (northWestNeighbor != null)
        {
            // Occupied - add to valid reach and stop.
            if (board.IsOccupied(northWestNeighbor))
            {
                validCoordinates.Add(northWestNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            if (!board.IsOccupied(northWestNeighbor))
            {
                validCoordinates.Add(northWestNeighbor);
                northEastRow = northWestNeighbor.GetNeighborCoordinate(Direction.North).GetRow();
                northEastColumn = northWestNeighbor.GetNeighborCoordinate(Direction.West).GetColumn();
                northWestNeighbor = new Coordinate(northEastColumn, northEastRow);
                northWestNeighbor = board.GetCoordinate(northWestNeighbor);
            }
        }

        // SouthEast
        Row southEastRow = currentCoordinate.GetNeighborCoordinate(Direction.South).GetRow();
        Column southEastColumn = currentCoordinate.GetNeighborCoordinate(Direction.East).GetColumn();
        Coordinate southEastNeighbor = new Coordinate(southEastColumn, southEastRow);
        southEastNeighbor = board.GetCoordinate(southEastNeighbor);
        while (southEastNeighbor != null)
        {
            // Occupied - add to valid reach and stop.
            if (board.IsOccupied(southEastNeighbor))
            {
                validCoordinates.Add(southEastNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            if (!board.IsOccupied(southEastNeighbor))
            {
                validCoordinates.Add(southEastNeighbor);
                southEastRow = southEastNeighbor.GetNeighborCoordinate(Direction.South).GetRow();
                southEastColumn = southEastNeighbor.GetNeighborCoordinate(Direction.East).GetColumn();
                southEastNeighbor = new Coordinate(southEastColumn, southEastRow);
                southEastNeighbor = board.GetCoordinate(southEastNeighbor);
            }
        }

        // SouthWest
        Row southWestRow = currentCoordinate.GetNeighborCoordinate(Direction.South).GetRow();
        Column southWestColumn = currentCoordinate.GetNeighborCoordinate(Direction.West).GetColumn();
        Coordinate southWestNeighbor = new Coordinate(southWestColumn, southWestRow);
        southWestNeighbor = board.GetCoordinate(southWestNeighbor);
        while (southWestNeighbor != null)
        {
            // Occupied - add to valid reach and stop
            if (board.IsOccupied(southWestNeighbor))
            {
                validCoordinates.Add(southWestNeighbor);
                break;
            }
            // Unoccupied - add to valid moves and continue
            else
            {
                validCoordinates.Add(southWestNeighbor);
                southWestRow = southWestNeighbor.GetNeighborCoordinate(Direction.South).GetRow();
                southWestColumn = southWestNeighbor.GetNeighborCoordinate(Direction.West).GetColumn();
                southWestNeighbor = new Coordinate(southWestColumn, southWestRow);
                southWestNeighbor = board.GetCoordinate(southWestNeighbor);
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
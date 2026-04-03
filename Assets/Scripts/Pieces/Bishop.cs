using UnityEngine;
using System.Collections.Generic;



public class Bishop : Piece
{
    


    public override bool CanReach(Coordinate coord)
    {
        Board board = Board.Instance;

        Coordinate currentCoordinate = this.GetCoordinate();
        List<Coordinate> validCoordinates = new List<Coordinate>();

        Coordinate northEastNeighbor = null;
        Coordinate northWestNeighbor = null;
        Coordinate southEastNeighbor = null;
        Coordinate southWestNeighbor = null;

        // NorthEast
        int northEastRow = (int)currentCoordinate.GetRow() + 1;
        int northEastColumn = (int)currentCoordinate.GetColumn() + 1;
        if (IsValidCoordinate(northEastColumn, northEastRow))
        {
            northEastNeighbor = new Coordinate((Column)northEastColumn, (Row)northEastRow);
            northEastNeighbor = board.GetCoordinate(northEastNeighbor);
        }
        
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
                northEastRow++;
                northEastColumn++;
                if (IsValidCoordinate(northEastColumn, northEastRow))
                {
                    northEastNeighbor = new Coordinate((Column)northEastColumn, (Row)northEastRow);
                    northEastNeighbor = board.GetCoordinate(northEastNeighbor);
                } else
                {
                    northEastNeighbor = null;
                }
            }
        }

        // NorthWest
        int northWestRow = (int)currentCoordinate.GetRow() + 1;
        int northWestColumn = (int)currentCoordinate.GetColumn() - 1;
        if (IsValidCoordinate(northWestColumn, northWestRow))
        {
            northWestNeighbor = new Coordinate((Column)northWestColumn, (Row)northWestRow);
            northWestNeighbor = board.GetCoordinate(northWestNeighbor);
        }
        
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
                northWestColumn--;
                northWestRow++;
                if (IsValidCoordinate(northWestColumn, northWestRow))
                {
                    northWestNeighbor = new Coordinate((Column)northWestColumn, (Row)northWestRow);
                    northWestNeighbor = board.GetCoordinate(northWestNeighbor);
                } else
                {
                    northWestNeighbor = null;
                }
            }
        }

        // SouthEast
        int southEastRow = (int)currentCoordinate.GetRow() - 1;
        int southEastColumn = (int)currentCoordinate.GetColumn() + 1;
        if (IsValidCoordinate(southEastColumn, southEastRow))
        {
            southEastNeighbor = new Coordinate((Column)southEastColumn, (Row)southEastRow);
            southEastNeighbor = board.GetCoordinate(southEastNeighbor);
        }
        
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
                southEastRow--;
                southEastColumn++;
                if (IsValidCoordinate(southEastColumn, southEastRow))
                {
                    southEastNeighbor = new Coordinate((Column)southEastColumn, (Row)southEastRow);
                    southEastNeighbor = board.GetCoordinate(southEastNeighbor);
                } else
                {
                    southEastNeighbor = null;
                }
            }
        }

        // SouthWest
        int southWestRow = (int)currentCoordinate.GetRow() - 1;
        int southWestColumn = (int)currentCoordinate.GetColumn() - 1;
        if (IsValidCoordinate(southWestColumn, southWestRow))
        {
            southWestNeighbor = new Coordinate((Column)southWestColumn, (Row)southWestRow);
            southWestNeighbor = board.GetCoordinate(southWestNeighbor);
        }
        
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
                southWestRow--;
                southWestColumn--;
                if (IsValidCoordinate(southWestColumn, southWestRow))
                {
                    southWestNeighbor = new Coordinate((Column)southWestColumn, (Row)southWestRow);
                    southWestNeighbor = board.GetCoordinate(southWestNeighbor);
                } else
                {
                    southWestNeighbor = null;
                }
            }
        }

        foreach (Coordinate coordinate in validCoordinates)
            if (coordinate.isEqual(coord)) return true;

        return false;
    }

    public override bool CanMove(Coordinate coord)
    {
        Board board = Board.Instance;
        bool canMove = true;

        if (!CanReach(coord)) canMove = false;
        if (board.IsOccupiedByFriendly(coord, this.GetColor())) canMove = false;
        
        return canMove;
    }

    public override bool IsInCaptureRange(Coordinate coord)
    {
        return CanReach(coord);
    }

}
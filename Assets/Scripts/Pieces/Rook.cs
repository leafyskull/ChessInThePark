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

        Coordinate northNeighbor = null;
        Coordinate southNeighbor = null;
        Coordinate eastNeighbor = null;
        Coordinate westNeighbor = null;

        // North
        int currentColumn = (int)currentCoordinate.GetColumn();
        int northRow = (int)currentCoordinate.GetRow() + 1;
        if (IsValidCoordinate(currentColumn, northRow))
        {
            northNeighbor = new Coordinate((Column)currentColumn, (Row)northRow);
            northNeighbor = Board.Instance.GetCoordinate(northNeighbor);
        }
        
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
                northRow++;
                if (IsValidCoordinate(currentColumn, northRow))
                {
                    northNeighbor = new Coordinate(northNeighbor.GetColumn(), (Row)northRow);
                    northNeighbor = board.GetCoordinate(northNeighbor);
                } else
                {
                    northNeighbor = null;
                }
            }
        }

        // South
        int southRow = (int)currentCoordinate.GetRow() - 1;
        if (IsValidCoordinate(currentColumn, southRow))
        {
            southNeighbor = new Coordinate((Column)currentColumn, (Row)southRow);
            southNeighbor = board.GetCoordinate(southNeighbor);
        }
        
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
                southRow--;
                if (IsValidCoordinate(currentColumn, southRow))
                {
                    southNeighbor = new Coordinate((Column)currentColumn, (Row)southRow);
                    southNeighbor = board.GetCoordinate(southNeighbor);
                } else
                {
                    southNeighbor = null;
                }
            }
        }

        // East
        int eastColumn = (int)currentCoordinate.GetColumn() + 1;
        int currentRow = (int)currentCoordinate.GetRow();
        if (IsValidCoordinate(eastColumn, currentRow))
        {
            eastNeighbor = new Coordinate((Column)eastColumn, (Row)currentRow);
            eastNeighbor = board.GetCoordinate(eastNeighbor);
        }
        
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
                eastColumn++;
                if (IsValidCoordinate(eastColumn, currentRow))
                {
                    eastNeighbor = new Coordinate((Column)eastColumn, (Row)currentRow);
                    eastNeighbor = board.GetCoordinate(eastNeighbor);
                } else
                {
                    eastNeighbor = null;
                }
            }
        }

        // West
        int westColumn = (int)currentCoordinate.GetColumn() - 1;
        if (IsValidCoordinate(westColumn, currentRow))
        {
            westNeighbor = new Coordinate((Column)westColumn, (Row)currentRow);
            westNeighbor = board.GetCoordinate(westNeighbor);
        }
        
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
                westColumn--;
                if (IsValidCoordinate(westColumn, currentRow))
                {
                    westNeighbor = new Coordinate((Column)westColumn, (Row)currentRow);
                    westNeighbor = board.GetCoordinate(westNeighbor);
                } else
                {
                    westNeighbor = null;
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
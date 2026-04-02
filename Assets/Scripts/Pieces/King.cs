using UnityEngine;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;



public class King : Piece
{



    public override bool CanReach(Coordinate coord)
    {
        Coordinate currentCoordinate = this.GetCoordinate();
        List<Coordinate> validCoordinates = new List<Coordinate>();
        List<Coordinate> potentialCoordinates = new List<Coordinate>();

        Coordinate north = currentCoordinate.GetNeighborCoordinate(Direction.North);
        Coordinate northEast = currentCoordinate.GetNeighborCoordinate(Direction.NorthEast);
        Coordinate east = currentCoordinate.GetNeighborCoordinate(Direction.East);
        Coordinate southEast = currentCoordinate.GetNeighborCoordinate(Direction.SouthEast);
        Coordinate south = currentCoordinate.GetNeighborCoordinate(Direction.South);
        Coordinate southWest = currentCoordinate.GetNeighborCoordinate(Direction.SouthWest);
        Coordinate west = currentCoordinate.GetNeighborCoordinate(Direction.West);
        Coordinate northWest = currentCoordinate.GetNeighborCoordinate(Direction.NorthWest);

        potentialCoordinates.Add(north);
        potentialCoordinates.Add(northEast);
        potentialCoordinates.Add(east);
        potentialCoordinates.Add(southEast);
        potentialCoordinates.Add(south);
        potentialCoordinates.Add(southWest);
        potentialCoordinates.Add(west);
        potentialCoordinates.Add(northWest);

        if (potentialCoordinates.Contains(coord)) return true;
        else return false;
    }

    public override bool CanMove(Coordinate coord)
    {
        Board board = Board.Instance;

        bool canMove = true;

        if (!CanReach(coord)) canMove = false;
        if (board.IsOccupiedByFriendly(coord, this.GetColor())) canMove = false;
        if (board.IsReachableByEnemy(coord, this.GetColor())) canMove = false;

        return canMove;
    }

    public override bool IsInCaptureRange(Coordinate coord)
    {
        return CanReach(coord);
    }

}
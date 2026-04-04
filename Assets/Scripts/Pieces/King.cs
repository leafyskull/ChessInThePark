using UnityEngine;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;



public class King : Piece
{



    public override bool CanReach(Coordinate coord)
    {
        Coordinate currentCoordinate = this.GetCoordinate();
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

        foreach (Coordinate coordinate in potentialCoordinates)
            if (coordinate != null)
                if (coordinate.isEqual(coord)) return true;
        
        return false;
    }

    public override bool CanMove(Coordinate coord)
    {
        Board board = Board.Instance;

        bool canMove = true;

        if (!CanReach(coord)) {
            canMove = false;
            Debug.Log("King cannot reach coordinate!");
        }

        if (board.IsOccupiedByFriendly(coord, this.GetColor())){
            canMove = false;
            Debug.Log("King cannot move because coordinate is occupied by a friendly piece!");
        }

        if (board.IsReachableByEnemy(coord, this.GetColor()))
        {
            canMove = false;
            Debug.Log("King cannot move because this coordinate is reachable by an enemy!");   
        }


        return canMove;
    }

    public override bool IsInCaptureRange(Coordinate coord)
    {
        return CanReach(coord);
    }

}
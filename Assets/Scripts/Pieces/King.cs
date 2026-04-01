using UnityEngine;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;



public class King : Piece
{



    public override bool CanReach(Coordinate coord)
    {
        Board board = Board.Instance;

        bool canMove = false;

        Coordinate currentCoordinate = this.getCoordinate();
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

        foreach (Coordinate coordinate in potentialCoordinates)
        {
            // If coordinate is empty and not watched by enemy - valid move
            if (!coordinate.IsOccupied())
                validCoordinates.Add(coordinate);
            // If coordinate is occupied by enemy and not watched - valid move

            // If coordinate is watched by enemy, or occupied by friendly - not valid move
        }




        return canMove;
    }

    public override bool CanMove(Coordinate coord)
    {
        // TODO: Implement
        return false;
    }

}
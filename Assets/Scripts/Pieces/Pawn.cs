using UnityEngine;
using System.Collections.Generic;



public class Pawn : Piece
{


    // TODO: Enpassant
    public override bool CanMove(Coordinate coord)
    {
        if (coord == null) return false;

        Coordinate currentCoordinate = this.getCoordinate();
        List<Coordinate> validCoordinates = new List<Coordinate>();
        bool canMove = false;

        Coordinate front = null;
        Coordinate frontRight = null;
        Coordinate frontLeft = null;
        Coordinate doubleFront = null;
        
        switch (this.GetColor()){
            case Color.White:
                front = new Coordinate(currentCoordinate.GetNeighborCoordinate(Direction.North));
                doubleFront = new Coordinate(front.GetNeighborCoordinate(Direction.North));
                frontRight = new Coordinate(currentCoordinate.GetNeighborCoordinate(Direction.NorthEast));
                frontLeft = new Coordinate(currentCoordinate.GetNeighborCoordinate(Direction.NorthWest));

                break;
            case Color.Black:
                front = new Coordinate(currentCoordinate.GetNeighborCoordinate(Direction.South));
                doubleFront = new Coordinate(front.GetNeighborCoordinate(Direction.South));
                frontRight = new Coordinate(currentCoordinate.GetNeighborCoordinate(Direction.SouthWest));
                frontLeft = new Coordinate(currentCoordinate.GetNeighborCoordinate(Direction.SouthEast));

                break;
        }

        if (front != null && !front.IsOccupied()) validCoordinates.Add(front);
        if (doubleFront != null && !PieceHasMoved() && !doubleFront.IsOccupied()) validCoordinates.Add(doubleFront);
        if (frontRight != null && frontRight.IsOccupied() && frontRight.GetOccupiedColor() != this.GetColor()) validCoordinates.Add(frontRight);
        if (frontLeft != null && frontLeft.IsOccupied() && frontLeft.GetOccupiedColor() != this.GetColor()) validCoordinates.Add(frontLeft);

        if (validCoordinates.Contains(coord)) canMove = true;

        return canMove;
    }





}
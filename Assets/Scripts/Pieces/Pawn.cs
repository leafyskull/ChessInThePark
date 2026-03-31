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
                front = Board.Instance.GetCoordinate(currentCoordinate.GetNeighborCoordinate(Direction.North));
                doubleFront = Board.Instance.GetCoordinate(front.GetNeighborCoordinate(Direction.North));
                frontRight = Board.Instance.GetCoordinate(currentCoordinate.GetNeighborCoordinate(Direction.NorthEast));
                frontLeft = Board.Instance.GetCoordinate(currentCoordinate.GetNeighborCoordinate(Direction.NorthWest));

                break;
            case Color.Black:
                front = Board.Instance.GetCoordinate(currentCoordinate.GetNeighborCoordinate(Direction.South));
                doubleFront = Board.Instance.GetCoordinate(front.GetNeighborCoordinate(Direction.South));
                frontRight = Board.Instance.GetCoordinate(currentCoordinate.GetNeighborCoordinate(Direction.SouthWest));
                frontLeft = Board.Instance.GetCoordinate(currentCoordinate.GetNeighborCoordinate(Direction.SouthEast));

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
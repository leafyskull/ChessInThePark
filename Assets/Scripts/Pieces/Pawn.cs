using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Unity.VisualScripting;



public class Pawn : Piece
{

    // TODO: Enpassant
    public override bool CanReach(Coordinate coord)
    {
        if (coord == null) return false;

        Board board = Board.Instance;

        Coordinate currentCoordinate = this.GetCoordinate();
        List<Coordinate> validCoordinates = new List<Coordinate>();
        bool canMove = false;

        Coordinate front = null;
        Coordinate doubleFront = null;
        
        switch (this.GetColor()){
            case Color.White:
                front = currentCoordinate.GetNeighborCoordinate(Direction.North);
                if (front!= null && !board.IsOccupied(front)) doubleFront = front.GetNeighborCoordinate(Direction.North);

                break;
            case Color.Black:
                front = currentCoordinate.GetNeighborCoordinate(Direction.South);
                if (front!= null && !board.IsOccupied(front)) doubleFront = front.GetNeighborCoordinate(Direction.South);

                break;
        }

        if (front != null) validCoordinates.Add(front);
        if (doubleFront != null && !PieceHasMoved() && !board.IsOccupied(front)) validCoordinates.Add(doubleFront);

        foreach (Coordinate coordinate in validCoordinates){
            if (coord != null)
                if (coord.isEqual(coordinate)) canMove = true;
        }
            
        return canMove;
    }

    public override bool IsInCaptureRange(Coordinate coord)
    {
        if (coord == null) return false;

        Coordinate currentCoordinate = this.GetCoordinate();
        List<Coordinate> validCoordinates = new List<Coordinate>();
        bool isInCaptureRange = false;

        Coordinate frontLeft = null;
        Coordinate frontRight = null;

        switch (this.GetColor())
        {
            case Color.White:
                frontLeft = currentCoordinate.GetNeighborCoordinate(Direction.NorthWest);
                frontRight = currentCoordinate.GetNeighborCoordinate(Direction.NorthEast);
                break;
            case Color.Black:
                frontLeft = currentCoordinate.GetNeighborCoordinate(Direction.SouthEast);
                frontRight = currentCoordinate.GetNeighborCoordinate(Direction.SouthWest);
                break;
        }

        if (frontLeft != null) validCoordinates.Add(frontLeft);
        if (frontRight != null) validCoordinates.Add(frontRight);

        foreach (Coordinate coordinate in validCoordinates)
        {
            if (coordinate.isEqual(coord)) isInCaptureRange = true;
        }

        return isInCaptureRange;
    }

    public override bool CanMove(Coordinate coord)
    {
        Board board = Board.Instance;

        bool canMove = false;

        if (CanReach(coord) && !board.IsOccupied(coord)) canMove = true;
        if (IsInCaptureRange(coord) && board.IsOccupiedByEnemy(coord, this.GetColor())) canMove = true;

        return canMove;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        Debug.Log("Child pointer down.");
    }





}
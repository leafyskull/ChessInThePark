using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;



public class Pawn : Piece
{

    // TODO: Enpassant
    public override bool CanMove(Coordinate coord)
    {
        if (coord == null) return false;

        Board board = Board.Instance;

        Coordinate currentCoordinate = this.GetCoordinate();
        List<Coordinate> validCoordinates = new List<Coordinate>();
        bool canMove = false;

        Coordinate front = null;
        Coordinate frontRight = null;
        Coordinate frontLeft = null;
        Coordinate doubleFront = null;
        
        switch (this.GetColor()){
            case Color.White:
                front = currentCoordinate.GetNeighborCoordinate(Direction.North);
                if (front!= null) doubleFront = front.GetNeighborCoordinate(Direction.North);
                frontRight = currentCoordinate.GetNeighborCoordinate(Direction.NorthEast);
                frontLeft = currentCoordinate.GetNeighborCoordinate(Direction.NorthWest);

                break;
            case Color.Black:
                front = currentCoordinate.GetNeighborCoordinate(Direction.South);
                if (front!= null) doubleFront = front.GetNeighborCoordinate(Direction.South);
                frontRight = currentCoordinate.GetNeighborCoordinate(Direction.SouthWest);
                frontLeft = currentCoordinate.GetNeighborCoordinate(Direction.SouthEast);

                break;
        }

        if (front != null && !board.IsOccupied(front)) validCoordinates.Add(front);
        if (doubleFront != null && !PieceHasMoved() && !board.IsOccupied(doubleFront)) validCoordinates.Add(doubleFront);
        if (frontRight != null && board.IsOccupiedByEnemy(frontRight, this.GetColor())) validCoordinates.Add(frontRight);
        if (frontLeft != null && board.IsOccupied(frontLeft) && board.IsOccupiedByEnemy(frontLeft, this.GetColor())) validCoordinates.Add(frontLeft);

        if (validCoordinates.Contains(coord)) canMove = true;

        return canMove;
    }

    public override bool CanReach(Coordinate coord)
    {
        // TODO: Implement
        return false;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        Debug.Log("Child pointer down.");
    }





}
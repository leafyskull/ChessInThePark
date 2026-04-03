using UnityEngine;
using System.Collections.Generic;



public class Queen : Piece
{



    public override bool CanReach(Coordinate coord)
    {
        Rook hypotheticalRook = new Rook();
        hypotheticalRook.SetCoordinate(this.GetCoordinate());
        Bishop hypotheticalBishop = new Bishop();
        hypotheticalBishop.SetCoordinate(this.GetCoordinate());

        if (hypotheticalBishop.CanMove(coord) || hypotheticalRook.CanMove(coord))
            return true;
        else
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
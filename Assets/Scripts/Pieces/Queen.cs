using UnityEngine;
using System.Collections.Generic;



public class Queen : Piece
{



    public override bool CanMove(Coordinate coord)
    {
        Rook hypotheticalRook = new Rook();
        hypotheticalRook.SetCoordinate(this.getCoordinate());
        Bishop hypotheticalBishop = new Bishop();
        hypotheticalBishop.SetCoordinate(this.getCoordinate());

        if (hypotheticalBishop.CanMove(coord) || hypotheticalRook.CanMove(coord))
            return true;
        else
            return false;
    }

    public override bool CanReach(Coordinate coord)
    {
        // TODO: Implement
        return false;
    }

}
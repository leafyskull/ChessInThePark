using UnityEngine;

public enum Column
{
    A, B, C, D, E, F, G, H
}

public enum Row
{
    One, Two, Three, Four, Five, Six, Seven, Eight
}


public class Coordinate : MonoBehaviour
{
    private Row row;
    private Column column;
    private Piece piece;
    



    public Coordinate(){
        this.column = Column.A;
        this.row = Row.One;
    }

    public Coordinate(Column column, Row row){
        this.column = column;
        this.row = row;
    }

    public bool isOccupied()
    {
        return (this.piece != null);
    }

    public Piece getPieceAt()
    {
        return this.piece;
    }
}

using UnityEngine;

public class BoardTile : MonoBehaviour
{
    private Coordinate coordinate;



    public void Initialize(Coordinate coordinate)
    {
        this.coordinate = coordinate;
    }

    public Coordinate GetCoordinate() { return this.coordinate; }

    


}

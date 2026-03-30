using System.Text.RegularExpressions;
using UnityEditor.Tilemaps;
using UnityEngine;


public abstract class Piece : MonoBehaviour
{
    private Coordinate coordinate;
    
    public Piece(){
        this.coordinate = new Coordinate();
    }

    public Coordinate getCoordinate()
    {
        return this.coordinate;
    }

    public void MoveTo(Coordinate coordinate)
    {
        this.coordinate = coordinate;
    }

    // TODO: Movement
    // TODO: Special piece logic
    // TODO: Click handling/interaction
}
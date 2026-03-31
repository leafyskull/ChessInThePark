using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Tilemaps;
using UnityEngine;


public enum Color
{
    White,
    Black
}

public abstract class Piece : MonoBehaviour
{
    private Color color;
    private bool hasMoved = false;
    private Coordinate coordinate;
    
    public Piece(){
        this.coordinate = new Coordinate();
    }

    public Coordinate getCoordinate() {return this.coordinate;}
    public void SetCoordinate(Coordinate coord) {this.coordinate = coord;}

    public Color GetColor() {return this.color;}
    public void SetColor(Color newcolor) {this.color = newcolor;}

    public void MoveTo(Coordinate coordinate)
    {
        hasMoved = true;
        this.coordinate = coordinate;
    }

    public bool PieceHasMoved()
    {
        return this.hasMoved;
    }

    public abstract bool CanMove(Coordinate coord);
    
    // TODO: Movement
    // TODO: Special piece logic
    // TODO: Click handling/interaction
}
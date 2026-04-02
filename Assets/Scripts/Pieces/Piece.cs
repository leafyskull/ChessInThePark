using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;


public enum Color
{
    White,
    Black
}

public abstract class Piece : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Color color;
    private bool hasMoved = false;
    private Coordinate coordinate;
    private LayerMask TileLayerMask;

    public bool dragging {get; private set;}
    Coordinate previousCoordinate = null;
    Vector3 originalPosition;
    Vector3 pointerOffsetInWorld;



    public void Initialize(Color color, Coordinate coord)
    {
        this.color = color;
        this.coordinate = coord;
        TileLayerMask = LayerMask.GetMask("Tile");
    }

    public Coordinate GetCoordinate() {return this.coordinate;}
    public void SetCoordinate(Coordinate coord) {this.coordinate = coord;}

    public Color GetColor() {return this.color;}
    public void SetColor(Color newcolor) {this.color = newcolor;}

    public void SetMoved() {this.hasMoved = true;}
    public bool GetHasMoved() {return this.hasMoved;}

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
    public abstract bool CanReach(Coordinate coord);
    
    // TODO: Movement
    // TODO: Special piece logic
    // TODO: Click handling/interaction

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;

        previousCoordinate = this.coordinate;
        originalPosition = this.transform.position;

        Vector3 pointerToWorld = ScreenToWorld(eventData.position);
        pointerOffsetInWorld = transform.position - pointerToWorld;

        Debug.Log($"Pointer down at {eventData.position}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging) return;

        Debug.Log("Currently dragging...");

        Vector3 pointerWorld = ScreenToWorld(eventData.position);
        Vector3 targetPosition = pointerWorld + pointerOffsetInWorld;

        transform.position = targetPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer down - now trying to place...");

        dragging = false;

        // Try to find a coordinate under the cursor
        Coordinate destination = FindCoordinateUnderCursor(eventData);

        // If found, assign CrewMember to station
        if (!this.CanMove(destination))
        {
            Debug.Log($"Cannot move to destination!");
            Debug.Log($"Returning to original coordinate: {previousCoordinate}");
            this.MoveTo(previousCoordinate);
            this.transform.position = originalPosition;
            return;
        }

        MoveTo(destination);
    }

    // ScreenToWorld(): Converts a screen position to it's position in the world.
    //
    // screenPosition: The position on the screen.
    private Vector3 ScreenToWorld(Vector2 screenPosition)
    {
        Camera cam = Camera.main;

        Vector3 sp = new Vector3(screenPosition.x, screenPosition.y, 0f);
        Vector3 world = cam.ScreenToWorldPoint(sp);
        world.z = 0f;
        return world;
    }

    private Coordinate FindCoordinateUnderCursor(PointerEventData eventData)
    {
        RaycastHit2D raycast = Physics2D.Raycast(ScreenToWorld(eventData.position), Vector3.forward, 100f, TileLayerMask);

        if (raycast.collider != null)
        {
            Coordinate coordinate = raycast.collider.GetComponentInParent<BoardTile>().GetCoordinate();
            Debug.Log($"Found coordinate: {coordinate.GetColumn()}{coordinate.GetRow()}");
            return coordinate;
        }

        return null;
    }



}
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;


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
    [SerializeField] private LayerMask TileLayerMask;

    public bool dragging {get; private set;}
    Coordinate previousCoordinate = null;
    Vector3 originalPosition;
    Vector3 pointerOffsetInWorld;



    public void Initialize(Color color, Coordinate coord)
    {
        this.color = color;
        this.coordinate = coord;
        this.hasMoved = false;
    }

    public Coordinate GetCoordinate() {return this.coordinate;}
    public void SetCoordinate(Coordinate coord) {this.coordinate = coord;}

    public Color GetColor() {return this.color;}
    public void SetColor(Color newcolor) {this.color = newcolor;}

    public void SetMoved() {this.hasMoved = true;}
    public bool GetHasMoved() {return this.hasMoved;}

    public void MoveTo(Coordinate coordinate)
    {
        this.coordinate = coordinate;
    }

    public void MoveToTile(BoardTile tile)
    {
        this.coordinate = tile.GetCoordinate();
        this.transform.position = tile.transform.position;
    }

    public bool PieceHasMoved()
    {
        return this.hasMoved;
    }

    public abstract bool CanMove(Coordinate coord);
    public abstract bool CanReach(Coordinate coord);
    public abstract bool IsInCaptureRange(Coordinate coord);

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
        Debug.Log("Pointer up - now trying to place...");

        dragging = false;

        // Try to find a coordinate under the cursor
        Coordinate destination = FindCoordinateUnderCursor(eventData);
        BoardTile destinationTile = FindTileUnderCursor(eventData);

        // If found, assign CrewMember to station
        if (!this.CanMove(destination))
        {
            Debug.Log($"Cannot move to destination!");
            Debug.Log($"Returning to original coordinate: {previousCoordinate}");
            this.MoveTo(previousCoordinate);
            this.transform.position = originalPosition;
            return;
        }

        // MoveTo(destination);
        //MoveToTile(destinationTile);
        Board.Instance.MovePieceToTile(this, destinationTile);
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
        Debug.Log($"EventData position: {ScreenToWorld(eventData.position)}");

        if (raycast.collider == null)
            Debug.Log("Raycast Collider is null!");

        if (raycast.collider != null)
        {
            Coordinate coordinate = raycast.collider.GetComponentInParent<BoardTile>().GetCoordinate();
            if (coordinate != null) Debug.Log($"Found coordinate: {coordinate.GetColumn()}{coordinate.GetRow()}");
            return coordinate;
        }

        return null;
    }

    private BoardTile FindTileUnderCursor(PointerEventData eventData)
    {
        RaycastHit2D raycast = Physics2D.Raycast(ScreenToWorld(eventData.position), Vector3.forward, 100f, TileLayerMask);

        if (raycast.collider == null)
            Debug.Log("Raycast Collider is null!");

        if (raycast.collider != null)
        {
            BoardTile tile = raycast.collider.GetComponentInParent<BoardTile>();
            return tile;
        }

        return null;
    }

    public bool IsValidCoordinate(int column, int row)
    {
        bool isValidCoordinate = true;

        if (column < 0 || row < 0) isValidCoordinate = false;
        if (column > 7 || row > 7) isValidCoordinate = false;

        return isValidCoordinate;
    }




}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    public Transform[] gridPoints; // array untuk menampung semua titik grid yang tersedia

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        SnapToClosestGrid();
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    // Fungsi untuk snap ke grid terdekat dari array gridPoints
    void SnapToClosestGrid()
{
    Transform closestGridPoint = null;
    float closestDistance = Mathf.Infinity;

    // Cari titik grid terdekat
    foreach (Transform gridPoint in gridPoints)
    {
        float distance = Vector3.Distance(transform.position, gridPoint.position);
        if (distance < closestDistance)
        {
            closestDistance = distance;
            closestGridPoint = gridPoint;
        }
    }

    // Snap ke titik grid terdekat jika ditemukan
    if (closestGridPoint != null)
    {
        Debug.Log("Snapping to: " + closestGridPoint.position);

        // Membulatkan posisi agar snap tepat ke grid
        transform.position = new Vector3(Mathf.Round(closestGridPoint.position.x),
                                         Mathf.Round(closestGridPoint.position.y),
                                         closestGridPoint.position.z);
    }
}

}

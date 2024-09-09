using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour
{
    public GameObject detector;
    public Vector3 pos_awal;
    public bool on_pos = false, on_tempel = false;
    public float rotationAngle = 90f; // Sudut rotasi tiap klik kanan
    private bool isDragging = false; // Apakah objek sedang di-drag

    // Start is called before the first frame update
    void Start()
    {
        pos_awal = transform.position;
    }

    void OnMouseDrag()
    {
        isDragging = true;
        Vector3 pos_mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        transform.position = new Vector3(pos_mouse.x, pos_mouse.y, -1f);
    }

    void OnMouseUp()
    {
        isDragging = false;
        if (on_pos)
        {
            transform.position = detector.transform.position;
            on_tempel = true;
        }
        else
        {
            transform.position = pos_awal;
            on_tempel = false;
        }
    }

    void OnTriggerStay2D(Collider2D objek)
    {
        // Jika benda menyentuh detector
        if (objek.gameObject == detector)
        {
            on_pos = true;
        }
    }

    void OnTriggerExit2D(Collider2D objek)
    {
        // Jika benda tidak menyentuh detector
        if (objek.gameObject == detector)
        {
            on_pos = false;
        }
    }

    void Update()
    {
        // Deteksi klik kanan mouse hanya jika objek sedang di-drag
        if (isDragging && Input.GetMouseButtonDown(1))
        {
            RotateTetromino();
        }
    }

    void RotateTetromino()
    {
        // Rotasi GameObject sebesar rotationAngle pada sumbu Z
        transform.Rotate(0f, 0f, rotationAngle);
    }
}

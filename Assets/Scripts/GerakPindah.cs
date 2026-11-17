using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerakPindah : MonoBehaviour
{
    float speed = 3f;
    public Sprite[] sprites;

    private Vector3 screenPoint;
    private Vector3 offset;
    private float firstY;

    void Start()
    {
        if (sprites == null || sprites.Length == 0)
        {
            return;
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            int index = UnityEngine.Random.Range(0, sprites.Length);
            spriteRenderer.sprite = sprites[index];
        }

        firstY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(
            transform.position.x - (speed * Time.deltaTime),
            transform.position.y,
            transform.position.z
        );
    }

    private Vector3 GetMousePosition()
    {
        try
        {
            return Input.mousePosition;
        }
        catch (InvalidOperationException)
        {
            var mouse = UnityEngine.InputSystem.Mouse.current;
            if (mouse == null)
            {
                return Vector3.zero;
            }

            var pos = mouse.position.ReadValue();
            return new Vector3(pos.x, pos.y, 0f);
        }
    }

    void OnMouseDown()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            return;
        }

        firstY = transform.position.y;
        screenPoint = mainCamera.WorldToScreenPoint(transform.position);
        Vector3 mousePos = GetMousePosition();
        offset = transform.position - mainCamera.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, screenPoint.z)
        );
    }

    void OnMouseDrag()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            return;
        }

        Vector3 mousePos = GetMousePosition();
        Vector3 curScreenPoint = new Vector3(mousePos.x, mousePos.y, screenPoint.z);
        Vector3 curPosition = mainCamera.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = new Vector3(curPosition.x, curPosition.y, transform.position.z);
    }

    private void OnMouseUp()
    {
        transform.position = new Vector3(transform.position.x, firstY, transform.position.z);
    }
}

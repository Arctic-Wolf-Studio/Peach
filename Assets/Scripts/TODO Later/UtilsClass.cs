using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass {

    private static Camera mainCamera;

    public static Vector3 GetMouseWorldPosition() {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = mainCamera.nearClipPlane;
        return mouseWorldPosition;
    }

    public static Vector2 GetMouseWorld2DPosition() { 
        if (mainCamera == null) mainCamera = Camera.main;

        Vector2 mouseWorld2DPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return mouseWorld2DPosition;
    }
}
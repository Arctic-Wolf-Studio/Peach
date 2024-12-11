using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour {
    public FollowCursor Instance;

    public GameObject crosshair;
    public GameObject target;

    [HideInInspector] public Vector3 difference, MouseCursorPosition;
    [HideInInspector] public float rotationZ;

    private void Awake() {
        Instance = this;
        crosshair = GameObject.FindGameObjectWithTag("Crosshair").gameObject;
        target = GameObject.FindGameObjectWithTag("Weapon Pivot Point").gameObject;
    }

    void Update() {
        UpdateFollowCursor();
    }

    public void UpdateFollowCursor() {
        MouseCursorPosition = UtilsClass.GetMouseWorldPosition();
        crosshair.transform.position = new Vector2(MouseCursorPosition.x, MouseCursorPosition.y);
        Cursor.visible = false;

        difference = target.transform.position - crosshair.transform.position;
        difference.Normalize();

        rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        target.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
}

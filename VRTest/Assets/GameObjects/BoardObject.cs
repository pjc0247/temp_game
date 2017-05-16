using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObject : MonoBehaviour {
    public float x = 1;
    public float y = 1;

	protected virtual void Start () {
        SetPosition2D(x, y);
    }

    public Vector2 GetPosition2D()
    {
        return new Vector2(x, y);
    }
    public void SetPosition2D(float x, float y)
    {
        this.x = x;
        this.y = y;

        transform.localPosition =
            new Vector3(x, transform.localPosition.y, y) -
            new Vector3(4.5f, 0, 4.5f);
    }
}

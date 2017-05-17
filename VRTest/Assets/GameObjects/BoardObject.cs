using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObject : MonoBehaviour {
    public float x = 1;
    public float y = 1;

    // offset : 실제 타일 위치와 관련 없는 위치
    public float offsetX = 0; 
    public float offsetY = 0;

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
            new Vector3(4.5f, 0, 4.5f) + 
            new Vector3(offsetX, 0, offsetY);
    }
}

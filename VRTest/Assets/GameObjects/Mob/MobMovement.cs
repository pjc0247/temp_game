using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour {
    public float step = 0.05f;
    public float interval = 0.25f;

    private BoardObject boardXY;
    private Direction direction;

    private bool moving = true;
    private float movementScale = 1.0f;

	void Start () {
        boardXY = gameObject.GetComponent<BoardObject>();
        direction = Direction.Right;

        StartCoroutine(MoveFunc());
	}

    public void SetMovementScale(float f)
    {
        movementScale = f;
    }

    float GetAngleByDirection(float angle, Direction dir)
    {
        // 0  / 360 문제
        if (dir == Direction.Right)
        {
            return 0;
        }
        if (dir == Direction.Left) return 180;
        if (dir == Direction.Up)
        {
            if (angle >= 0 && angle <= 180)
                return 90;
            else 
                return -270;
        }
        if (dir == Direction.Down)
        {
            if (angle >= 270 - 90 && angle <= 270 + 90)
                return 270;
            else return -90;
        }
        return 0;
    }

    IEnumerator MoveFunc()
    {
        var board = GameBoard.instance.board;

        while (true)
        {
            var x = boardXY.x;
            var y = boardXY.y;

            if (direction == Direction.Right)
                x += step * movementScale;
            else if (direction == Direction.Left)
                x -= step * movementScale;
            else if (direction == Direction.Up)
                y -= step * movementScale;
            else if (direction == Direction.Down)
                y += step * movementScale;

            boardXY.SetPosition2D(x, y);

            float xp = 0.0f;
            float xy = 0.0f;
            if (direction == Direction.Left)
                xp = -0.95f;
            if (direction == Direction.Up)
                xy = -0.95f;

            var ix = Mathf.Max(0, (int)Mathf.Floor(x - xp));
            var iy = Mathf.Max(0, (int)Mathf.Floor(y - xy));
            direction = board[iy, ix];

            var original = transform.rotation.eulerAngles.y;
            var target = GetAngleByDirection(original, direction);

            //Debug.Log(string.Format("Orig : {0} / Target : {1}", original, target));
            transform.rotation =
                Quaternion.Euler(0, original + (target - original) * 0.25f, 0);

            if (ix == 5 && iy == 0)
                ReachToEnd();

            yield return new WaitForSeconds(interval);
        }
    }
    IEnumerator AscensionFunc()
    {
        for (int i = 0; i < 60; i++)
        {
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                transform.localPosition.y + (1.7f - transform.localPosition.y) * 0.1f,
                transform.localPosition.z);
            gameObject.SetOpacity(1.0f - (transform.localPosition.y / 1.7f));

            yield return new WaitForEndOfFrame();
        }
    }

    void ReachToEnd()
    {
        GlobalGFX.instance.StartRedOverlay();

        StopAllCoroutines();
        StartCoroutine(AscensionFunc());

        Destroy(gameObject, 1);
    }
}

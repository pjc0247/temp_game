using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour {
    public float step = 0.05f;
    public float interval = 0.25f;

    private BoardObject boardXY;
    private Direction direction;

	void Start () {
        boardXY = gameObject.GetComponent<BoardObject>();
        direction = Direction.Right;

        StartCoroutine(MoveFunc());
	}

	IEnumerator MoveFunc()
    {
        var board = GameBoard.instance.board;

        while (true)
        {
            var x = boardXY.x;
            var y = boardXY.y;

            if (direction == Direction.Right)
                x += step;
            else if (direction == Direction.Left)
                x -= step;
            else if (direction == Direction.Up)
                y -= step;
            else if (direction == Direction.Down)
                y += step;

            boardXY.SetPosition2D(x, y);

            float xp = -0.0f;
            if (direction == Direction.Left)
                xp = -0.5f;

            var ix = Mathf.Max(0, (int)Mathf.Floor(x - xp));
            var iy = Mathf.Max(0, (int)Mathf.Floor(y));
            direction = board[iy, ix];

            yield return new WaitForSeconds(interval);
        }
    }
}

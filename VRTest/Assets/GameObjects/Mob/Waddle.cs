using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(10)]
public class Waddle : MonoBehaviour {
    public float amount = 5;
    public float interval = 0.25f;

	void Start () {
        StartCoroutine(WaddleFunc());
	}

    IEnumerator WaddleFunc()
    {
        while (true)
        {
            var prev = transform.rotation.eulerAngles;

            transform.rotation = Quaternion.Euler(prev.x, prev.y, -amount);
            yield return new WaitForSeconds(interval);
            transform.rotation = Quaternion.Euler(prev.x, prev.y, amount);
            yield return new WaitForSeconds(interval);
        }
    }
}

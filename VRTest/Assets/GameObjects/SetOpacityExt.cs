using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SetOpacityExt {
    public static void SetOpacity(this GameObject _this, float opacity) {
        var meshRenderers = _this.GetComponentsInChildren<MeshRenderer>();

        foreach (var mr in meshRenderers)
            mr.material.color = new Color(1, 1, 1, opacity);
    }
}

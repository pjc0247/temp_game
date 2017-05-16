using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SetOpacityExt {
    public static void SetOpacity(this GameObject _this, float opacity) {
        var meshRenderers = _this.GetComponentsInChildren<MeshRenderer>();

        foreach (var mr in meshRenderers)
            mr.material.color = new Color(1, 1, 1, opacity);
    }

    public static void SetColor(this GameObject _this, Color color)
    {
        var meshRenderers = _this.GetComponentsInChildren<MeshRenderer>();
        var skinnedMeshRenderers = _this.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (var mr in meshRenderers)
            mr.material.color = color;
        foreach (var mr in skinnedMeshRenderers)
            mr.material.color = color;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public Direction direction;
    public TileType type;
    public int x, y;
    public bool occupied = false;

    private Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;

        SetupTileTexture();

        /* // DebugText
        var textObj = new GameObject();
        textObj.transform.SetParent(transform);
        textObj.transform.localPosition = Vector3.zero;
        var text = textObj.AddComponent<TextMesh>();
        text.text = direction.ToString().Substring(0, 1);
        text.fontSize = 10;
        text.anchor = TextAnchor.MiddleCenter;
        */
    }
    void SetupTileTexture()
    {
        Texture2D texture = null;

        if (type == TileType.Normal)
            texture = Resources.Load<Texture2D>("Env/Tileset/TileSet_03");
        else if (type == TileType.Path)
            texture = Resources.Load<Texture2D>("Env/Tileset/TileSet_06");

        material.mainTexture = texture;
    }

    public bool IsBuildable()
    {
        return type == TileType.Normal && occupied == false;
    }

    public void Highlight()
    {
        //material.SetFloat("_Opacity", 1.0f);
    }
    public void Unhighlight()
    {
        //material.color = Color.red;
    }
    public void AnimateOpacity(float f)
    {
        StopAllCoroutines();
        StartCoroutine(OpacityFunc(f));
    }

    IEnumerator OpacityFunc(float opacityTo)
    {
        float initialOpacity = material.GetFloat("_Opacity");
        float step = (opacityTo - initialOpacity) / 10;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForEndOfFrame();

            var opacity = initialOpacity + step * i;
            material.color = new Color(opacity, opacity, opacity);
            material.SetFloat("_Opacity", opacity);
        }
    }
}

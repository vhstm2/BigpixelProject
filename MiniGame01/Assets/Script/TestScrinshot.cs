using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScrinshot : MonoBehaviour
{
    public new Camera camera;

    public Image shotImage;

    private int resWidth = Screen.width;
    private int resHeight = Screen.height;

    public Sprite[] sprites;

    private Coroutine st;

    public RenderTexture rt;

    private void Start()
    {
        Invoke("SaveStart", 3.0f);
    }

    public void SaveStart()
    {
        st = StartCoroutine(SaveSprite());
    }

    private int a = 0;

    public void Shot()
    {
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        //camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        camera.Render();
        camera.targetTexture = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        RenderTexture.active = rt;
        //RenderTexture.active = null; // JC: added to avoid errors
        // Destroy(rt);
        screenShot.Apply();

        Sprite sp =
            Sprite.Create(screenShot,
            new Rect(0.0f,
                     0.0f,
                     screenShot.width,
                     screenShot.height),
                     shotImage.rectTransform.position,
                     100);

        sprites[a] = sp;
        a++;
        //shotImage.sprite = sp;
    }

    private void LateUpdate()
    {
        Shot();
    }

    private IEnumerator SaveSprite()
    {
        while (true)
        {
            Shot();
            yield return null;
            yield return new WaitForEndOfFrame();
        }
    }

    public void Replay()
    {
        StartCoroutine(plays());
    }

    private int n = 0;

    private IEnumerator plays()
    {
        // StopCoroutine(st);
        while (true)
        {
            shotImage.sprite = sprites[n];
            //shotImage.sprite.texture.Apply();

            n++;
            if (n >= sprites.Length)
                n = 0;
            yield return new WaitForFixedUpdate();
            yield return null;
        }
    }
}
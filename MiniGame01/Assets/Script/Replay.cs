using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class Replay : MonoBehaviour
{
    private int width = 1920, height = 1080;

    public screen sc;

    Vector2 size = Vector2.zero;

    private void Start()
    {
        size.Set(width, height);

    }


    public IEnumerator Shot(Image frame)
    {
        yield return new WaitForEndOfFrame();
     

        Texture2D tex = new Texture2D((int)size.x,(int)size.y,
                                      TextureFormat.RGB565, false);

        tex.ReadPixels(new Rect(0, 0, (int)size.x, (int)size.y), 0, 0, true);
        tex.Apply();
        var imageByte = tex.EncodeToPNG();

        // DestroyImmediate(tex);

        var t = new Texture2D(2, 2, TextureFormat.RGB24, false);
        t.LoadImage(imageByte);

        var spr = Sprite.Create(t,
                  new Rect(0.0f, 0.0f, tex.width, tex.height),
                  new Vector2(0.5f, 0.5f), 100.0f);


        frame.sprite = spr;

    
    }

    public List<Sprite> texs = new List<Sprite>();

    public IEnumerator FrameSave(Image frame)
    {
      //  while (gmr.gameSceneState == GameManager.GameSceneState.GameStart)
        {
            yield return new WaitForEndOfFrame();
            yield return null;
            Texture2D tex = new Texture2D(Screen.width, Screen.height,
                                       TextureFormat.RGB24, false);

            tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, true);
            tex.Apply();
            var imageByte = tex.EncodeToPNG();

            // DestroyImmediate(tex);

            var t = new Texture2D(2, 2, TextureFormat.RGB24, false);
            t.LoadImage(imageByte);

            var spr = Sprite.Create(t,
                      new Rect(0.0f, 0.0f, tex.width, tex.height),
                      new Vector2(0.5f, 0.5f), 100.0f);
            texs.Add(spr);
        }
    }

    public IEnumerator ChangeFrame(Image frame)
    {
        for (int i = 0; i < texs.Count; i++)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(1);

            yield return null;
            frame.sprite = texs[i];
        }
    }

    #region b

    //private RenderTexture renderTexture;
    //private Texture2D screenTexure;
    //public Texture2D[] snapshot;
    // Start is called before the first frame update
    //private void Start()
    //{
    //    //renderTexture = new RenderTexture(width, height, 24);
    //    //Camera.main.targetTexture = renderTexture;
    //    //screenTexure = new Texture2D(width, height, TextureFormat.RGB24, false);
    //}
    //public byte[] AsPng()
    //{
    //    RenderTexture currentActive = RenderTexture.active;
    //    Camera.main.Render();
    //    RenderTexture.active = renderTexture;
    //    screenTexure.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    //    RenderTexture.active = currentActive;
    //    return screenTexure.EncodeToPNG();
    //}

    //public IEnumerator TakeSnapshot()
    //{
    //    for (int i = 0; i < 30; i++)
    //    {
    //        yield return new WaitForSeconds(0.2F);

    //        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, true);
    //        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    //        texture.Apply();
    //        snapshot[i] = texture;
    //    }
    //}

    #endregion b
}
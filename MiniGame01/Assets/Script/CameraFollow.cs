using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //public Transform Target;

    //Vector3 CameraPos;

    //float Posx = 1.0f;
    //float Posz = 1.7f;

    //private bool CameraMv = false;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    CameraPos = transform.position;
    //}

    // Update is called once per frame
    //void LateUpdate()
    //{
    //    Vector3 offset = new Vector3(Target.position.x, Target.position.y+1.0f, Target.position.z);

    //    if (Target.position.x <= -Posx) offset.x = -Posx;
    //    if (Target.position.x > Posx) offset.x = Posx;
    //    if (Target.position.z <= -Posz) offset.z = -Posz;
    //    if (Target.position.z > Posz) offset.z = Posz;

    //    transform.position = Vector3.Lerp(transform.position, offset, Time.deltaTime * 2.0f);
    //    //transform.position = Vector3.Lerp(transform.position, Target.position + Vector3.up *1.0f, Time.deltaTime*10);
    //}

    public void OnEnable()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (가로 / 세로)
        print("scaleheight = " + scaleheight);
        float scalewidth = 1f / scaleheight;
        print("scalewidth = " + scalewidth);
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
            print("y = " + rect.y);
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
            print("x = " + rect.x);
        }
        camera.rect = rect;
    }

    private void OnPreCull() => GL.Clear(true, true, Color.black);
}
using UnityEngine;
using System.Collections;

public class CameraScreenResolution : MonoBehaviour {
	public bool maintainWidth=true;
	[Range(-1,1)]
	public int adaptPosition;


	float defaultWidth;
	float defaultHeight;


	Vector3 CameraPos;

	// Use this for initialization
	void Start () {
	
		CameraPos = Camera.main.transform.position;
        defaultHeight = Camera.main.fieldOfView;
        defaultWidth = Camera.main.fieldOfView * Camera.main.aspect;
    }
	
	// Update is called once per frame
	void Update () {
	
		if (maintainWidth) {

            Camera.main.fieldOfView = defaultWidth / Camera.main.aspect;
            Camera.main.transform.position= new Vector3(CameraPos.x,CameraPos.y + adaptPosition*(defaultHeight-Camera.main.fieldOfView),CameraPos.z);
            
		} else {
            Camera.main.transform.position = new Vector3(adaptPosition * adaptPosition * (defaultWidth - Camera.main.fieldOfView * Camera.main.aspect), CameraPos.y, CameraPos.z);

        }


	}
}

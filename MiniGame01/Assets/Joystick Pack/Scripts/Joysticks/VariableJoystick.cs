using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VariableJoystick : Joystick
{
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;
    [SerializeField] private JoystickType joystickType = JoystickType.Fixed;

    private Vector2 fixedPosition = Vector2.zero;

    //private PlayerController playerController;

    public void SetMode(JoystickType joystickType)
    {
        this.joystickType = joystickType;
        if (joystickType == JoystickType.Fixed)
        {
            background.anchoredPosition = fixedPosition;
            //background.gameObject.SetActive(true);
        }
        // else
        //background.gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();
        fixedPosition = background.anchoredPosition;
        //playerController = FindObjectOfType<PlayerController>();
        SetMode(joystickType);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (joystickType != JoystickType.Fixed)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            //RefManager.instance.angle.rotation = RefManager.instance.angle.rotation;
            //background.gameObject.SetActive(true);
        }
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //if(joystickType != JoystickType.Fixed)
        //background.gameObject.SetActive(false);

        //background.position = Vector2.zero + background.sizeDelta;

        //switch (RefManager.instance.player.direction)
        //{
        //    case DirectionEnum.Down:
        //        RefManager.instance.player.anim.Play("IdleDown");
        //        break;
        //    case DirectionEnum.Up:
        //        RefManager.instance.player.anim.Play("IdleUp");
        //        break;
        //    case DirectionEnum.Left:
        //        RefManager.instance.player.anim.Play("IdleLeft");
        //        break;
        //    case DirectionEnum.Right:
        //        RefManager.instance.player.anim.Play("IdleRight");
        //        break;
        //}

        //RefManager.instance.player.b_IdleAnim = true;
        //RefManager.instance.gaugeSlider.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        var dir = (background.position - handle.position);
        var angleZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


        if (joystickType == JoystickType.Dynamic && magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;

            base.HandleInput(magnitude, normalised, radius, cam);
        }


    }

}
    public enum JoystickType { Fixed, Floating, Dynamic };
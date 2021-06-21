using UnityEngine;
using UnityEngine.UI;

public class contentOnoff : MonoBehaviour
{
    //2개
    public GameObject[] OnoffGob;

    public Button[] buttons;

    public int contentIdx = 0;

    public enum ButtonRot { Left, Right };

    private void Awake()
    {
        if (contentIdx == 0 || UserDataManager.instance.Player_Eqip.idx <= 8)
        {
            contentIdx = 0;
            //     buttonAndGob(false, true, true, false);
        }
        if (contentIdx == 1 || UserDataManager.instance.Player_Eqip.idx >= 9)
        {
            contentIdx = 1;
            //     buttonAndGob(true, false, false, true);
        }
    }

    private void buttonAndGob(bool flag1, bool flag2, bool flag3, bool flag4)
    {
        buttons[0].interactable = flag1;
        buttons[1].interactable = flag2;
        OnoffGob[0].SetActive(flag3);
        OnoffGob[1].SetActive(flag4);
    }

    public void LeftButton()
    {
        UserDataManager.instance.character = Character.NORMAL;
        ButttonProsses(ButtonRot.Left, false, false, true, true);
    }

    public void RightButton()
    {
        UserDataManager.instance.character = Character.RARE;
        ButttonProsses(ButtonRot.Right, false, true, false, true);
    }

    private void ButttonProsses(ButtonRot rot, bool flag1, bool flag2, bool flag3, bool flag4)
    {
        OnoffGob[contentIdx].SetActive(flag1);

        if (rot == ButtonRot.Left)
            contentIdx--;
        else if (rot == ButtonRot.Right)
            contentIdx++;

        buttons[0].interactable = flag2;
        buttons[1].interactable = flag3;
        OnoffGob[contentIdx].SetActive(flag4);
    }
}
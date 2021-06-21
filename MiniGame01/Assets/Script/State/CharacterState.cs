using System.Collections.Generic;
using UnityEngine;

public class CharacterState : GameStateBase
{
    public CharacterState(StateMachine machine) : base(machine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Character Scene Start");
        machine.currentState.FadeIn(machine.fadeob);
    }

    public override void UpdateBase()
    {
    }

    public override void End(string Scenename)
    {
        Debug.Log("Character Scene End");

        //케릭터씬 탈출
        if (UserDataManager.instance.Player_Eqip.select_Character.E_charcter == Character.NULL)
        {
            Debug.Log("선택안됨");
            int n = UserDataManager.instance.Player_Eqip.idx;
            //현재 선택햇던 idx가 노말케릭이면
            if (UserDataManager.instance.Player_Eqip.user_Characters[n].E_charcter == Character.NORMAL)
            {
                //선택완료
                UserDataManager.instance.Player_Eqip.select_Character =
                   UserDataManager.instance.Player_Eqip.user_Characters[n];
            }
            //현재 선택햇던 idx가 레어케릭이고
            else if (UserDataManager.instance.Player_Eqip.user_Characters[n].E_charcter == Character.RARE)
            {
                // 구매완료된 케릭터면
                if (UserDataManager.instance.Player_Eqip.user_Characters[n].is_purchase == true)
                {
                    //선택완료
                    UserDataManager.instance.Player_Eqip.select_Character =
                        UserDataManager.instance.Player_Eqip.user_Characters[n];
                }
                //구매완료된 케릭터가 아니라면
                else if (UserDataManager.instance.Player_Eqip.user_Characters[n].is_purchase == false)
                {
                    List<charcter_infomation> list = new List<charcter_infomation>(UserDataManager.instance.Player_Eqip.user_Characters);
                    //전체케릭터 중 true된 애들 다 뽑아서 랜덤으로 또 뽑은애를 선택함.
                    var listALL = list.FindAll(x => x.is_purchase == true);
                    var rnd = Random.Range(0, list.Count);

                    UserDataManager.instance.Player_Eqip.select_Character =
                    UserDataManager.instance.Player_Eqip.user_Characters[rnd];
                }
            }
        }

        FadeOut(machine.fadeob, Scenename);
    }
}
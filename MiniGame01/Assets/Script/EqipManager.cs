using UnityEngine;

public class EqipManager : MonoBehaviour
{
    public int idx = 0;

    public void eqip()
    {
        idx = UserDataManager.instance.Player_Eqip.idx;
    }
}
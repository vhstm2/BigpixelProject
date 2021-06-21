using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Achiement
{
    [Tooltip("업적")] public Image penal;

    [Tooltip("업적라인")] public Image Line;

    [Tooltip("완료 체크")] public Image check;

    [Tooltip("업적Text")] public Text Achiement_Text;

    [Tooltip("Korea_Text")] public string KO_Text;
    public float Ko_Lenght;

    [Tooltip("Japan_Text")] public string JA_Text;
    public float JA_Lenght;

    [Tooltip("English_Text")] public string EN_Text;
    public float EN_Lenght;

    [Tooltip("업적열림?")]
    private bool achiement_success;

    public bool Achiement_success
    {
        get { return achiement_success; }
        set
        {
            achiement_success = value;
            var achiement = GameObject.FindObjectOfType<Achiement_Abillity>();

            if (value)
            {
                check.gameObject.SetActive(true);
                if (UserDataManager.instance.Player_Eqip.Achiement[Achiement_Abillity.checkint] == false)
                {
                    //보상 주고요
                    UserDataManager.instance.opalmoney += achiement_Text.Compensation_Int;
                    //true로 바꿈.
                    UserDataManager.instance.Player_Eqip.Achiement[Achiement_Abillity.checkint] = true;
                }

                //선택한녀석 == 체크가된녀석이 같으면
                if (achiement.selectInt == Achiement_Abillity.checkint)
                {
                    //보상포스트잇부분 체크이미지,컴플릿트 텍스트 활성화.
                    achiement.compensation.Check.gameObject.SetActive(true);
                    // achiement.compensation.completed_Text.gameObject.SetActive(true);

                    //   achiement.StartCoroutine(achiement.text_Tiping());
                }
            }
        }
    }

    public Achiement_Text achiement_Text;

    public int currentInt;
}

[System.Serializable]
public class Achiement_Compensation
{
    [Tooltip("업적 KOREA")] public Text KO_Text;
    [Tooltip("업적 Japan")] public Text JA_Text;

    [Tooltip("업적 보상 Image")] public Image Compensation;
    [Tooltip("업적 보상 체크")] public Image Check;

    [Tooltip("업적 보상 개수")] public Text Compensation_Count;
    [Tooltip("업적 보상 텍스트")] public Text completed_Text;
}

[System.Serializable]
public class Achiement_Text
{
    public string Ko_Text;
    public string JA_Text;
    public string EN_Text;

    public string Compensation_Name;
    public Sprite Compensation_Image;
    public int Compensation_Int;
}
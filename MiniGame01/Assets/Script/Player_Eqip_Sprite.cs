using UnityEngine;

public class Player_Eqip_Sprite : MonoBehaviour
{
    public PlayerCtrl player;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer ChildSpriteRenderer;

    public Material matEffect;

    public ParticleSystem ParticleEffect;


    private void Awake()
    {
        charcter_infomation _charcter = UserDataManager.instance.Player_Eqip.select_Character;
        if (_charcter.E_charcter == Character.NULL)
        {
            _charcter = UserDataManager.instance.Player_Eqip.user_Characters[0];
            UserDataManager.instance.Player_Eqip.select_Character = _charcter;
            
        }
        player.charcter_Info = _charcter;
    }


    private void Start()
    {
        //케릭터씬 탈출
        if (UserDataManager.instance.Player_Eqip.select_Character.GamePlayCharacter == null)
        {
            Sprite s = UserDataManager.instance.Player_Eqip.user_Characters[0].GamePlayCharacter;

            spriteRenderer.sprite = s;
        }
        else
        {
            Sprite sprite = player.charcter_Info.GamePlayCharacter;
            spriteRenderer.sprite = sprite;
        }

        matEffect.SetTexture("_MainTex", spriteRenderer.sprite.texture);

        //ChildSpriteRenderer.sprite =
        //   UserDataManager.instance.EffectUserSprite[UserDataManager.instance.Player_Eqip.idx];
    }

    public Transform particlePos()
    {
        return ParticleEffect.transform;
    }

    public (Color color, Collider coll) ColorAlpha_transparecy(bool transparecy)
    {
        var PColor = spriteRenderer.color;

        if (transparecy)
        {
            return (new Color(PColor.r, PColor.g, PColor.b, 0.3f), player.coll);
        }
        else
        {
            player.coll.enabled = true;
            return (Color.white, player.coll);
        }
    }
}
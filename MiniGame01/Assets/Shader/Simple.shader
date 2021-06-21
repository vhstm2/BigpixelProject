Shader "SH_Shader/Simple"
{
	Properties
	{
		_Color("BaseColor" , Color) = (1,1,1,1)
	}

		SubShader
	{
		Pass
		{
			CGPROGRAM
			//쉐이더관련파일을 프로젝트로 가져오기
			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			//vecter4와 같음
			//같은 이름으로 선언하면 프로퍼티로 할당한 내부의 값을 저장함
			fixed4 _Color;

			struct vertexInput	//정점데이터 구조체
			{
				//어디에서 사용될지 모름	: 용도(시멘틱)
				float3 positionOnObjectSpace : POSITION;
			};

			//버텍스내용이 끝나면 전단될 데이터
			struct fragmentInput
			{
				float4 positionOnClipSpace : SV_POSITION;
			};

			fragmentInput vert(vertexInput input)
			{
				//정점의 위치를 오브젝트공간상의 위치로 변환
				//제공되는 함수(UnityObjectToClipPos)
				float4 positionOnClipSpace =
					UnityObjectToClipPos(input.positionOnObjectSpace);

				fragmentInput output;
				//버텍스쉐이더에 출력됨
				output.positionOnClipSpace = positionOnClipSpace;

				return output;
			}

			//어떤정점위치를 채울 컬러값을 리턴
										//시멘틱(SV_TARGET) 랜더버퍼에 쓰겟다
			//이미 정점값이 결정되있는 상태라서 컬러값을 바로 보여줌.
			fixed4 frag(fragmentInput input) : SV_TARGET
			{
				return _Color;
			}

			ENDCG
		}
	}
}
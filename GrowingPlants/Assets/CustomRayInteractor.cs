using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomRayInteractor : XRRayInteractor
{
	public override void ProcessInteractor(XRInteractionUpdateOrder.UpdatePhase updatePhase)
	{
		// 기본 클래스의 ProcessInteractor 메서드를 호출
		base.ProcessInteractor(updatePhase);

		// 회전 및 거리 조절 로직을 여기에서 제거하거나 수정
		// 예를 들어, 해당 기능을 담당하는 코드 부분을 주석 처리하거나 삭제
	}

	// 필요한 경우 다른 메서드들도 오버라이드하여 추가적인 수정 진행
}

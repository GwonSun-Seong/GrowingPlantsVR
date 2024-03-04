using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapTurnInteractorControl : MonoBehaviour
{
	public ActionBasedSnapTurnProvider snapTurnProvider;
	public XRRayInteractor leftRayInteractor;
	public XRRayInteractor rightRayInteractor;

	private void Update()
	{
		// 스냅턴이 진행 중인지 확인
		var isTurning = IsTurning();

		// 스냅턴이 진행 중이라면 Ray Interactor를 비활성화
		leftRayInteractor.enabled = !isTurning;
		rightRayInteractor.enabled = !isTurning;
	}

	private bool IsTurning()
	{
		// 스냅턴 액션의 입력 값을 읽어서 스냅턴이 진행 중인지 확인
		var leftHandValue = snapTurnProvider.leftHandSnapTurnAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
		var rightHandValue = snapTurnProvider.rightHandSnapTurnAction.action?.ReadValue<Vector2>() ?? Vector2.zero;

		// 두 손 중 하나라도 스냅턴 입력을 받고 있다면 true 반환
		return leftHandValue != Vector2.zero || rightHandValue != Vector2.zero;
	}
}

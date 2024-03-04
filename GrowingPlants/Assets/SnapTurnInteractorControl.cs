using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapTurnInteractorControl : MonoBehaviour
{
	public ActionBasedSnapTurnProvider snapTurnProvider;
	public XRRayInteractor leftRayInteractor;
	public XRRayInteractor rightRayInteractor;

	private void Update()
	{
		// �������� ���� ������ Ȯ��
		var isTurning = IsTurning();

		// �������� ���� ���̶�� Ray Interactor�� ��Ȱ��ȭ
		leftRayInteractor.enabled = !isTurning;
		rightRayInteractor.enabled = !isTurning;
	}

	private bool IsTurning()
	{
		// ������ �׼��� �Է� ���� �о �������� ���� ������ Ȯ��
		var leftHandValue = snapTurnProvider.leftHandSnapTurnAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
		var rightHandValue = snapTurnProvider.rightHandSnapTurnAction.action?.ReadValue<Vector2>() ?? Vector2.zero;

		// �� �� �� �ϳ��� ������ �Է��� �ް� �ִٸ� true ��ȯ
		return leftHandValue != Vector2.zero || rightHandValue != Vector2.zero;
	}
}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomRayInteractor : XRRayInteractor
{
	public override void ProcessInteractor(XRInteractionUpdateOrder.UpdatePhase updatePhase)
	{
		// �⺻ Ŭ������ ProcessInteractor �޼��带 ȣ��
		base.ProcessInteractor(updatePhase);

		// ȸ�� �� �Ÿ� ���� ������ ���⿡�� �����ϰų� ����
		// ���� ���, �ش� ����� ����ϴ� �ڵ� �κ��� �ּ� ó���ϰų� ����
	}

	// �ʿ��� ��� �ٸ� �޼���鵵 �������̵��Ͽ� �߰����� ���� ����
}

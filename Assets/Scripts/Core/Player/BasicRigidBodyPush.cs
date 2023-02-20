using UnityEngine;

namespace Core
{
	public class BasicRigidBodyPush : MonoBehaviour
	{
		[SerializeField] private LayerMask _pushLayers;
		[SerializeField] private bool _canPush;
		[SerializeField] [Range(0.5f, 5f)] private float _strength = 1.1f;

		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			if (_canPush)
				PushRigidBodies(hit);
		}

		private void PushRigidBodies(ControllerColliderHit hit)
		{
			Rigidbody body = hit.collider.attachedRigidbody;
			if (body == null || body.isKinematic)
				return;

			var bodyLayerMask = 1 << body.gameObject.layer;
			if ((bodyLayerMask & _pushLayers.value) == 0)
				return;

			if (hit.moveDirection.y < -0.3f)
				return;

			Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

			body.AddForce(pushDir * _strength, ForceMode.Impulse);
		}
	}
}
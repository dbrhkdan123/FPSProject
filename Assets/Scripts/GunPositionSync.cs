using UnityEngine;
using UnityEngine.Networking;

public class GunPositionSync : NetworkBehaviour {
	//카메라의 위치, 회전각도
	[SerializeField] Transform cameraTransform;
	// 참조할 손의 위치
	[SerializeField] Transform handMount;
	// 총을 배치할때 기준으로 할 참조 위치 (총의 부모 오브젝트)
	[SerializeField] Transform gunPivot;
	// 매 프레임마다 씽크하기엔 네트워크가 아까우니, 마지막으로 씽크된 각도 (lastSyncedPitch) 보다 각도가 10도 이상 변경된 경우에만 씽크하기. 즉 씽크의 최소한계치
	[SerializeField] float threshold = 10f;
	// 총을 카메라에 맞춰 이동시킬때 스무스하게 총을 이동시키기
	[SerializeField] float smoothing = 5f;
	// 플레이어들 끼리 씽크할 총의 각도
	[SyncVar] float pitch;
	/*
	   애니메이션 때문에 모델의 손의 위치가 자꾸 바뀐다.
	   모델의 손의 위치가 모델의 중심보다 얼만큼 떨어져있는지 저장한다.

       이전 프레임에서 모델의 손의 위치가 모델의 중심에서 얼만큼 떨어져있었는지 => lastOffse
	   현재 프레임에서 모델의 손의 위치가 얼만큼 떨어져있는지를 => currentOffset
	   
	   이렇게 해서 (현재 오프셋 - 이전 오프셋) 만큼 총기 위치를 옮겨 주면,
	   애니메이션에 의해 손의 위치가 계속 옮겨가도
	   거기에 맞춰서 총기의 위치도 계속 옮겨지게 된다 
	*/

	// 그러면
	Vector3 lastOffset;
	float lastSyncedPitch;
	Animator anim;

	void Start()
	{
		if (isLocalPlayer)
			gunPivot.parent = cameraTransform;
		else
			lastOffset = handMount.position - transform.position;
	}


}

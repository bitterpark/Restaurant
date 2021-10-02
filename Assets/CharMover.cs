using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMover : MonoBehaviour
{
    [SerializeField]
    float playerSpeed;
    [SerializeField]
    float gravity = 1f;
    CharacterController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

	private void Update() {
		var direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		DoMove(direction);
		ApplyPlayerGravity();
	}

	private void ApplyPlayerGravity() {
		controller.Move(-transform.up * gravity);
	}

	void DoMove(Vector3 direction) {
        var moveVect = transform.TransformDirection(direction) * playerSpeed * Time.deltaTime;
        controller.Move(moveVect);
    }
}

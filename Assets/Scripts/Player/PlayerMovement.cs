using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float gravity = 5;
    [SerializeField] float modelRotateSpeed = 10;
    
    CharacterController cc;
    
    private void Awake() {
        cc = GetComponent<CharacterController>();
    }
    
    private void Update() {
        //Handle PlayerMovement
        {
            //get desired movement vector.
            Vector3 dir = Vector3.zero;
            if (PlayerInput.Up)       dir += new Vector3(0, 0, 1);
            if (PlayerInput.Left)     dir += new Vector3(-1, 0, 0);
            if (PlayerInput.Down)     dir += new Vector3(0, 0, -1);
            if (PlayerInput.Right)    dir += new Vector3(1, 0, 0);
            dir = dir.normalized;

            //transform movement vector by camera rotation.y and apply movement.
            Vector3 rotation = new Vector3(0, Camera.main.transform.rotation.eulerAngles.y, 0);
            Matrix4x4 m = Matrix4x4.Rotate(Quaternion.Euler(rotation));
            dir = m.MultiplyVector(dir);
            cc.Move(dir * Time.deltaTime * speed);

            //this lerps player model rotation.
            if (dir != Vector3.zero) {
                Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, modelRotateSpeed * Time.deltaTime);
            }            

            //gravity
            cc.Move(Vector3.down * Time.deltaTime * gravity);
        }
    }
}

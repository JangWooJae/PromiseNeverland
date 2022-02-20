using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] Transform tf_Cam;
    [SerializeField] float MoveSpeed;   // WASD 이동속도
    [SerializeField] float sightSensitivity;    // 시야 회전 민감도
    [SerializeField] float sightLimits; // 최대 이동 가능 시야 (x 축을 기준으로 위, 아래로 돌릴때 180도를 넘어가면 축이 이상해져서 화면이 뒤집혀서 필요함)
    [SerializeField] float jump_force;  // 점프하는 힘: 4로 설정

    float currentAngleX;
    float currentAngleY;

    Rigidbody rigid;
    bool isjumping;

    void Start(){
        // 커서 고정 밑 숨기기
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        // 리지드바디 컴포넌트의 인스턴스를 얻는다
        rigid = GetComponent<Rigidbody>();
        isjumping = false;
    } //
    
    void Update()
    {
        Move();
        Sight();
        Jump();
    } //


    // 바닥에 착지할 때 점프 가능하게 바꿔주는 함수
    void OnCollisionEnter(Collision collision){
        // if (collision.gameObject.tag == "Ground"){
        isjumping = false;
        // }
    } // 끝


    // 점프
    void Jump(){
        // jump_force(점프하는 힘)=4 일 때, Rigidbody의 Mass(무게)와 Drag(저항) = 1 로 설정
        if (Input.GetKey(KeyCode.Space) && !isjumping){
            rigid.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
            isjumping = true;
        }
    } // 끝


    // 마우스 움직임으로 시야 및 방향 조정
    void Sight(){
        float yRotation = Input.GetAxisRaw("Mouse X");  // 마우스를 좌우로 움직임 = Y축을 중심으로 회전
        float Cam_y = yRotation * sightSensitivity; // 회전량 = 마우스 이동량 * 민감도
        currentAngleY += Cam_y; // 현재 시야 + 회전량

        float xRotation = Input.GetAxisRaw("Mouse Y");  // X축을 중심으로 회전
        float Cam_x = xRotation * sightSensitivity;
        currentAngleX -= Cam_x;

        currentAngleX = Mathf.Clamp(currentAngleX, -sightLimits, sightLimits);  // x축을 기준으로(위, 아래) 회전 하는 각도를 제한함

        tf_Cam.localEulerAngles = new Vector3(currentAngleX, 0, tf_Cam.localEulerAngles.z); // 카메라는 몸통 기준 y성분이 0방향이어야 정면이 고정되고 x,z 성분은 회전시키는 대로 돌아야함 
        transform.localEulerAngles = new Vector3(0, currentAngleY, 0);  // x, z성분도 값이 있으면 화면이 뒤집어 지거나 하늘이나 땅 방향으로 날아다님

    } // 끝


    //WASD 이동
    void Move(){
        Vector3 move = transform.forward * Input.GetAxis("Vertical") + 
                       transform.right * Input.GetAxis("Horizontal");

        transform.position += move * MoveSpeed * Time.deltaTime;        
    } // 끝


}

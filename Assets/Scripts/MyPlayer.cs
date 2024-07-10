using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyPlayer : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] Animator Animator;

    public GameObject ink;
    public GameObject bomb;
    private GameObject counter;
    private GameObject spawner;

    public float speed;
    public float inkspeed;

    public float width;

    public float jumpForce = 100.0f;
    public float bombForce = 0.2f;

    const string ANIM_BOOL_MOVE = "a_Run";
    const string ANIM_BOOL_BOMB_A = "a_Bomb";
    const string ANIM_BOOL_BOMB_B = "b_Bomb";
    const string ANIM_BOOL_SHOOT_A = "a_Shoot";
    const string ANIM_BOOL_SHOOT_B = "b_Shoot";
    const string ANIM_BOOL_EMOTE_A = "a_Emote";
    const string ANIM_BOOL_EMOTE_B = "b_Emote";
    const string ANIM_TRIG_SPECIAL = "t_Special";

    private bool ZRisPressed = false;
    private bool isGrounded = false;

    private int RollerCount = 0;
    public int RollerSpeed = 10;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = true;  //object落下防止

        counter = GameObject.Find("PointCounter");
        spawner = GameObject.Find("Spawner");
    }

    public void Mode(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        counter.GetComponent<CountPoint>().SwitchGameMode();
        spawner.GetComponent<SpawnDummy>().SwitchGameMode();
    }

    public void Manual(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        counter.GetComponent<CountPoint>().SwitchManual();
    }

    public void StartMenu(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
    }

    public void Move(InputAction.CallbackContext context)
    {
        rb.isKinematic = false;
        Animator.SetBool(ANIM_BOOL_BOMB_B, false);  //動いたらbomb中断
        Animator.SetBool(ANIM_BOOL_EMOTE_B, false); //動いたらemote中断
        // スティックの入力を受け取る
        var v = context.ReadValue<Vector2>();

        // 移動量を計算
        var mov = new Vector3(v.x * speed * Time.deltaTime, 0, v.y * speed * Time.deltaTime);

        switch (context.phase)
        {
            case InputActionPhase.Started:
                // 入力開始

                // アニメーション設定
                Animator.SetBool(ANIM_BOOL_MOVE, true);

                // 移動方向を向く
                transform.forward = mov;
                break;
            case InputActionPhase.Canceled:
                // 入力終了

                // アニメーション設定
                Animator.SetBool(ANIM_BOOL_MOVE, false);
                break;
            default:
                // 移動方向を向く
                transform.forward = mov;
                break;
        }

        // 移動させる
        transform.position = transform.position + mov;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (isGrounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
	}
    
    public void Reset(InputAction.CallbackContext context)
	{
        if (!context.performed) return;
        SceneManager.LoadScene("SampleScene");
	}

    public void Paint(InputAction.CallbackContext context)
	{
        switch (context.phase)
        {
            
            case InputActionPhase.Started:
                Animator.SetBool(ANIM_BOOL_SHOOT_A, true);
                ThrowingBall(6.0f, 0.2f, 0.0f);
                ThrowingBall(5.0f, 0.6f, 3.0f);
                ThrowingBall(5.0f, 0.1f, 0.0f);
                ThrowingBall(5.0f, -0.6f, -3.0f);
				ThrowingBall(4.0f, 0.5f, 4.0f);
				ThrowingBall(4.0f, -0.3f, -4.0f);
                ThrowingBall(3.0f, 1.1f, 5.0f);
                ThrowingBall(3.0f, -1.0f, -5.0f);
                ThrowingBall(1.0f, 0.0f, 0.0f);
                ThrowingBall(1.0f, -0.4f, -4.0f);
                break;
                
            case InputActionPhase.Performed:
                // ボタンが押された時の処理
                Animator.SetBool(ANIM_BOOL_SHOOT_B, true);
                ZRisPressed = true;
                break;

            case InputActionPhase.Canceled:
                // ボタンが離された時の処理
                Animator.SetBool(ANIM_BOOL_SHOOT_A, false);
                Animator.SetBool(ANIM_BOOL_SHOOT_B, false);
                ZRisPressed = false;
                break;
        }
    }

    public void Bomb(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                // ボタンが押された時の処理

                GameObject curling = Instantiate(bomb, this.transform.position + this.transform.up * width + this.transform.right * width, Quaternion.identity);
                curling.GetComponent<Rigidbody>().AddForce(this.transform.forward * bombForce, ForceMode.Impulse);

                Animator.SetBool(ANIM_BOOL_BOMB_A, true);
                Animator.SetBool(ANIM_BOOL_BOMB_B, true);
                break;

            case InputActionPhase.Canceled:
                // ボタンが離された時の処理
                Animator.SetBool(ANIM_BOOL_BOMB_A, false);
                break;
        }
    }

    public void Special(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Animator.SetTrigger(ANIM_TRIG_SPECIAL);
    }

    public void Emote(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                // ボタンが押された時の処理
                Animator.SetBool(ANIM_BOOL_EMOTE_A, true);
                Animator.SetBool(ANIM_BOOL_EMOTE_B, true);
                break;

            case InputActionPhase.Canceled:
                // ボタンが離された時の処理
                Animator.SetBool(ANIM_BOOL_EMOTE_A, false);
                break;
        }
    }

    private void ThrowingBall(float x, float y, float z)
	{
        GameObject ball = Instantiate(ink, this.transform.position + this.transform.up * width * x + this.transform.right * width * y, Quaternion.identity);    //Bombを右手付近に配置
        Rigidbody rid = ball.GetComponent<Rigidbody>();
        rid.AddForce((this.transform.forward + this.transform.up + this.transform.right * width * z) * rid.mass * 0.5f, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isGrounded = false;
        }
    }

    public void Update()
	{
        if (ZRisPressed)    //ZRが押されている間カウントアップし、一定以上でリセット、inkを放出する。inkの放出スピードを調節するため。
        {
            RollerCount++;
            if (RollerCount >= (int)(100/RollerSpeed))
            {
                Instantiate(ink, this.transform.position + this.transform.forward * width * 0.9f, Quaternion.identity);
                Instantiate(ink, this.transform.position + this.transform.forward * width * 0.9f + this.transform.right * width * 0.7f, Quaternion.identity);
                Instantiate(ink, this.transform.position + this.transform.forward * width * 0.9f - this.transform.right * width * 0.7f, Quaternion.identity);
                RollerCount = 0;
            }
        }

        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Bomb")) //Bombアニメーション中のみrollerを非表示
        {
            this.transform.GetChild(6).gameObject.SetActive(false);
        }
        else
        {
            this.transform.GetChild(6).gameObject.SetActive(true);
        }
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Emote")) //Emoteアニメーション中のみrollerを非表示
        {
            this.transform.GetChild(6).gameObject.SetActive(false);
        }
        else
        {
            this.transform.GetChild(6).gameObject.SetActive(true);
        }

        if (!isGrounded)
		{
            rb.AddForce(this.transform.up * rb.mass * 3.0f, ForceMode.Acceleration);   //Jump時の重力を和らげる
		}
    }

}

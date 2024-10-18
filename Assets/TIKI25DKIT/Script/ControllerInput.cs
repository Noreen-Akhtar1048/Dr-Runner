using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class ControllerInput : MonoBehaviour
{
    public static ControllerInput Instance;

    public delegate void InputEvent(Vector2 direction);
    public static event InputEvent inputEvent;

    public GameObject btnJetpack;
    public GameObject btnSlide;

    CanvasGroup canvasGroup;
    public DoubleTapTut doubleTapTut; 
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        //if (GameManager.Instance.Player.IgnoreControllerInput())
        //{
        //    canvasGroup.interactable = false;
        //    canvasGroup.blocksRaycasts = false;
        //}
        //else
        //{
        //    canvasGroup.interactable = true;
        //    canvasGroup.blocksRaycasts = true;
        //}

        btnJetpack.SetActive(GameManager.Instance.Player.isJetpackActived);
        btnSlide.SetActive(GameManager.Instance.Player.isRunning);
    }

    public void ShowController(bool show)
    {
        if (show)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
    }

    [ReadOnly] public bool allowJump = true;
    [ReadOnly] public bool allowSlide = true;

    public void Jump()
    {
        if (allowJump)
            GameManager.Instance.Player.Jump();
        Time.timeScale = 1;
    }

    public void JumpOff()
    {
        if (allowJump)
            GameManager.Instance.Player.JumpOff();
    }

    public void SlideOn()
    {
        if (allowSlide)
            GameManager.Instance.Player.SlideOn();
    }

    public void MoveLeft()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Playing)
        {
            GameManager.Instance.Player.MoveLeft();

            //isMovingLeft = true;
            if (inputEvent != null)
                inputEvent(Vector2.left);
        }
    }

    public void MoveLeftTap()
    {
        GameManager.Instance.Player.MoveLeftTap();
    }

    public void MoveRight()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Playing)
        {
            GameManager.Instance.Player.MoveRight();
            //isMovingRight = true;
            if (inputEvent != null)
                inputEvent(Vector2.right);
        }
        Time.timeScale = 1;
    }

    public void MoveRightTap()
    {
        GameManager.Instance.Player.MoveRightTap();
        if (doubleTapTut && PlayerPrefs.GetInt("DoubleTapTut")==1)
        {
            doubleTapTut.tuttorialpanel.SetActive(false);
            doubleTapTut.doubleTapArrow.SetActive(false);
            doubleTapTut.mainControl.SetActive(true);
            print("Yessssssssss");
        }
       
    }

    public void MoveDown()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Playing)
        {
            GameManager.Instance.Player.MoveDown();
            if (inputEvent != null)
                inputEvent(Vector2.down);
        }
        Time.timeScale = 1;
    }

    public void StopMove(int fromDirection)
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Playing)
        {
            GameManager.Instance.Player.StopMove(fromDirection);
            //isMovingLeft = false;
            //isMovingRight = false;
            if (inputEvent != null)
                inputEvent(Vector2.zero);
        }
        Time.timeScale = 1;
    }

    public void RangeAttack()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Playing)
        {
            GameManager.Instance.Player.RangeAttack();
        }

        Time.timeScale = 1;
    }

    public void MeleeAttack()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Playing) 
            GameManager.Instance.Player.MeleeAttack();
        Time.timeScale = 1;
    }
    //public void FireAttack()
    //{
    //    if (GameManager.Instance.gameState == GameManager.GameState.Playing) 
    //        GameManager.Instance.Player.FireAttack();
    //    Time.timeScale = 1;
    //}



    public void UseJetpack(bool use)
    {
        GameManager.Instance.Player.UseJetpack(use);
    }
}

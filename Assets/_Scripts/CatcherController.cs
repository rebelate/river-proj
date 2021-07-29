using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class CatcherController : MonoBehaviour
{
    PlayerInput playerInput;
    Camera cam;
    
    public float maxHoldTime;
    public float maxThrowPower;
    public Slider slider;

    bool poweringUp;
    bool incrementing;
    float timeElapsed;

    public Catcher catcher;
    float throwPower;
    bool isThrowing;
    Vector2 direction;

    void Start()
    {
        cam = Camera.main;
        catcher.HookReturned += ThrowEnded;
        catcher.HookTouchedGarbage += TouchedGarbage;
    }

    void Update()
    {
        UpdatePowerBar();
    }

    void ThrowEnded(){
        isThrowing = false;
        timeElapsed = 0;
        UpdatePowerSlider();
    }

    void TouchedGarbage(){
        timeElapsed = 0;
        UpdatePowerSlider();
    }

    public void Hook(InputAction.CallbackContext context)
    {
        if(isThrowing)
            return;

        TouchState state = context.ReadValue<TouchState>();

        if(state.phase != UnityEngine.InputSystem.TouchPhase.Began)
            return;

        StartCountingPower();
    }

    private void StartCountingPower()
    {
        timeElapsed = 0;
        poweringUp = true;
    }

    private void StopCountingPower()
    {
        poweringUp = false;
        incrementing = true;
    }

    private void GenerateHookDirection(Vector2 touchScreenPosition)
    {
        Vector2 touchWorldPosition = cam.ScreenToWorldPoint(touchScreenPosition);
        direction = (touchWorldPosition - (Vector2)transform.position).normalized;
    }

    void UpdatePowerBar(){
        if(!poweringUp)
            return;

        if(incrementing)
            timeElapsed += Time.deltaTime;

        if(!incrementing)
            timeElapsed -= Time.deltaTime;

        if(timeElapsed < 0)
            incrementing = true;

        if(timeElapsed > maxHoldTime)
            incrementing = false;

        UpdatePowerSlider();
    }

    void UpdatePowerSlider(){
        slider.value = (timeElapsed / maxHoldTime) * 100;
    }

    void CalculateThrow()
    {
        throwPower = (timeElapsed / maxHoldTime) * maxThrowPower;
    }

    public void HookRelease(InputAction.CallbackContext context){
        if(isThrowing){
            return;
        }

        Touchscreen touchscreen = Touchscreen.current;

        StopCountingPower();
        GenerateHookDirection(touchscreen.position.ReadValue());
        StartHook();
    }

    private void StartHook()
    {
        CalculateThrow();
        if(throwPower > 1){
            isThrowing = true;
            catcher.StartThrow(direction, throwPower);
        }
    }

    #region Enable/Disable

    void OnEnable(){
        if(playerInput == null){
            playerInput = new PlayerInput();
        }

        playerInput.Player.Enable();
        playerInput.Player.Hook.performed += Hook;
        playerInput.Player.HookRelease.performed += HookRelease;
    }

    void OnDisable(){
        playerInput.Player.Disable();
    }

    void OnDestroy(){
        catcher.HookReturned -= ThrowEnded;
        catcher.HookTouchedGarbage -= TouchedGarbage;
    }

    #endregion

}

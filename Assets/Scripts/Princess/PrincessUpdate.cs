using Spine;
using Spine.Unity;
using System;
using UnityEngine;

public class PrincessUpdate : MonoBehaviour {

    public static PrincessUpdate Instance;
    
    [Header("Current State")]
    public PrincessSpineState state;
    public enum PrincessSpineState { // This will call the different states for each animation
        Aim,
        Blink,
        CollisionAir,
        CollisionGround,
        Idle,
        Recoil

    }
    PrincessSpineState previousAnim, currentAnim;

    public KeyCode w = KeyCode.W;
    public KeyCode a = KeyCode.A;
    public KeyCode s = KeyCode.S;
    public KeyCode d = KeyCode.D;

    public Rigidbody2D rb;
    public AnimationReferenceAsset aim, blink, collision_air, collision_ground, idle, recoil;
    public Spine.AnimationState spineAnimationState;
    public SkeletonAnimation skeletonAnim;

    public bool isAim, isBlink, collisionAir, collisionGround, isIdle, isRecoil;

    private void Awake() {
            Instance = this;
    }

    private void OnEnable() {
        PrincessController.EnterAirCollision += OnAirCollision;
        PrincessController.EnterGroundCollision += OnGroundCollision;
        PrincessController.EnterIdleCollision += IdleState;
    }

    private void OnDisable() {
        PrincessController.EnterAirCollision -= OnAirCollision;
        PrincessController.EnterGroundCollision -= OnGroundCollision;
        PrincessController.EnterIdleCollision -= IdleState;
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        skeletonAnim = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnim.AnimationState;
    }

    

    void Update()
    {
        if (skeletonAnim == null) return;

        if (isIdle) {
            IdleState();
        }
        if (Input.GetKey(a)) {
            skeletonAnim.AnimationState.SetAnimation(0, recoil, false);
            skeletonAnim.Update(0);
        }
        if (Input.GetKey(s)) {
            skeletonAnim.AnimationState.SetAnimation(0, collision_ground, false);
            skeletonAnim.Update(0);
        }
        if (Input.GetKey(d)) {
            skeletonAnim.AnimationState.SetAnimation(0, blink, false);
            skeletonAnim.Update(0);
        }
    }

    public void IdleState() {
        spineAnimationState.SetAnimation(0, idle, false);
        skeletonAnim.Update(0);
        isIdle = false;
    }

    public void OnAirCollision() {
        skeletonAnim.AnimationState.SetAnimation(0, collision_air, false);
    }

    public void OnGroundCollision() {
        spineAnimationState.SetAnimation(0, collision_ground, false);
    }

    public void WeaponAim() {
        skeletonAnim.AnimationState.SetAnimation(0, aim, false);
    }

    public void WeaponRecoil() {
        skeletonAnim.AnimationState.SetAnimation(0, recoil, false);
    }
}
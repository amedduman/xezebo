using UnityEngine;

namespace Xezebo.Player
{
    
public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] Animator animator;
    
    // animation IDs
    int _animIDSpeed;
    int _animIDGrounded;
    int _animIDJump;
    int _animIDFreeFall;
    int _animIDMotionSpeed;
    int _animIDInputX;
    int _animIDInputY;

    void Start()
    {
        AssignAnimationIDs();
    }
    
    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        _animIDInputX = Animator.StringToHash("InputX");
        _animIDInputY = Animator.StringToHash("InputY");
    }

    public void Move(float animationBlend_, float inputMagnitude)
    {
        // animator.SetFloat(_animIDSpeed, animationBlend_);
        // animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
    }

    public void Strafe(float inputX, float inputY)
    {
        animator.SetFloat(_animIDInputX, inputX);
        animator.SetFloat(_animIDInputY, inputY);
    }

    public void Jump(bool value)
    {
        animator.SetBool(_animIDJump, value);
    }

    public void FreeFall(bool value)
    {
        animator.SetBool(_animIDFreeFall, value);
    }

    public void Ground(bool value)
    {
        animator.SetBool(_animIDGrounded, value);
    }
}

}
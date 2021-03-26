using UnityEngine;

public class controllAnimations : MonoBehaviour
{
    private Animator animator;
    public enum direction { up, down, right, left};
    private direction directionNow = direction.up;
    private bool move = false;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMovement(bool move, direction directionX)
    {
        if(move != this.move || directionNow != directionX)
        {
            this.move = move;
            directionNow = directionX;
            string animaId = "";
            if (move)
            {
                if(directionNow == direction.up)
                {
                    animaId = "MoveUp";
                }
                else if(directionNow == direction.down)
                {
                    animaId = "MoveDown";
                }
                else if(directionNow == direction.right)
                {
                    animaId = "MoveRight";
                }
                else if(directionNow == direction.left)
                {
                    animaId = "MoveLeft";
                }
            }
            else
            {
                if (directionNow == direction.up)
                {
                    animaId = "IdleUp";
                }
                else if (directionNow == direction.down)
                {
                    animaId = "IdleDown";
                }
                else if (directionNow == direction.right)
                {
                    animaId = "IdleRight";
                }
                else if (directionNow == direction.left)
                {
                    animaId = "IdleLeft";
                }
            }

            animator.Play(animaId);
        }
    }

    public direction GetDirection()
    {
        return directionNow;
    }
}

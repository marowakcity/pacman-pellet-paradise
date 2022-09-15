using UnityEngine;

public class Eyes : MonoBehaviour
{
    public Sprite up, right, left, down;
    public SpriteRenderer spriteRenderer{get; private set; }
    public Movement movement {get; private set; }

    private void Awake(){
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        if(this.movement.direction == Vector2.up)
        {
            this.spriteRenderer.sprite = this.up;
        }
        else if(this.movement.direction == Vector2.right)
        {
            this.spriteRenderer.sprite = this.right;
        }
        else if(this.movement.direction == Vector2.left)
        {
            this.spriteRenderer.sprite = this.left;
        }
        else if(this.movement.direction == Vector2.down)
        {
            this.spriteRenderer.sprite = this.down;
        }

    }
}

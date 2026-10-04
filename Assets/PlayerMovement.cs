using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    public float speed = 5f;

    void Update()
    {
        if (!isLocalPlayer)
            return;

        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed)
                input.x -= 1;

            if (Keyboard.current.dKey.isPressed)
                input.x += 1;

            if (Keyboard.current.sKey.isPressed)
                input.y -= 1;

            if (Keyboard.current.wKey.isPressed)
                input.y += 1;
        }

        Vector3 movement = new Vector3(input.x, 0f, input.y);

        transform.Translate(movement * speed * Time.deltaTime);
    }
}
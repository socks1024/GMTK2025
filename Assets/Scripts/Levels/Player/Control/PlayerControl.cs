using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private bool isMoving = false;
    private MMF_Player player;

    void Start()
    {
        player = GetComponent<MMF_Player>();
    }

    void Update()
    {
        int x = 0;
        if (Input.GetAxis("Horizontal") > 0) x = 1;
        if (Input.GetAxis("Horizontal") < 0) x = -1;

        int y = 0;
        if (Input.GetAxis("Vertical") > 0) y = 1;
        if (Input.GetAxis("Vertical") < 0) y = -1;

        Vector2 dir = new Vector2(x, y);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, dir.magnitude);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Obstacles"))
        {
            return;
        }

        if (!isMoving && dir.magnitude > 0) Move(dir, Step, Duration);
    }

    private void Move(Vector2 direction, float length, float duration)
    {
        if (isMoving) return;

        isMoving = true;

        player.PlayFeedbacks();

        transform.DOMove(transform.position + new Vector3(direction.x, direction.y, 0) * length, duration)
            .OnComplete(()=> isMoving = false);
    }

    #region Inspector Field

    [SerializeField]
    public float Step = 1.0F;

    [SerializeField]
    public float Duration = 0.3F;
    
    #endregion
}

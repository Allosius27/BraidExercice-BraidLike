using AllosiusDevCore.Controller2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTimeBody : MonoBehaviour
{
    #region Fields

    private bool _isRewinding;

    #endregion

    #region Properties

    public List<AnimPointInTime> pointsInTime { get; protected set; }

    public bool IsRewinding => _isRewinding;

    #endregion

    #region UnityInspector

    [SerializeField] private Animator _animator;

    #endregion

    #region Behaviour

    public virtual void Start()
    {
        pointsInTime = new List<AnimPointInTime>();

        GameCore.Instance.AddAnimTimeBody(this);
    }

    public virtual void FixedUpdate()
    {
        if (GameCore.Instance == null)
        {
            return;
        }

        if (_isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    public virtual void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            AnimPointInTime pointInTime = pointsInTime[0];
            transform.localPosition = pointInTime.localPosition;
            transform.localRotation = pointInTime.localRotation;
            transform.localScale = pointInTime.localScale;
            _animator.Play(pointInTime.clipAnim, 0, (1f / pointInTime.clipLength) * pointInTime.clipDesiredFrame);
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    public virtual void Record()
    {
        if (pointsInTime.Count > Mathf.Round(GameCore.Instance.MaxRecordTime * (1f / Time.fixedDeltaTime)))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        AnimatorClipInfo[] animationClip = _animator.GetCurrentAnimatorClipInfo(0);
        int currentFrame = (int)(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime * (animationClip[0].clip.length * animationClip[0].clip.frameRate));
        //Debug.Log(animationClip[0].clip.name + " CurrentFrame : " + currentFrame + " " + animationClip[0].clip.length);

        pointsInTime.Insert(0, new AnimPointInTime(transform.localPosition, transform.localRotation, transform.localScale, "Base Layer." + animationClip[0].clip.name + "Param", animationClip[0].clip.length, currentFrame));
    }

    public virtual void StartRewind()
    {
        _isRewinding = true;
    }

    public virtual void StopRewind()
    {
        PlayerAnimator playerAnimator = GetComponent<PlayerAnimator>();
        if(playerAnimator != null)
        {
            Debug.Log("Reset Player Anims Params");
            playerAnimator.Anim.SetTrigger("ResetAnim");
        }

        _isRewinding = false;
    }

    #endregion
}

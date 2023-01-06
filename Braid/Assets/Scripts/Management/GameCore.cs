using AllosiusDevUtilities;
using AllosiusDevUtilities.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameCore : Singleton<GameCore>
{
    #region Fields

    private bool _isRewinding;

    private List<TimeBody> _timeBodies = new List<TimeBody>();
    private List<AnimTimeBody> _animTimeBodies = new List<AnimTimeBody>();

    #endregion

    #region Properties

    public float MaxRecordTime => _maxRecordTime;

    #endregion

    #region UnityInspector

    [SerializeField] private AudioData mainMusic;

    [SerializeField] private float _maxRecordTime;

    [SerializeField] private Volume rewindTimePostProcess;

    #endregion

    #region Behaviour

    private void Start()
    {
        AudioController.Instance.PlayAudio(mainMusic);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _isRewinding = true;
            rewindTimePostProcess.gameObject.SetActive(true);

            for (int i = 0; i < _timeBodies.Count; i++)
            {
                _timeBodies[i].StartRewind();
            }

            for (int i = 0; i < _animTimeBodies.Count; i++)
            {
                _animTimeBodies[i].StartRewind();
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            _isRewinding = false;
            rewindTimePostProcess.gameObject.SetActive(false);

            for (int i = 0; i < _timeBodies.Count; i++)
            {
                _timeBodies[i].StopRewind();
            }

            for (int i = 0; i < _animTimeBodies.Count; i++)
            {
                _animTimeBodies[i].StopRewind();
            }

        }

        CheckRewindState();
    }

    public void CheckRewindState()
    {
        if (_isRewinding)
        {
            for (int i = 0; i < _timeBodies.Count; i++)
            {
                if (_timeBodies[i].IsRewinding)
                {
                    return;
                }
            }

            for (int i = 0; i < _animTimeBodies.Count; i++)
            {
                if (_animTimeBodies[i].IsRewinding)
                {
                    return;
                }
            }

            _isRewinding = false;
            rewindTimePostProcess.gameObject.SetActive(false);
        }
    }

    public void AddTimeBody(TimeBody newTimeBody)
    {
        if(_timeBodies.Contains(newTimeBody) == false)
        {
            _timeBodies.Add(newTimeBody);
        }
    }

    public void AddAnimTimeBody(AnimTimeBody newTimeBody)
    {
        if (_animTimeBodies.Contains(newTimeBody) == false)
        {
            _animTimeBodies.Add(newTimeBody);
        }
    }

    #endregion
}

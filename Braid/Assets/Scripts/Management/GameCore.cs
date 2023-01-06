using AllosiusDevUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameCore : Singleton<GameCore>
{
    #region Fields

    private bool _isRewinding;

    private List<TimeBody> _timeBodies = new List<TimeBody>();

    #endregion

    #region Properties

    public float MaxRecordTime => _maxRecordTime;

    #endregion

    #region UnityInspector

    [SerializeField] private float _maxRecordTime;

    [SerializeField] private Volume rewindTimePostProcess;

    #endregion

    #region Behaviour

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
            
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            _isRewinding = false;
            rewindTimePostProcess.gameObject.SetActive(false);

            for (int i = 0; i < _timeBodies.Count; i++)
            {
                _timeBodies[i].StopRewind();
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

    #endregion
}

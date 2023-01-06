using AllosiusDevUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : Singleton<GameCore>
{
    #region Fields

    private List<TimeBody> _timeBodies = new List<TimeBody>();

    #endregion

    #region Properties

    public float MaxRecordTime => _maxRecordTime;

    #endregion

    #region UnityInspector

    [SerializeField] private float _maxRecordTime;

    #endregion

    #region Behaviour

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (int i = 0; i < _timeBodies.Count; i++)
            {
                _timeBodies[i].StartRewind();
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            for (int i = 0; i < _timeBodies.Count; i++)
            {
                _timeBodies[i].StopRewind();
            }
                
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

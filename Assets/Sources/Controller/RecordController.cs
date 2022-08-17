using Sapper.Model;
using Sapper.View;
using UnityEngine;
using System;

namespace Sapper.Controller
{
    public class RecordController : MonoBehaviour
    {
        private const string RecordMinutes = nameof(RecordMinutes);
        private const string RecordSeconds = nameof(RecordSeconds);

        [SerializeField] private RecordView _recordView;

        private Record _record;

        public bool RecordExist { get; private set; }

        public event Action<bool, Model.Time> Changed; 

        public void Init()
        {
            int minutes = PlayerPrefs.GetInt(RecordMinutes, int.MinValue);

            if (minutes == int.MinValue)
            {
                RecordExist = false;
                _record = new();

                Changed?.Invoke(RecordExist, default);

                return;
            }

            int seconds = PlayerPrefs.GetInt(RecordSeconds);
            _record = new(new Model.Time(minutes, seconds));

            RecordExist = true;

            Changed?.Invoke(RecordExist, _record.Time);
        }

        public bool TrySave(Model.Time recordTime)
        {
            if (RecordExist == false)
            {
                _record.Update(recordTime);
                Save(recordTime);
                RecordExist = true;

                Changed?.Invoke(RecordExist, _record.Time);

                return true;
            }

            if (_record.TryUpdate(recordTime))
            {
                Save(recordTime);

                Changed?.Invoke(RecordExist, _record.Time);

                return true;
            }
            else
                return false;
        }

        private void Save(Model.Time recordTime)
        {
            PlayerPrefs.SetInt(RecordMinutes, recordTime.Minutes);
            PlayerPrefs.SetInt(RecordSeconds, recordTime.Seconds);
            PlayerPrefs.Save();
        }
    }
}
using Sapper.Controller;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace Sapper.View
{
    public class RecordView : MonoBehaviour
    {
        [SerializeField] private Text _record;

        private RecordController _recordController;

        public void Init(RecordController recordController)
        {
            _recordController = recordController;
            _recordController.Changed += OnChanged;
        }

        private void OnDisable()
        {
            _recordController.Changed -= OnChanged;
        }

        private void OnChanged(bool isExist, Model.Time time)
        {
            if (isExist == false)          
                _record.text = "Record: none";         
            else
                _record.text = "Record: " + time.GetFormatedTime();
        }
    }
}
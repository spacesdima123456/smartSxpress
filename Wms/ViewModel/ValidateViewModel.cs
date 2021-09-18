using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.Mvvm.Native;

namespace Wms.ViewModel
{
    public class ValidateViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        public bool HasErrors => Errors.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        protected readonly Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        public IEnumerable GetErrors(string propertyName)
        {
            return Errors.ContainsKey(propertyName) ? Errors[propertyName] : null;
        }

        protected void AddError(string propertyName, string error = "")
        {
            if (!Errors.ContainsKey(propertyName))
                Errors[propertyName] = new List<string>();

            if (!Errors[propertyName].Contains(error))
            {
                Errors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        protected void ClearError(string property)
        {
            if (Errors.ContainsKey(property))
            {
                Errors.Remove(property);
                OnErrorsChanged(property);
            }
        }

        protected void ClearErrors()
        {
            Errors.ForEach(f =>
            {
                ClearError(f.Key);
            });
        }

        protected void OnErrorsChanged(string property)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(property));
        }
    }
}

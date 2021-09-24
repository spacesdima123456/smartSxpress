using System;
using System.Linq;
using Wms.API.Models;
using System.Collections;
using System.ComponentModel;
using DevExpress.Mvvm.Native;
using System.Collections.Generic;

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

        protected virtual void HandleErrors(Error error)
        {
            error.Errors.ForEach(f =>
            {
                var errors = f.Value.ToString()?.Split(',').ToList();
                if (errors != null)
                {
                    var message = errors.Select(s => s.Split(':')[1].Replace("}", "").Replace("\"", ""))
                        .Aggregate((a, b) => a + "\r\n" + b);
                    AddError(ToUpperCaseFirstLetter(f.Key), message);
                }
            });
        }

        private static string ToUpperCaseFirstLetter(string str)
        {
            if (str.Length == 0)
                return "";
            if (str.Length == 1)
                return str.ToUpper();
            return char.ToUpper(str[0]) + str.Remove(0, 1);
        }

        protected void OnErrorsChanged(string property)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(property));
        }
    }
}

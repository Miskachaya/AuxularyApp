using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OperatorApplication.ViewModels.Base
{
    public abstract class ViewModel : INotifyPropertyChanged, IDisposable
    {
        protected readonly IServiceProvider _serviceProvider;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void Dispose()
        {
            Dispose(true);
        }
        private bool _Disposed; 
        protected virtual void Dispose(bool Disposing)
        {
            if(!Disposing || _Disposed)
            {
                return;
                _Disposed = true;
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}

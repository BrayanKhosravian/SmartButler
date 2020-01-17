using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartButler.Logic.Common
{
	public interface IDelegateCommand : ICommand
	{
		void RaiseCanExecuteChanged();
	}

	public class DelegateCommand : IDelegateCommand
	{
		public event EventHandler CanExecuteChanged;

		private readonly Predicate<object> _canExecute;
		private readonly Action<object> _execute;

		public DelegateCommand(Action<object> execute) : this(execute, null) { }

		public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			if (_canExecute == null)
			{
				return true;
			}

			return _canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			_execute(parameter);
		}

		public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

	}

}

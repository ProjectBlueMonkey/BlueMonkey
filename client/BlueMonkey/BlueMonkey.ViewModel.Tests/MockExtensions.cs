using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace BlueMonkey.ViewModel.Tests
{
    public static class MockExtensions
    {
        public static void NotifyPropertyChanged<T, TResult>(this Mock<T> mock, Expression<Func<T, TResult>> expression, TResult result) where T : class, INotifyPropertyChanged
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null) throw new ArgumentException("expression.Body is not MemberExpression");

            mock.Setup(expression).Returns(result);

            mock.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs(memberExpression.Member.Name));
        }
    }
}

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Moq;

namespace BlueMonkey.ViewModels.Tests
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

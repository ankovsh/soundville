using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Soundville.Infrastructure.Utils
{
    public static class NameOf<T>
    {
        public static string Property<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var propertyInfo = (expression.Body as MemberExpression).Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }

            return propertyInfo.Name;
        }
    }
}

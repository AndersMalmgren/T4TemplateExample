using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MyApp.Core.Extensions
{
    public static class ReflectionExtension
    {
        public static string GetMethodName<T>(this T obj, Expression<Action<T>> expression)
        {
            return GetMethodName(expression);
        }

        public static string GetMethodName<T>(Expression<Action<T>> expression)
        {
            return GetMethodInfo(expression).Name;
        }

        public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression)
        {
            var body = expression.Body as MethodCallExpression;
            return body.Method;
        }

        public static MemberInfo GetMethodInfo<T>(this T obj, Expression<Action<T>> expression)
        {
            return GetMethodInfo(expression);
        }

        public static IEnumerable<PropertyInfo> GetPublicProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            var lambdaExpression = (LambdaExpression)expression;
            return (!(lambdaExpression.Body is UnaryExpression) ? (MemberExpression)lambdaExpression.Body : (MemberExpression)((UnaryExpression)lambdaExpression.Body).Operand).Member;
        }

        public static IEnumerable<Type> ListDescendants<T>()
        {
            var lookup = typeof (T);

            return lookup
                .Assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && lookup.IsAssignableFrom(t));
        }

        public static Dictionary<Type, Type> ListVisitorHandlers(this Type handlerInterface, Func<Type[], Type> typeSelector)
        {
            return handlerInterface.Assembly.GetTypes()
                .Where(t => !t.IsAbstract)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
                    .Select(i => new { Type = t, Handles = typeSelector(i.GetGenericArguments()) }))
                .ToDictionary(t => t.Handles, t => t.Type);
        } 
    }
}

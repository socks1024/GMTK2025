using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Tools.Expressions
{
    public enum OperatorMode
    {
        Comparison,
        Logical,
    }

    public enum ComparisonOperators
    {
        Equals,
        NotEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
    }

    public enum LogicalOperators
    {
        And,
        Or,
    }

    public struct ExpressionEntity
    {
        /// <summary>
        /// 通过字符串引用方式查询的左操作数
        /// </summary>
        public string Key;

        /// <summary>
        /// 右操作数
        /// </summary>
        public string Value;

        /// <summary>
        /// 两个操作数之间的比较运算符
        /// </summary>
        public ComparisonOperators ComparisonOperator;

        /// <summary>
        /// 连接上一段表达式的逻辑运算符
        /// </summary>
        public LogicalOperators LogicalOperator;
    }

    public interface IExpressionContainer
    {
        ExpressionEntity GetEntity();
    }

    public static class ExpressionExtension<T> where T : class, new()
    {
        public static bool GetExpressionResult(List<ExpressionEntity> entities, T input)
        {
            Expression<Func<T, bool>> expression = ExpressionSplice(entities);
            return expression.Compile().Invoke(input);
        }

        public static Expression<Func<T, bool>> ExpressionSplice(List<ExpressionEntity> entities)
        {
            if (entities.Count < 1)
            {
                return ex => true;
            }

            var expressionFirst = CreateExpressionDelegate(entities[0]);

            foreach (var entity in entities.Skip(1))
            {
                var expression = CreateExpressionDelegate(entity);

                InvocationExpression invocation = Expression.Invoke(expressionFirst, expression.Parameters.Cast<Expression>());
                BinaryExpression binary;
                if (entity.LogicalOperator == LogicalOperators.And)
                {
                    binary = Expression.And(expression.Body, invocation);
                }
                else
                {
                    binary = Expression.Or(expression.Body, invocation);
                }
                expressionFirst = Expression.Lambda<Func<T, bool>>(binary, expression.Parameters);
            }
            return expressionFirst;
        }

        private static Expression<Func<T, bool>> CreateExpressionDelegate(ExpressionEntity entity)
        {
            ParameterExpression param = Expression.Parameter(typeof(T));
            Expression key = param;

            string entityKey = entity.Key.Trim();

            if (entityKey.Contains('.'))
            {
                var tableNameAndField = entityKey.Split('.');
                key = Expression.Property(key, tableNameAndField[0].ToString());
                key = Expression.Property(key, tableNameAndField[1].ToString());
            }
            else
            {
                key = Expression.Property(key, entityKey);
            }

            Expression value = Expression.Constant(ParseType(entity));
            Expression body = CreateComparisonExpression(key, value, entity.ComparisonOperator);

            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        private static object ParseType(ExpressionEntity entity)
        {
            try
            {
                PropertyInfo property;

                if (entity.Key.Contains('.'))
                {
                    var tableNameAndField = entity.Key.Split('.');

                    property = typeof(T).GetProperty(tableNameAndField[0], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    property = property.PropertyType.GetProperty(tableNameAndField[1], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                }
                else
                {
                    property = typeof(T).GetProperty(entity.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                }

                return Convert.ChangeType(entity.Value, property.PropertyType);
            }
            catch (Exception)
            {
                throw new ArgumentException("Failed to convert type : invalid name or type");
            }
        }

        private static Expression CreateComparisonExpression(Expression left, Expression value, ComparisonOperators operatorEnum)
        {
            return operatorEnum switch
            {
                ComparisonOperators.Equals => Expression.Equal(left, Expression.Convert(value, left.Type)),
                ComparisonOperators.NotEqual => Expression.NotEqual(left, Expression.Convert(value, left.Type)),
                ComparisonOperators.GreaterThan => Expression.GreaterThan(left, Expression.Convert(value, left.Type)),
                ComparisonOperators.GreaterThanOrEqual => Expression.GreaterThanOrEqual(left, Expression.Convert(value, left.Type)),
                ComparisonOperators.LessThan => Expression.LessThan(left, Expression.Convert(value, left.Type)),
                ComparisonOperators.LessThanOrEqual => Expression.LessThanOrEqual(left, Expression.Convert(value, left.Type)),
                _ => Expression.Equal(left, Expression.Convert(value, left.Type)),
            };
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DynamicDataLibrary.Attributes
{
    public enum BinaryOperator { Equal, LessThan, LessThanOrEqual, GraterThan, GraterThanOrEqual }
    public class BinaryExpressionAttribute : Attribute
    {
        public BinaryOperator Operator { set; get; }
        public Func<Expression, Expression, BinaryExpression> BinaryExpressionFunction
        {
            get
            {
                switch (Operator)
                {
                    case BinaryOperator.Equal:
                        return BinaryExpression.Equal;
                    case BinaryOperator.LessThan:
                        return BinaryExpression.LessThan;
                    case BinaryOperator.LessThanOrEqual:
                        return BinaryExpression.LessThanOrEqual;
                    case BinaryOperator.GraterThan:
                        return BinaryExpression.GreaterThan;
                    case BinaryOperator.GraterThanOrEqual:
                        return BinaryExpression.GreaterThanOrEqual;
                    default:
                        return BinaryExpression.GreaterThanOrEqual;
                }
            }
        }

        public BinaryExpressionAttribute(BinaryOperator Boperator)
        {
            this.Operator = Operator;
        }
    }
}
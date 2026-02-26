using PSharp.CodeAnalysis.Binding;
using PSharp.CodeAnalysis.Binding.Expressions;
using PSharp.CodeAnalysis.Binding.Kind;
using PSharp.CodeAnalysis.Binding.Statements;
using PSharp.CodeAnalysis.Compilations;
using PSharp.CodeAnalysis.Symbols;

namespace PSharp.CodeAnalysis
{
    internal class Evaluator
    {

        private readonly BoundBlockStatement _root;
        private readonly Dictionary<VariableSymbol, object> _variables;
        private readonly Compilation _compilation;

        private object _lastValue;

        public Evaluator(BoundBlockStatement root, Dictionary<VariableSymbol, object> variables, Compilation compilation)
        {
            _root = root;
            _variables = variables;
            _compilation = compilation;
        }

        public object Evaluate()
        {
            var labelToIndex = new Dictionary<BoundLabel, int>();

            for (var i = 0; i < _root.Statements.Length; i++)
            {
                if (_root.Statements[i] is BoundLabelStatement l)
                {
                    labelToIndex.Add(l.Label, i + 1);
                }
            }

            var index = 0;

            while (index < _root.Statements.Length)
            {
                var s = _root.Statements[index];

                switch (s.Kind)
                {
                    case BoundNodeKind.VariableDeclaration:
                        EvaluateVariableDeclaration((BoundVariableDeclarationStatement)s);
                        index++;
                        break;
                    case BoundNodeKind.ExpressionStatement:
                        EvaluateExpressionStatement((BoundExpressionStatement)s);
                        index++;
                        break;
                    case BoundNodeKind.GotoStatement:
                        var gs = (BoundGotoStatement)s;
                        index = labelToIndex[gs.Label];
                        break;
                    case BoundNodeKind.ConditionalGotoStatement:
                        var cgs = (BoundConditionalGotoStatement)s;
                        var condition = (bool)EvaluateExpression(cgs.Condition);
                        if (condition == cgs.JumpIfTrue)
                            index = labelToIndex[cgs.Label];
                        else
                            index++;
                        break;
                    case BoundNodeKind.LabelStatement:
                        index++;
                        break;
                    default:
                        throw new Exception($"Unexpected node {s.Kind}");
                }
            }
            return _lastValue;
        }

        private void EvaluateVariableDeclaration(BoundVariableDeclarationStatement node)
        {
            var value = EvaluateExpression(node.Initializer);
            _variables[node.Variable] = value;
            _lastValue = value;
        }

        private void EvaluateExpressionStatement(BoundExpressionStatement node)
        {
            _lastValue = EvaluateExpression(node.Expression);
        }

        private object EvaluateExpression(BoundExpression node)
        {

            switch (node.Kind)
            {
                case BoundNodeKind.LiteralExpression:
                    return EvaluateLiteralExpression((BoundLiteralExpression)node);
                case BoundNodeKind.VariableExpression:
                    return EvaluateVariableExpression((BoundVariableExpression)node);
                case BoundNodeKind.AssignmentExpression:
                    return EvaluateAssignmentExpression((BoundAssignmentExpression)node);
                case BoundNodeKind.ConversionExpression:
                    return EvaluateConversionExpression((BoundConversionExpression)node);
                case BoundNodeKind.UnaryExpression:
                    return EvaluateUnaryExpression((BoundUnaryExpression)node);
                case BoundNodeKind.BinaryExpression:
                    return EvaluateBinaryExpression((BoundBinaryExpression)node);
                default:
                    throw new Exception($"Unexpected node {node.Kind}");
            }

        }

        private object EvaluateLiteralExpression(BoundLiteralExpression n)
        {
            return n.Value;
        }

        private object EvaluateVariableExpression(BoundVariableExpression v)
        {
            return _variables[v.Variable];
        }

        private object EvaluateConversionExpression(BoundConversionExpression node)
        {
            var value = EvaluateExpression(node.Expression);
            return Convert.ChangeType(value, node.Type.ClrType);
        }

        private object EvaluateAssignmentExpression(BoundAssignmentExpression a)
        {
            var value = EvaluateExpression(a.Expression);
            _variables[a.Variable] = value;
            return value;
        }

        private object EvaluateUnaryExpression(BoundUnaryExpression u)
        {
            var operand = EvaluateExpression(u.Operand);
            dynamic converted = Convert.ChangeType(operand, u.Op.Signature.OperandType.ClrType);

            return u.Op.Kind switch
            {
                UnaryOperatorKind.Identity => converted,
                UnaryOperatorKind.Negation => -converted,
                UnaryOperatorKind.LogicalNegation => !converted,
                UnaryOperatorKind.OnesComplement => ~converted,
                _ => throw new Exception($"Unexpected unary operator {u.Op.Kind}")
            };
        }

        private object EvaluateBinaryExpression(BoundBinaryExpression b)
        {
            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);
            var type = b.Left.Type; // or b.Type depending on your design

            var opKind = b.Op.Kind & (BinaryOperatorKind)0xFF;
            switch (opKind)
            {
                // Arithmetic operators - all numeric types
                case BinaryOperatorKind.Add:
                    if (type == Compilation.typeOf(SpecialType.System_Byte)) return (byte)left + (byte)right;
                    if (type == Compilation.typeOf(SpecialType.System_SByte)) return (sbyte)left + (sbyte)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int16)) return (short)left + (short)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt16)) return (ushort)left + (ushort)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int32)) return (int)left + (int)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt32)) return (uint)left + (uint)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int64)) return (long)left + (long)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt64)) return (ulong)left + (ulong)right;
                    if (type == Compilation.typeOf(SpecialType.System_Single)) return (float)left + (float)right;
                    if (type == Compilation.typeOf(SpecialType.System_Double)) return (double)left + (double)right;
                    if (type == Compilation.typeOf(SpecialType.System_Decimal)) return (decimal)left + (decimal)right;
                    throw new Exception($"Cannot add type {type}");

                case BinaryOperatorKind.Subtract:
                    if (type == Compilation.typeOf(SpecialType.System_Byte)) return (byte)left - (byte)right;
                    if (type == Compilation.typeOf(SpecialType.System_SByte)) return (sbyte)left - (sbyte)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int16)) return (short)left - (short)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt16)) return (ushort)left - (ushort)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int32)) return (int)left - (int)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt32)) return (uint)left - (uint)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int64)) return (long)left - (long)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt64)) return (ulong)left - (ulong)right;
                    if (type == Compilation.typeOf(SpecialType.System_Single)) return (float)left - (float)right;
                    if (type == Compilation.typeOf(SpecialType.System_Double)) return (double)left - (double)right;
                    if (type == Compilation.typeOf(SpecialType.System_Decimal)) return (decimal)left - (decimal)right;
                    throw new Exception($"Cannot subtract type {type}");

                case BinaryOperatorKind.Multiply:
                    if (type == Compilation.typeOf(SpecialType.System_Byte)) return (byte)left * (byte)right;
                    if (type == Compilation.typeOf(SpecialType.System_SByte)) return (sbyte)left * (sbyte)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int16)) return (short)left * (short)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt16)) return (ushort)left * (ushort)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int32)) return (int)left * (int)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt32)) return (uint)left * (uint)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int64)) return (long)left * (long)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt64)) return (ulong)left * (ulong)right;
                    if (type == Compilation.typeOf(SpecialType.System_Single)) return (float)left * (float)right;
                    if (type == Compilation.typeOf(SpecialType.System_Double)) return (double)left * (double)right;
                    if (type == Compilation.typeOf(SpecialType.System_Decimal)) return (decimal)left * (decimal)right;
                    throw new Exception($"Cannot multiply type {type}");

                case BinaryOperatorKind.Divide:
                    if (type == Compilation.typeOf(SpecialType.System_Byte)) return (byte)left / (byte)right;
                    if (type == Compilation.typeOf(SpecialType.System_SByte)) return (sbyte)left / (sbyte)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int16)) return (short)left / (short)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt16)) return (ushort)left / (ushort)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int32)) return (int)left / (int)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt32)) return (uint)left / (uint)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int64)) return (long)left / (long)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt64)) return (ulong)left / (ulong)right;
                    if (type == Compilation.typeOf(SpecialType.System_Single)) return (float)left / (float)right;
                    if (type == Compilation.typeOf(SpecialType.System_Double)) return (double)left / (double)right;
                    if (type == Compilation.typeOf(SpecialType.System_Decimal)) return (decimal)left / (decimal)right;
                    throw new Exception($"Cannot divide type {type}");

                // Bitwise operators - integer types only
                case BinaryOperatorKind.And:
                    if (type == Compilation.typeOf(SpecialType.System_Boolean)) return (bool)left & (bool)right;
                    if (type == Compilation.typeOf(SpecialType.System_Byte)) return (byte)left & (byte)right;
                    if (type == Compilation.typeOf(SpecialType.System_SByte)) return (sbyte)left & (sbyte)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int16)) return (short)left & (short)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt16)) return (ushort)left & (ushort)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int32)) return (int)left & (int)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt32)) return (uint)left & (uint)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int64)) return (long)left & (long)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt64)) return (ulong)left & (ulong)right;
                    throw new Exception($"Cannot bitwise AND type {type}");

                case BinaryOperatorKind.Or:
                    if (type == Compilation.typeOf(SpecialType.System_Boolean)) return (bool)left | (bool)right;
                    if (type == Compilation.typeOf(SpecialType.System_Byte)) return (byte)left | (byte)right;
                    if (type == Compilation.typeOf(SpecialType.System_SByte)) return (sbyte)left | (sbyte)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int16)) return (short)left | (short)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt16)) return (ushort)left | (ushort)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int32)) return (int)left | (int)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt32)) return (uint)left | (uint)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int64)) return (long)left | (long)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt64)) return (ulong)left | (ulong)right;
                    throw new Exception($"Cannot bitwise OR type {type}");

                case BinaryOperatorKind.ExclusiveOr:
                    if (type == Compilation.typeOf(SpecialType.System_Boolean)) return (bool)left ^ (bool)right;
                    if (type == Compilation.typeOf(SpecialType.System_Byte)) return (byte)left ^ (byte)right;
                    if (type == Compilation.typeOf(SpecialType.System_SByte)) return (sbyte)left ^ (sbyte)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int16)) return (short)left ^ (short)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt16)) return (ushort)left ^ (ushort)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int32)) return (int)left ^ (int)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt32)) return (uint)left ^ (uint)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int64)) return (long)left ^ (long)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt64)) return (ulong)left ^ (ulong)right;
                    throw new Exception($"Cannot bitwise XOR type {type}");

                // Logical operators - bool only
                case BinaryOperatorKind.ConditionalAnd:
                    if (type != Compilation.typeOf(SpecialType.System_Boolean))
                        throw new Exception($"Cannot logical AND type {type}");
                    return (bool)left && (bool)right;

                case BinaryOperatorKind.ConditionalOr:
                    if (type != Compilation.typeOf(SpecialType.System_Boolean))
                        throw new Exception($"Cannot logical OR type {type}");
                    return (bool)left || (bool)right;

                // Comparison operators - all numeric types returning bool
                case BinaryOperatorKind.Equal:
                    return Equals(left, right);

                case BinaryOperatorKind.NotEqual:
                    return !Equals(left, right);

                case BinaryOperatorKind.LessThan:
                    if (type == Compilation.typeOf(SpecialType.System_Byte)) return (byte)left < (byte)right;
                    if (type == Compilation.typeOf(SpecialType.System_SByte)) return (sbyte)left < (sbyte)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int16)) return (short)left < (short)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt16)) return (ushort)left < (ushort)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int32)) return (int)left < (int)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt32)) return (uint)left < (uint)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int64)) return (long)left < (long)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt64)) return (ulong)left < (ulong)right;
                    if (type == Compilation.typeOf(SpecialType.System_Single)) return (float)left < (float)right;
                    if (type == Compilation.typeOf(SpecialType.System_Double)) return (double)left < (double)right;
                    if (type == Compilation.typeOf(SpecialType.System_Decimal)) return (decimal)left < (decimal)right;
                    throw new Exception($"Cannot compare type {type} with <");

                case BinaryOperatorKind.LessThanOrEqual:
                    if (type == Compilation.typeOf(SpecialType.System_Byte)) return (byte)left <= (byte)right;
                    if (type == Compilation.typeOf(SpecialType.System_SByte)) return (sbyte)left <= (sbyte)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int16)) return (short)left <= (short)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt16)) return (ushort)left <= (ushort)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int32)) return (int)left <= (int)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt32)) return (uint)left <= (uint)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int64)) return (long)left <= (long)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt64)) return (ulong)left <= (ulong)right;
                    if (type == Compilation.typeOf(SpecialType.System_Single)) return (float)left <= (float)right;
                    if (type == Compilation.typeOf(SpecialType.System_Double)) return (double)left <= (double)right;
                    if (type == Compilation.typeOf(SpecialType.System_Decimal)) return (decimal)left <= (decimal)right;
                    throw new Exception($"Cannot compare type {type} with <=");

                case BinaryOperatorKind.GreaterThan:
                    if (type == Compilation.typeOf(SpecialType.System_Byte)) return (byte)left > (byte)right;
                    if (type == Compilation.typeOf(SpecialType.System_SByte)) return (sbyte)left > (sbyte)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int16)) return (short)left > (short)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt16)) return (ushort)left > (ushort)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int32)) return (int)left > (int)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt32)) return (uint)left > (uint)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int64)) return (long)left > (long)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt64)) return (ulong)left > (ulong)right;
                    if (type == Compilation.typeOf(SpecialType.System_Single)) return (float)left > (float)right;
                    if (type == Compilation.typeOf(SpecialType.System_Double)) return (double)left > (double)right;
                    if (type == Compilation.typeOf(SpecialType.System_Decimal)) return (decimal)left > (decimal)right;
                    throw new Exception($"Cannot compare type {type} with >");

                case BinaryOperatorKind.GreaterThanOrEqual:
                    if (type == Compilation.typeOf(SpecialType.System_Byte)) return (byte)left >= (byte)right;
                    if (type == Compilation.typeOf(SpecialType.System_SByte)) return (sbyte)left >= (sbyte)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int16)) return (short)left >= (short)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt16)) return (ushort)left >= (ushort)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int32)) return (int)left >= (int)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt32)) return (uint)left >= (uint)right;
                    if (type == Compilation.typeOf(SpecialType.System_Int64)) return (long)left >= (long)right;
                    if (type == Compilation.typeOf(SpecialType.System_UInt64)) return (ulong)left >= (ulong)right;
                    if (type == Compilation.typeOf(SpecialType.System_Single)) return (float)left >= (float)right;
                    if (type == Compilation.typeOf(SpecialType.System_Double)) return (double)left >= (double)right;
                    if (type == Compilation.typeOf(SpecialType.System_Decimal)) return (decimal)left >= (decimal)right;
                    throw new Exception($"Cannot compare type {type} with >=");

                default:
                    throw new Exception($"Unexpected binary operator {b.Op}");
            }
        }
    }
}
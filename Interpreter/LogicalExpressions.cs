namespace Lr1.Interpreter
{
    public class AndExpression : IExpression
    {
        private readonly IExpression _left;
        private readonly IExpression _right;

        public AndExpression(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public bool Interpret(Station context)
        {
            return _left.Interpret(context) && _right.Interpret(context);
        }
    }

    public class OrExpression : IExpression
    {
        private readonly IExpression _left;
        private readonly IExpression _right;

        public OrExpression(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public bool Interpret(Station context)
        {
            return _left.Interpret(context) || _right.Interpret(context);
        }
    }
}
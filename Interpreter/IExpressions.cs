namespace Lr1.Interpreter
{
    public interface IExpression
    {
        bool Interpret(Station context);
    }
}
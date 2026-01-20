using System;

namespace Lr1.Exceptions
{
    internal class MyExeption
    {
        private int a;
        private int b;

        public MyExeption()
        {
            a = 1;
            b = 0;
        }

        public void createExeption()
        {
            try
            {
                int c = a / b;
            }
            catch (DivideByZeroException)
            {
                throw new MyDivideByZeroException();
            }
        }
    }

    public class MyDivideByZeroException : DivideByZeroException
    {
        public MyDivideByZeroException() : base("Вызванно исключение (деление на ноль)") { }
    }
}
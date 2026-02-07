using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lr1
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

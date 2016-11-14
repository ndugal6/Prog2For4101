// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
    public class If : Special
    {
	public If() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printIf(t, n, p);
        }
       public override Node eval(Node a, Environment e) 
        {
           Node condition = a.getCdr().getCar();
           Node expression;
           if (condition.eval(e).getBoolean()) {
               expression = a.getCdr().getCdr().getCar();
               return expression.eval(e);
           } else if (!(a.getCdr().getCdr().getCdr()).isNull()) {
               expression = a.getCdr().getCdr().getCdr().getCar();
               return expression.eval(e);
           } else {
               Console.Error.WriteLine("There's not an else expression");
               return Nil.getInstance();
           }
        }
    }
}


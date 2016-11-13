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
        // public virtual Node eval(Node a, Environment e) {

        // }
        // public override Node eval(Node a, Environment e, bool p) 
        // {
        //     if (p) 
        //         eval(a.getCdr().getCar(), e);
        //     else 
        //         eval(a.getCdr().getCdr(), e);
        // }
    }
}


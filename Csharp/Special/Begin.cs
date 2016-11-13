// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
	public Begin() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printBegin(t, n, p);
        }

        public override Node eval(Node a, Environment e) 
        {
            if (a.getCdr() != null) {
                return this.eval(a.getCdr(), e);
            } else {
                return a.eval(a, e);
            }
        }
    }
}


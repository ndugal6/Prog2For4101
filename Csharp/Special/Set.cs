// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree
{
    public class Set : Special
    {
	public Set() { }
	
        public override void print(Node t, int n, bool p)
        {
            Printer.printSet(t, n, p);
        }

        public override Node eval(Node a, Environment e) 
        {
            Node key = a.getCdr().getCar();
            Node expression = t.getCdr().getCdr().getCar();
            e.define(key, expression.eval(e));
            return new StrLit("");
        }
    }
}


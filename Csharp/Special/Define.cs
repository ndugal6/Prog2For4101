// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
	public Define() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printDefine(t, n, p);
        }

        public override Node eval(Node a, Environment e) 
        {
           Node key = a.getCdr().getCar();
           Node val = a.getCdr().getCdr().getCar();

           if (key.isSymbol()) {
               e.define(key, val);
           } else {
               Closure function = new Closure(new Cons(a.getCdr().getCar().getCdr(), a.getCdr().getCdr()),e);
               e.define(key.getCar(), function);
           }
           return new StringLit("; no values returned");

        }
    }
}



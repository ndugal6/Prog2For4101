// Let -- Parse tree node strategy for printing the special form let

using System;

namespace Tree
{
    public class Let : Special
    {
	public Let() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printLet(t, n, p);
        }

        public override Node eval(Node a, Environment e) 
        {
            Node argument = a.getCdr().getCar();
            Node expression = a.getCdr().getCdr().getCar();
            Environment currentScope = new Environment(e);
            argument = evalFrame(argument,currentScope);
            return expression.eval(currentScope);
        }

        public Node evalFrame(Node a, Environment e) {
            if (a == null || a.isNull()) {
                return new Cons(Nil.getInstance(), Nil.getInstance());
            } else {
                Node argument = a.getCar().getCar();
                Node expression = a.getCar().getCdr().getCar();
                Node errThangElse = a.getCdr();
                if (argument.isSymbol()) {
                    environment.define(argument, expression.eval(e));
                    return evalFrame(errThangElse, e);
                } else if (argument.isPair()) {
                    return argument.eval(e);
                } else if (argument == null || argument.isNull) {
                    return Nil.getInstance();
                }
            }
            return null;
        }
        
    }
}



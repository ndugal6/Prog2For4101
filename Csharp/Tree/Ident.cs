// Ident -- Parse tree node class for representing identifiers

using System;

namespace Tree
{
    public class Ident : Node
    {
        private string name;

        public Ident(string n)
        {
            name = n;
        }

        public Node eval(Node a, Environment e)
        {
            Node node = new Cons(new Ident(a.getName()), Nil.getInstance());
            Node argument = evalAll(node, e);
            if(!argument.getCar().isPair())
            {
                if(Enviornment.errorMessages.size() == 0)
                {
                    Console.Error.WriteLine("Error: should not be a pair.");
                }
                else if(argument.getCar().isNumber())
                {
                    return new IntLit(argument.getCar().getVal());
                }
                else if(argument.getCar().isString())
                {
                    return new StringLit(argument.getCar().getStringVal());
                }
                else if(argument.getCar().isBool())
                {
                    return new boolLit(argument.getCar().getBool());
                }
                else
                {
                    Console.Write("just chilling here for the time being.");
                    return Nil.getInstance();
                }
            }
            else
            {
                reutn null;
            }
            return new stringLit("");
        }

        public Node evalAll(Node a, Environment e)
        {
            if(a == null || a.isNull())
            {
                return new Cons(Nil.getInstance(), Nil.getInstance());
            }
            else
            {
                Node car = a.getCar();
                Node cdr = a.getCdr();
                if(car.isSymbol())
                {
                    car = e.lookup(car);
                }
                if(car == null || car.isNull())
                {
                    return Nil.getInstance();
                }
                return new Cons(car.eval(e), evalAll(cdr, e));
            }
        }

        public override void print(int n)
        {
            Printer.printIdent(n, name);
        }

        public override String getName()
        {
            return name;
        }

        public override bool isSymbol()
        {
            return true;
        }

        public override Node eval(Environment e) {
            return e.lookup(a).getCar();
        }
    }
}
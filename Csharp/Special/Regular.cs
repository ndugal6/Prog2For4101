// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {
        public Regular() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printRegular(t, n, p);
        }

        public override Node eval(Node a, Environment e) 
        {
            Node car = a.getCar();
            Node argument = evalAll(a.getCdr(), e);

            while (car.isSybol()) {
                car = e.lookup(car);
            }

            if (car == null || car.isNul()) {
                returnn null
            }
            if (car.isProcedure()) {
                return car.apply(argument);
            } else {
                return car.eval(e).apply(argument);
            }
        }

        public Node evalAll(Node a, Environment e) {
            if (a == null || a.isNull()) {
                return new Cons(Nil.getInstance(), Nil.getInstance());
            } else {
                Node car = a.getCar();
                Node cdr = a.getCdr();
                if (car.isSymbol()) {
                    car = e.lookup(car);
                }
                if (car == null || car.isNull()) {
                    return null;
                }
                return new Cons(car.eval(e), evalAll(cdr,e));
            }
        }
    }
}



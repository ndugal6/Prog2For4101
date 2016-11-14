// BuiltIn -- the data structure for built-in functions

// Class BuiltIn is used for representing the value of built-in functions
// such as +.  Populate the initial environment with
// (name, new BuiltIn(name)) pairs.

// The object-oriented style for implementing built-in functions would be
// to include the C# methods for implementing a Scheme built-in in the
// BuiltIn object.  This could be done by writing one subclass of class
// BuiltIn for each built-in function and implementing the method apply
// appropriately.  This requires a large number of classes, though.
// Another alternative is to program BuiltIn.apply() in a functional
// style by writing a large if-then-else chain that tests the name of
// the function symbol.

using System;

namespace Tree
{
    public class BuiltIn : Node
    {
        private Node symbol;            // the Ident for the built-in function

        public BuiltIn(Node s)		{ symbol = s; }

        public Node getSymbol()		{ return symbol; }

        // TODO: The method isProcedure() should be defined in
        // class Node to return false.
        public override bool isProcedure()	{ return true; }

        public override void print(int n)
        {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.Write("#{Built-in Procedure ");
            if (symbol != null)
                symbol.print(-Math.Abs(n));
            Console.Write('}');
            if (n >= 0)
                Console.WriteLine();
        }

        // TODO: The method apply() should be defined in class Node
        // to report an error.  It should be overridden only in classes
        // BuiltIn and Closure.
        public override Node apply (Node args)
        {
            if (args == null) {
                return null;
            }

            String name = symbol.getName();
            Node car = args.getCar();
            if (car == null || car.isNull()) {
                car = Nil.getInstance();
            }
            Node cdr = args.getCdr();
            if (cdr == null || cdr.isNull()) {
                cdr = Nil.getInstance();
            } else {
                cdr = cdr.getCar();
            }

            if (name.equals("b+")) {
                if(car.isNumber() && cdr.isNumber()) {
                    return new IntLit(car.getVal() + cdr.getVal());
                } else {
                    Console.Error.WriteLine("Invalid arguments for b+");
                    return new StringLit("");
                }
            } else if (name.equals("b-")) {
                if (car.isNumber() && cdr.isNumber()) {
                    return new IntLit(car.getVal() - cdr.getVal());
                } else {
                    Console.Error.WriteLine("Invalid arguments for b-");
                    return new StringLit("");
                }
            } else if (name.equals("b*")) {
                if (car.isNumber() && cdr.isNumber()) {
                    return new IntLit(car.getVal() * cdr.getVal());
                } else {
                    Console.Error.WriteLine("Invalid arguments for b*");
                    return new StringLit("");
                }
            } else if (name.equals("b/")) {
                if (car.isNumber() && cdr.isNumber()) {
                    return new IntLit(car.getVal() / cdr.getVal());
                } else {
                    Console.Error.WriteLine("Invalid arguments for b/");
                    return new StringLit("");
                } 
            } else if (name.equals("b=")) {
                if (car.isNumber() && cdr.isNumber()) {
                    return new BoolLit(car.getVal() == cdr.getVal());
                } else {
                    Console.Error.WriteLine("Invalid arguments for b=");
                }
            } else if (name.equals("b>")) {
                if (car.isNumber() && cdr.isNumber()) {
                    return new BoolLit(car.getVal() > cdr.getVal());
                } else {
                    Console.Error.WriteLine("Invalid arguments for b>");
                }
            } else if (name.equals("b<")) {
                if (car.isNumber() && cdr.isNumber()) {
                    return new BoolLit(car.getVal() < cdr.getVal());
                } else {
                    Console.Error.WriteLine("Invalid arguments for b<");
                }
            } else if (name.equals("car")) {
                if (car.isNull()) {
                    return car;
                }
                return car.getCar();
            } else if (name.equals("cdr")) {
                if (car.isNull()) {
                    return cdr;
                }
                return cdr.getCdr();
            } else if (name.equals("cons")) {
                return new Cons(car, cdr);
            } else if (name.equals("set-cdr!")) {
                car.setCdr(cdr);
                return car;
            } else if (name.equals("set-car!")) {
                car.setCar(cdr);
                return car;
            } else if (name.equals("symbol?")) {
                return new BoolLit(car.isSymbol());
            } else if (name.equals("number?")) {
                return new BoolLit(car.isNumber());
            } else if (name.equals("null?")) {
                return new BoolLit(car.isNull());
            } else if (name.equals("pair?")) {
                return new BoolLit(car.isPair());
            } else if (name.equals("eq?")) {
                if (car.isBoolean() && cdr.isBoolean()) {
                    return new BoolLit(car.getBoolean() == cdr.getBoolean());
                } else if (car.isNumber && cdr.isNumber()) {
                    return new BoolLit(car.getVal() == cdr.getVal());
                } else if (car.isString() && cdr.isString()) {
                    return new BoolLit(car.getStringVal().equals(cdr.getStringVal()));
                } else if (car.isSymbol() && cdr.isSymbol) {
                    return new BoolLit(car.getName().equals(cdr.getName()));
                } else if (car.isNull() && cdr.isNull()) {
                    return new BoolLit(true);
                } else if (car.isPair() && cdr.isPair()) {
                    Node opener = new Cons(car.getCar(), new Cons(cdr.getCar(), Nil.getInstance()));
                    Node closer = new Cons(car.getCdr(), new Cons(cdr.getCdr(), Nil.getInstance()));
                    return new BoolLit(apply(opener).getBoolean() && apply(closer.getBoolean()));
                }
                return new BoolLit(false);
             } else if (name.equals("procedure?")) {
                 return new BoolLit(car.isProcedure());
             } else if (name.equals("display")) {
                 return car;
             } else if (name.equals("newline")) {
                 return new StringLit("", false);
             } else if (name.equals("exit") || name.equals("quit")) {
                 return 0;
             } else if (name.equals("write")) {
                 car.print(0);
                 return new StringLit("");
             } else if (name.equals("eval")) {
                 return car;
             } else if (name.equals("apply")) {
                 return car.apply(cdr)
             } else if (name.equals("read")) {
                 Parser isParsingMeOff = new Parser(new Scanner(Console.in));
                 return isParsingMeOff.parseExp();
             } else {
                 car.print(0);
                 return Nil.getInstance();
             }
             return new StringLit(">");
    	}

        public Node eval(Node t, environment e) {
            return Nil.getInstance();
        }

    }    
}


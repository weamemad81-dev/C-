using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int num = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("The number is: " + num);
    }
}



using System;

class Program
{
    static void Main()
    {
        string text = "123abc";

        int number = Convert.ToInt32(text);

        Console.WriteLine(number);
    }
}

///////// will throw a FormatException,  "123abc" contains non-numeric characters and cannot be converted to an integer////////




using System;

class Program
{
    static void Main()
    {
        double num1 = 10.5;
        double num2 = 2.5;

        double result = num1 + num2;

        Console.WriteLine("Result = " + result);
    }
}

////////Result = 13////////




using System;

class Program
{
    static void Main()
    {
        string text = "Hello World";

        string result = text.Substring(0, 5);

        Console.WriteLine(result);
    }
}

////////Hello///////




using System;

class Program
{
    static void Main()
    {
        int x = 10;
        int y = x;

        y = 20;

        Console.WriteLine("x = " + x);
        Console.WriteLine("y = " + y);
    }
}

////////x = 10
y = 20////////




using System;

class Program
{
    static void Main()
    {
        int[] arr1 = { 10, 20, 30 };
        int[] arr2 = arr1;

        arr2[0] = 100;

        Console.WriteLine("arr1[0] = " + arr1[0]);
        Console.WriteLine("arr2[0] = " + arr2[0]);
    }
}
/////////arr1[0] = 100
arr2[0] = 100/////////


using System;

class Program
{
    static void Main()
    {
        string firstName = "Hello";
        string secondName = "World";

        string result = firstName + " " + secondName;

        Console.WriteLine(result);
    }
}

/////Hello World////////

int d;
d = Convert.ToInt32(!(30 < 20));
//////d = 1///////




Console.WriteLine(13 / 2 + " " + 13 % 2);

//////6 1////////



int num = 1, z = 5;

if (!(num <= 0))
    Console.WriteLine(++num + z++ + " " + ++z);
else
    Console.WriteLine(--num + z-- + " " + --z);

//////7 7////////
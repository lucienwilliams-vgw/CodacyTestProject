using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class Fruit { }
public class Orange : Fruit { }
public class Apple : Fruit { }

public class ProblemExamples : MonoBehaviour
{
    public void DoSomething() { }

    public void ExplicitForEachConversionTest()
    {
        var fruitBasket = new List<Fruit>();
        fruitBasket.Add(new Orange());
        fruitBasket.Add(new Orange());
        // fruitBasket.Add(new Apple());  // uncommenting this line will make both foreach below throw an InvalidCastException

        foreach (Fruit fruit in fruitBasket)
        {
            var orange = (Orange)fruit; // This "explicit" conversion is hidden within the foreach loop below
        }

        foreach (Orange orange in fruitBasket) // Noncompliant
        {
            orange.GetType();
        }
    }

    public class Person
    {
        public int age;
        public string name;

        public override int GetHashCode()
        {
            int hash = 12;
            hash += this.age.GetHashCode(); // Noncompliant
            hash += this.name.GetHashCode(); // Noncompliant
            return hash;
        }
    }

    public void NaNCheck()
    {
        var a = double.NaN;

        if (a == double.NaN) // Noncompliant; always false
        {
            Debug.Log("a is not a number");  // this is dead code
        }
        if (a != double.NaN)  // Noncompliant; always true
        {
            Debug.Log("a is not NaN"); // this statement is not necessarily true
        }
    }

    public void ToStringRedundant()
    {
        var s = "foo";
        var t = "fee fie foe " + s.ToString();  // Noncompliant
        var someObject = new object();
        var u = "" + someObject.ToString(); // Noncompliant
        var v = string.Format("{0}", someObject.ToString()); // Noncompliant
    }

    public void UseOptionalBadly([Optional] ref int i) // Noncompliant
    {
        Debug.Log(i);
    }

    abstract class Base
    {
        public Base() // Noncompliant, should be private, private protected or protected
        {
            //...
        }
    }

    public void UseAwaitAndAsyncAsVars()
    {
        int async = 21;
        int await = 42;
    }

    public void BadTryCatch()
    {
        string fileName = "";
        string s = "";
        try
        {
            s = File.ReadAllText(fileName);
        }
        catch (Exception e)  // Noncompliant
        {
            throw;
        }
    }

    public void BadDefaultCaseLocation()
    {
        var result = 0;
        var param = 0;
        switch (param)
        {
            case 0:
                DoSomething();
                break;
            default: // default clause should be the first or last one
                DoSomething();
                break;
            case 1:
                DoSomething();
                break;
        }
    }

    private static void NonInvariantForEachValue()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine(i);
            if (true)
            {
                i = 20;
            }
        }
    }

    public void ElseIfNoBaseCase()
    {
        var x = 0;
        if (x == 0)
        {
            DoSomething();
        }
        else if (x == 1)
        {
            DoSomething();
        }
    }
}

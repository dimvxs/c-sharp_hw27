using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
using System.Text;
using System.Threading;

namespace hw
{
    public class Money: IDisposable
    {
        public int Dollars { get; set; }
        public int Cents { get; set; }
        public double Sum { get; set; }
        public string Move { get; set; }

        private FileStream fileStream;  // поле для FileStream
        private StreamWriter writer;    // поле для StreamWriter
     

public Money(){
WriteLine("enter count of dollars: ");
string d = ReadLine();
int dollars = int.Parse(d);
Dollars = dollars;


WriteLine("enter count of cents: ");
string c = ReadLine();
int cents = int.Parse(c);
Cents = cents;
Normalize();
Sum = dollars + cents;

Move = "money account is successfully created";
Logger(Move, Dollars, Cents);

}

private void Logger(string move, int dollars, int cents){
    if (fileStream == null)
            {
                fileStream = new FileStream("log.txt", FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(fileStream);
            }

             writer.WriteLine($"{move}: {DateTime.Now}, Amount: {dollars} dollars and {cents} cents");
}

public Money(int dollars, int cents){
    Dollars = dollars;
    Cents = cents;
    Normalize();
    Sum = dollars + cents;

    Move = "money account is successfully created";
    Logger(Move, Dollars, Cents);
}

public Money(double sum){
    Sum = sum;
    Dollars = (int)sum;
    Cents = (int)(sum - Dollars * 100);
    Normalize();

    Move = "money account is successfully created";
    Logger(Move, Dollars, Cents);
    }
      

         public static Money operator +(Money a, Money b){
            double result = a.Sum + b.Sum;

       if (a.Sum < 0)
    {
 throw new InvalidOperationException("Bankrupt");
    }

    a.Move = "money account operation +";
  a.Logger(a.Move, a.Dollars, a.Cents);

    return new Money(result);
    
}


public static Money operator -(Money a, Money b)
{
    double result = a.Sum - b.Sum;

        if (a.Sum < 0)
    {
 throw new InvalidOperationException("Bankrupt");
    }
     a.Move = "money account operation -";
    a.Logger(a.Move, a.Dollars, a.Cents);

    return new Money(result);
}

public static Money operator ++(Money a){
        if (a.Sum < 0)
    {
        throw new InvalidOperationException("Cannot apply the - operator to a negative value.");
    }
    
    a.Cents++;
    a.Normalize();
    a.Move = "money account operation ++";
     a.Logger(a.Move, a.Dollars, a.Cents);
    return new Money(a.Dollars, a.Cents); 
}    

public static Money operator --(Money a){
        if (a.Sum < 0)
    {
        throw new InvalidOperationException("Cannot apply the - operator to a negative value.");
    }
    
    a.Cents--;
    a.Normalize();
    a.Move = "money account operation --";
     a.Logger(a.Move, a.Dollars, a.Cents);
    return new Money(a.Dollars, a.Cents); 
}        

public static Money operator *(Money a, int num){

     double newSum = a.Sum * num;
    if (newSum < 0)
    {
        throw new InvalidOperationException("Bankrupt");
    }

    a.Move = "money account operation *";
    a.Logger(a.Move, a.Dollars, a.Cents);
    return new Money(newSum);
    
}

public static Money operator /(Money a, int num){

  

            if (a.Sum < 0)
    {
        throw new InvalidOperationException("Bankrupt");
    }
    double sum = a.Sum / num;

    a.Move = "money account operation /";
      a.Logger(a.Move, a.Dollars, a.Cents);

    return new Money(sum);
    
    
}


public static bool operator >(Money a, Money b){
    a.Move = "money account operation >";
    a.Logger(a.Move, a.Dollars, a.Cents);
    return a.Sum > b.Sum;
}

public static bool operator <(Money a, Money b){
    a.Move = "money account operation <";
    a.Logger(a.Move, a.Dollars, a.Cents);
    return a.Sum < b.Sum;
}

public static bool operator ==(Money a, Money b){
    a.Move = "money account operation ==";
  a.Logger(a.Move, a.Dollars, a.Cents);
    return a.Equals(b);
}
public static bool operator !=(Money a, Money b){
       a.Move = "money account operation !=";
    a.Logger(a.Move, a.Dollars, a.Cents);
      return !(a == b);
}
        public override string ToString() => $"{Dollars} dollars and {Cents} cents";


           private void Normalize()
        {
            if (Cents >= 100)
            {
                Dollars += Cents / 100;
                Cents %= 100;
            }
            else if (Cents < 0)
            {
                Dollars -= 1 + (-Cents) / 100;
                Cents = 100 - (-Cents) % 100;
            }

             Move = "money account operation Normalize";
               Logger(Move, Dollars, Cents);
        }

       

 public void Dispose()
        {
           if (writer != null)
            {
                writer.Close();  // Закрытие StreamWriter, что автоматически сбросит данные
                writer = null;
            }

            if (fileStream != null)
            {
                fileStream.Close();  // Закрытие FileStream
                fileStream = null;
            }

            WriteLine("Resources have been released!");
            
        }

    }

}
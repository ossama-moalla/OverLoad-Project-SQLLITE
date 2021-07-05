//using System;
//using System.Runtime.Remoting.Messaging;
//using System.Threading;

//class Program
//{
//    //Simple delegate declaration
//    public delegate int BinaryOp(int x, int y);
//    static void Main(string[] args)
//    {
//        Console.WriteLine("Main() running on thread {0}", Thread.CurrentThread.ManagedThreadId);

//        Program p = new Program();
//        BinaryOp bp = new BinaryOp(p.Add);
//        IAsyncResult iftAr = bp.BeginInvoke(5, 5, new AsyncCallback(p.AddComplete), "This message is from Main() thread " + Thread.CurrentThread.ManagedThreadId;
//        while (!iftAr.AsyncWaitHandle.WaitOne(100, true))
//        {
//            Console.WriteLine("Doing some work in Main()!");
//        }
//        Console.Read();
//    }

//    //An Add() method that do some simple arithamtic operation
//    public int Add(int a, int b)
//    {
//        Console.WriteLine("Add() running on thread {0}", Thread.CurrentThread.ManagedThreadId);
//        Thread.Sleep(500);
//        return (a + b);
//    }

//    //Target of AsyncCallback delegate should match the following pattern
//    public void AddComplete(IAsyncResult iftAr)
//    {
//        Console.WriteLine("AddComplete() running on thread {0}", Thread.CurrentThread.ManagedThreadId);
//        Console.WriteLine("Operation completed.");

//        //Getting result
//        AsyncResult ar = (AsyncResult)iftAr;
//        BinaryOp bp = (BinaryOp)ar.AsyncDelegate;
//        int result = bp.EndInvoke(iftAr);

//        //Recieving the message from Main() thread.
//        string msg = (string)iftAr.AsyncState;
//        Console.WriteLine("5 + 5 ={0}", result);
//        Console.WriteLine("Message recieved on thread {0}: {1}", Thread.CurrentThread.ManagedThreadId, msg);
//    }
//}
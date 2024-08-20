using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTutorial.Singleton
{

    //what is singleton design pattern, advantages and use cases
    //Singleton Design pattern is a creational design pattern which ensures that a class can only have one instance
    //and provides a global point of access to that instance.
    //This makes our application more consistence, reduce complexity and help us to manage our resources in a better way
    //For example in a database driven application this design pattern can create a database connection manager which can 
    //create a single database connection, rather than creating multiple database connection.This help us to control the
    //number of instances of a class and ensures that only single instance will be used throughout the entire application.

    //Below is the example of how to create a singleton design pattern
    //First of all let's see the simple example of how we create an instance of a class
    //Below class will represent our bussiness logic
    public class Singleton
    {
        //Let's create a public constructor and a static feild counter to count the number of instances created
        private static int counter = 0;
        public Singleton()
        {
            counter++;
            Console.WriteLine("Instance created " + counter.ToString());
        }

        //This class has only one method which will take an input of type string and console that input
        public void method1(string str)
        {
            Console.WriteLine("method output " + str);
        }
    }

    //Now Let's write our client code where we will be creating multiple instance of singleton class and call the method1()
    public class ClientCode
    {
        public static void Main(string[] args)
        {
            Singleton instance1 = new Singleton();
            instance1.method1("first instance created");

            Singleton instance2 = new Singleton();
            instance2.method1("second instance created");

            //we are creating two instance of singleton class instance1 and instance2 and calling the method1
            //The output of the above code will be
            //Instance created 1
            //method output first instance created
            //Instance created 2
            //method output second instance created
            //As you can see that we are ending up creating two instances of singleton class which is against the singleton
            //design pattern to implement the singleton design pattern see the below example
        }
    }

    //Now to implement singleton design pattern we need to follow the below steps
    //Step 1 : Create a sealed class
    //Step 2 : Make all the constructors inside the class private
    //step 3 : Create a private static feild which will be taking care of the number of instances created
    //Step 4 : Create a public feild which we will be exposing to the outside world to create an instance
    public sealed class Singleton1
    {
        private static int counter = 0;
        private static Singleton1 instance;

        private Singleton1()
        {
            counter++;
            Console.WriteLine("Instance created " + counter.ToString());
        }

        //In this code block we are checking if the instance feild is null or not if it is null that means no instance has been created yet
        //So we will be creating a new instance and will provide the global point of access to that instance using GetInstance feild
        //if the instance feild is not null that means there is already an instance created of this class so instead of creating new instance
        //We are returning the old instance back to the client
        public static Singleton1 GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton1();
                }
                return instance;
            }
        }

        //This class has only one method which will take an input of type string and console that input
        public void method1(string str)
        {
            Console.WriteLine("method output " + str);
        }
    }

    public class ClientCode1
    {
        public static void Main(string[] args)
        {
            Singleton1 instance1 = Singleton1.GetInstance;
            instance1.method1("first instance created");

            Singleton1 instance2 = Singleton1.GetInstance;
            instance2.method1("second instance created");
        }

        //The output of the above code will be
        //Instance created 1
        //method output first instance created
        //method output second instance created
        //Here we are ending up using the same instance that is how the singeton design pattern works
    }

    //Now there might be a question comes to your mind that why our singleton class is sealed
    //You might already know why we use sealed keyword if not it is to restrict the user to inherit the class
    //Now to answer your question what will happen if some other class inherit our singleton class
    //Let's create a new example without using sealed keyword
    public class Singleton2
    {
        private static int counter = 0;
        private static Singleton2 instance;

        private Singleton2()
        {
            counter++;
            Console.WriteLine("Instance created " + counter.ToString());
        }

        public static Singleton2 GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton2();
                }
                return instance;

            }
        }

        public void method1(string str)
        {
            Console.WriteLine("method output " + str);
        }

        //This is a nested class which will be inheriting our Singleton2 class
        public class ChildClass : Singleton2
        {

        }
    }

    public class ClientCode2
    {
        public static void Main(string[] args)
        {
            Singleton2.ChildClass instance1 = new Singleton2.ChildClass();
            instance1.method1("first instance created from child class");

            Singleton2.ChildClass instance2 = new Singleton2.ChildClass();
            instance2.method1("second instance created from child class");
        }

        //The output of the above code will be
        //Instance created 1
        //method output first instance created from child class
        //Instance created 2
        //method output second instance created from child class
        //As you can see if we do not make our singleston class sealed the class that will inherit our singleton class
        //can end up creating multiple instances so that is why our singleton class is always sealed
    }

    //Although we have implemented the singleton design pattern but still we have not restrict the user from creating the
    //multiple instances of singleton class completely it is because our singleton class is not thread safe
    //How it is not thread safe let's find out using the below example

    public class ClientCode3
    {
        public static void Main(string[] args)
        {
            Parallel.Invoke(() => threadMethod1(),
                            () => threadMethod2());
        }

        public static void threadMethod1()
        {
            Singleton1 instance1 = Singleton1.GetInstance;
            instance1.method1("first instance created");
        }

        public static void threadMethod2()
        {
            Singleton1 instance2 = Singleton1.GetInstance;
            instance2.method1("first instance created");
        }

        //Here i have created two methods which will be creating the instance of Singleton1 and will call method1
        //But the catch is i am calling both the functions at the same time so in that way i am ending up creating
        //two instances which is against the singleton design pattern so to avoid this we have to make our class thread safe
        //Below is the example of how to do that
    }

    public sealed class Singleton3
    {
        private static int counter = 0;
        private static Singleton3 instance;
        private static readonly object obj = new object(); // a new feild of type object to implement locking

        private Singleton3()
        {
            counter++;
            Console.WriteLine("Instance created " + counter.ToString());
        }

        public static Singleton3 GetInstance
        {
            get
            {
                lock(obj) //lock the obj to make sure only one thread can enter at a single time
                {
                    if (instance == null)
                    {
                        instance = new Singleton3();
                    }
                }
                return instance;
            }
        }

        //by implementing lock we make sure that only one thread can enter into the code block at a given point of time
        //this makes our code thread safe for a multi threaded application but there is also something that we can do
        //as you can see we are using lock to make our code thread safe but locks are very expensive to use and in a multi
        //threaded application there can be multiple threads that will be accessing our public feild at a given time so
        //everytime checking lock whenever multiple threads try to access our feild is expensive to avoid that we can use
        //double lock checking how we can implement double lock checking is simple just see the below example
    }

    public sealed class Singleton4
    {
        private static int counter = 0;
        private static Singleton4 instance;
        private static readonly object obj = new object();

        private Singleton4()
        {
            counter++;
            Console.WriteLine("Instance created " + counter.ToString());
        }

        public static Singleton4 GetInstance
        {
            get
            {
                if(instance == null) // that's it as simple as it is by checking if instance feild is null or not we can 
                {                    // implement double lock checking so that we don't have to do it everytime
                    lock (obj)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton4();
                        }
                    }
                }
                return instance;
            }
        }
    }

    //Now to move on to the next thing in singleton design pattern is eager loading and lazy loading
    //let's first learn what is eager loading with the given example


}

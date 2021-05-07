using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace modules_n_csharp
{
    class Program
    {
        private static HttpClient httpClient = new HttpClient();
        static async Task Main(string[] args)
        {
            Console.WriteLine("------------------Mutable------------------------");
            //Values are mutable
            int x = 10;
            x = 20;
            x = 30;
            x = 50;
            Console.WriteLine(x);
            Console.WriteLine("------------------Dynamically implemented properties------------------------");
            Students student = new Students("James", 18, "21/ug-223/BSCS", 3000);

            //age mutable

            student.age = 28;

            //name is readyOnly(immutable); cannot rewrite

            //student.name="Job";


            //regNo is mutable

            student.regNo = "21/ug-335/BSSE-S";

            student.tution = 7000;

            Console.WriteLine(student.name);
            Console.WriteLine(student.age);
            Console.WriteLine(student.regNo);
            Console.WriteLine(student.tution);

            Console.WriteLine("------------------Implicity typing------------------------");
            var scores = new[] { 1, 2.5, 3, 4, 5 };
            foreach (double element in scores)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine("------------------Object and collactions initializers------------------------");

            Products phone = new Products { pPrice = 3000, pName = "Smart Phone", pQuantity = 20, pBrand = "Oppo" };
            phone = new Products();

            Console.WriteLine(phone.pName + "" + phone.pPrice);

            // List<Products> products = new List<Products>{
            //     new Products { pPrice = 3000, pName = "Smart Phone", pQuantity = 20, pBrand = "Oppo" },
            //     new Products { pPrice = 6000, pName = "Smart Tv", pQuantity = 10, pBrand = "Samsung" } ,
            //     new Products { pPrice = 4000, pName = "Fridge", pQuantity = 5, pBrand = "LG" },
            //     new Products { pPrice = 5000, pName = "Oven", pQuantity = 2, pBrand = "LG" },
            // };

            GetProductsDelegate productsDelegate = new GetProductsDelegate(getProducts);
            List<Products> products = productsDelegate.Invoke();


            Console.WriteLine("------------------Lambda expression ------------------------");
            //Lambda expression
            //Lambda calculus 
            //Anything coming after the arrow funtion, should be condition.
            List<Products> hightPrices = products.FindAll(value => value.pPrice < 4000);

            foreach (Products product in hightPrices)
            {
                Console.WriteLine(product.pName + " " + product.pPrice);
            }

            Console.WriteLine("------------------LINQ Expression queries------------------------");
            //

            IEnumerable<Products> lowPrices =
              from product in products where product.pPrice < 5000 orderby product.pName ascending select product;

            foreach (Products product in lowPrices)
            {
                Console.WriteLine(product.pName + " " + product.pPrice);
            }

            Console.WriteLine("------------------ANONYMOUS DATATYPE------------------------");
            // Example in Js
            /*
              Example in Js
              var user={
                  fristname:'',
                  lastname:'',
                  nationality:'',
              }
            */
            // Example in C#

            var user = new
            {
                firstname = "Victor",
                lastname = "Manakana",
                nationality = "SA"
            };

            Console.WriteLine(user.firstname);
            Console.WriteLine(user.lastname);
            Console.WriteLine(user.nationality);
            
            //Dynamic 
            Console.WriteLine("--------------------------dynamic typing ----------------------------");
            Calculator<double> calculator= new Calculator<double>();
            dynamic sum=calculator.sum(10.9,12.9);
            Console.WriteLine("Sum is"+sum);

            //Optional parameters
            Console.WriteLine("--------------------------Optional parameters ----------------------------");
            Lecturers lecturers= new Lecturers(200.0);
            Console.WriteLine("Sum is: "+lecturers.name+" "+lecturers.wages);
            Lecturers lecturers2= new Lecturers(name:"James",wages:200.0);

            //Asynchronous
            Console.WriteLine("--------------------------Asynchronous----------------------------");

            // synchronization , asynchronization
            // T1 & T2, waits for t1 to finish
            // OS dictionary

            Program program= new Program();
            await program.checkin();
            await program.fetchUserData();
            await fetchResultsData();
        }

        //Asynch waits for Task1 to finish for it to start
        
        public async Task checkin(){
          Console.WriteLine("Working on Task 1");
          
          await Task.Delay(10000);
        //   await Task.Run(()=>{
        //      long x=1000000000000000000;
        //      long y=1000000000000000000;
        //      long z= x*y;
        //      long k= z*x*y*x*y*z*x*y*x*y;
        //   });    
          Console.WriteLine("Working on Task 2");
        }

        //Asynch fetching data

        public async Task<dynamic> fetchUserData(){
            //https://randomuser.me/api/?results=10
            string link="https://jsonplaceholder.typicode.com/todos";
            var user= await httpClient.GetStringAsync(link);
            Console.WriteLine(user);
            return user;

        }

        //Anonymous function with Async

        public static Func<Task<dynamic>> fetchResultsData= async delegate(){
            //https://randomuser.me/api/?results=10
            string link="https://randomuser.me/api/?results=10";
            var results= await httpClient.GetStringAsync(link);
            Console.WriteLine(results);
            return results;
        };
        
        public static List<Products> getProducts()
        {

            List<Products> products = new List<Products>{
                new Products { pPrice = 3000, pName = "Smart Phone", pQuantity = 20, pBrand = "Oppo" },
                new Products { pPrice = 6000, pName = "Smart Tv", pQuantity = 10, pBrand = "Samsung" } ,
                new Products { pPrice = 4000, pName = "Fridge", pQuantity = 5, pBrand = "LG" },
                new Products { pPrice = 5000, pName = "Oven", pQuantity = 2, pBrand = "LG" },
            };

            return products;
        }


    }

    public delegate List<Products> GetProductsDelegate();



}




public delegate List<Products> GetProductsDelegate();

public class Products
{
    public string pName;
    public double pPrice;
    public int pQuantity;

    public string pBrand;

    // public Products(string brand){
    //    this.pBrand=brand;
    // }
}


// Automatically implemented properties
public class Students
{
    static double TUTION_RATE = 2.0;
    public string name { get; }
    public int age { get; set; }

    public string regNo { get; set; }

    public string nationality;

    public double tution { get; set; }

    public Students(string name, int age, string regNo, double tution)
    {
        this.age = age;
        this.name = name;
        this.regNo = regNo;
        this.tution = getTution(tution);
    }

    public static double getTution(double tution)
    {
        return tution * TUTION_RATE;
    }


}

class Lecturers{
   public string name;
   public double wages;

   public Lecturers(double wages,string name="Default Lecturer"){
       this.name=name;
       this.wages=wages;
   }
}

class Calculator<T>
{
    //dynamic dataype

    public T sum(T a, T b)
    {
        dynamic x=a;
        dynamic y=b;

        return x + y;
    }

}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Engine_Console
{   
    class Program
    {                
        static void Main(string[] args)
        {                    
            TestStand overheatTestStand = new TestStand("Тест перегрева двигателя"); // создаём тестовый стенд

            Engine internalCombustorEngine = new Engine();  // создаём двигатель внутреннего сгорания с указанными параметрами
            internalCombustorEngine.Name = "Двигатель внутреннего сгорания";
            internalCombustorEngine.Inertia = 10;
            internalCombustorEngine.Mtorque = new double[] { 20, 75, 100, 105, 75, 0 };
            internalCombustorEngine.Vspeed = new double[] { 0, 75, 150, 200, 250, 300 };
            internalCombustorEngine.Toverheat = 110;
            internalCombustorEngine.Hm = 0.01;
            internalCombustorEngine.Hv = 0.0001;
            internalCombustorEngine.C = 0.1;
                                                         
            overheatTestStand.TestResult(internalCombustorEngine);  // запускаем тест и выводим результат в консоль           

            Console.ReadKey(); // чтоб консоль не закрывалась :)


        }
    }
}

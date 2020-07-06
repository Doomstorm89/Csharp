using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Engine_Console
{
    /// <summary>
    /// Класс для создания тестов
    /// </summary>
    class TestStand
    {
        /// <summary>
        /// Название тестового стенда
        /// </summary>
        public string TestStandName { get; set; }
        /// <summary>
        /// Для переключения значений скорости вращения коленвала и крутящего момента
        /// </summary>
        protected int CurrentVspeed { get; set; }

        public TestStand()
        {
            
        }

        public TestStand(string testStandName)
        {
            this.TestStandName = testStandName;
        }

        /// <summary>
        /// Метод, берущий у пользователя температуру окружающей среды, преобразующий в нужный формат и возвращающий её
        /// </summary>
        /// <returns>Температура окружающей среды</returns>
        private double GetOutsideTemperature()
        {            
            double outsideTemperature;  // переменная для температуры окружающей среды
            string temperatureToString; // временная переменная для хранения строкового представления температуры
            do
            {
                Console.WriteLine("Введите температуру окружающей среды: ");                
                temperatureToString = Console.ReadLine();                
            }
            while (!double.TryParse(temperatureToString, out outsideTemperature));  // Пока не преобразует данные в double
            
            return outsideTemperature;
        }

        /// <summary>
        /// Статический метод, принимающий двигатель и возвращающий его ускорение
        /// </summary>
        /// <param name="engine">Двигатель</param>
        /// <returns>Ускорение</returns>
        private static double Acceleration(Engine engine)
        {
            double acceleration = engine.M / engine.Inertia;  
            return acceleration;
        }

        /// <summary>
        /// Метод, принимающий двигатель и возвращающий скорость его нагрева
        /// </summary>
        /// <param name="engine">Двигатель</param>
        /// <returns>Скорость нагрева двигателя</returns>
        private static double Vheat(Engine engine)
        {           
            double Vh = engine.M * engine.Hm + Math.Pow(engine.V, 2) * engine.Hv;
            return Vh;
        }
        
        /// <summary>
        /// Метод, принимающий двигатель, температуру окружающей среды и температуру двигателя, возвращающий скорость охлаждения
        /// </summary>
        /// <param name="engine">Двигатель</param>
        /// <param name="Toutside">Температура окружающей среды</param>
        /// <param name="Tengine">Температура двигателя</param>
        /// <returns>Скорость охлаждения двигателя</returns>
        private static double Vcold(Engine engine, double Toutside, double Tengine)
        {                      
            double Vc = engine.C * (Toutside - Tengine);
            return Vc;
        }

        /// <summary>
        /// Метод, принимающий двигатель и возвращающий истину/ложь. Для контроля переключения значений коленвала и крутящего момента
        /// </summary>
        /// <param name="engine">Двигатель</param>
        /// <returns>Проверка значений скорости коленвала и крутящего момента</returns>
        private bool VspeedCheck(Engine engine)
        {
            if (engine.V + Acceleration(engine) < engine.Vspeed[CurrentVspeed])
                return false;
            else
                return true;
        }

        /// <summary>
        /// Метод, принимающий двигатель и возвращающий результат тестирования на перегрев
        /// </summary>
        /// <param name="engine">Двигатель</param>
        /// <returns>Результат тестирования</returns>
        private int OverheatTest (Engine engine)
        {
            int time = 0;   // для моделирования времени (секунды)
            CurrentVspeed = 0;  

            double minOutsideTemperature = -100;    // минимальная температура окружающей среды
            double maxOutsideTemperature = 100;     // максимальная температура окружающей среды

            double engineTemperature;   // температура двигателя                 
            double outsideTemperature;  // температура окружающей среды
            double temporaryTemperature;// временная переменная для температуры двигателя

            outsideTemperature = GetOutsideTemperature();   // забираем у пользователя значение температуры окруж. среды
            
            if (outsideTemperature <= minOutsideTemperature || outsideTemperature >= maxOutsideTemperature)
            {
                return -1;  // если температура ниже минимальной или выше максимальной, то возвращаем -1
            }

            engineTemperature = outsideTemperature; // температура двигателя изначально равна температуре окружающей среды            

            engine.V = engine.Vspeed[0];    // стартовые значения скорости вращения коленвала и крутящего момента
            engine.M = engine.Mtorque[0];

            while (engineTemperature <= engine.Toverheat)   // пока температура двигателя не превышает температуру перегрева двигателя
            {
                time++; // плюсуем время (секунды)
               
                if (!VspeedCheck(engine))   // если значения не превысят пороговые значения, то далее присваиваем новые значения
                {
                    engine.V += Acceleration(engine);
                    engine.M = engine.Mtorque[CurrentVspeed];                    
                }
                if (VspeedCheck(engine) && CurrentVspeed < engine.Vspeed.Length - 1)    // если превышают, то переключаем вверх
                {
                    CurrentVspeed++;
                }

                temporaryTemperature = engineTemperature;   // присваиваем временной переменной текущую температуру двигателя
                engineTemperature += Vheat(engine) + Vcold(engine, outsideTemperature, engineTemperature); // присваиваем текущей
                                                            //температуре двигателя новое значение с учетом нагрева и охлаждения
                
                if (temporaryTemperature == engineTemperature) // если временная и новая текущая температура равны,
                {                                              // значит двигатель не перегреется и возвращаем 1
                    return 1;
                }
                
                if (engineTemperature >= engine.Toverheat)      // если текущая температура больше температуры перегрева
                {                                               // возвращаем значение времени на котором произошёл перегрев
                    return time;
                }
            }
            return time;
        }
        /// <summary>
        /// Метод, обрабатывающий результаты теста на перегрев и выводящий их в консоль
        /// </summary>
        /// <param name="engine">Двигатель</param>
        public void TestResult(Engine engine)
        {
            int tmp = OverheatTest(engine); // временная переменная для корректной обработки результата тестирования
            switch (tmp)
            {
                case -1: Console.WriteLine("Слишком низкая/высокая температура окружающей среды. Тест невозможен.");break;               
                case  1: Console.WriteLine("При текущей температуре окружающей среды двигатель не перегревается.");break;
                default: Console.WriteLine($"Двигатель перегрелся на {tmp} секунде.");break;
            }
        }
        
    }
}

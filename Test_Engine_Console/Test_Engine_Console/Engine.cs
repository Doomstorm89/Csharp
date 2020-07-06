using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Engine_Console
{
    /// <summary>
    /// Класс для создания двигателей
    /// </summary>
    class Engine 
    {
        /// <summary>
        /// Название двигателя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Момент инерции двигателя
        /// </summary>
        public double Inertia { get; set; }
        /// <summary>
        /// Показатели крутящего момента
        /// </summary>
        public double[] Mtorque { get; set; }
        /// <summary>
        /// Показатели скорости вращения коленвала
        /// </summary>
        public double[] Vspeed { get; set; }
        /// <summary>
        /// Температура перегрева двигателя
        /// </summary>
        public double Toverheat { get; set; }
        /// <summary>
        /// Коэффициент зависимости скорости нагрева от крутящего момента
        /// </summary>
        public double Hm { get; set; }
        /// <summary>
        /// Коэффициент зависимости скорости нагрева от скорости вращения коленвала
        /// </summary>
        public double Hv { get; set; }
        /// <summary>
        /// Коэффициент зависимости скорости охлаждения от температуры двигателя и окружающей среды
        /// </summary>
        public double C { get; set; }
       
        /// <summary>
        /// Крутящий момент
        /// </summary>
        public double M { get; set; }
        /// <summary>
        /// Скорость вращения коленвала
        /// </summary>
        public double V { get; set; }
        /// <summary>
        /// Конструктор двигателя без параметров
        /// </summary>
        public Engine()
        {
            
        }
        /// <summary>
        /// Конструктор двигателя с параметрами
        /// </summary>
        /// <param name="name">Название двигателя</param>
        /// <param name="inertia">Момент инерции двигателя</param>
        /// <param name="mtorque">Показатели крутящего момента</param>
        /// <param name="vspeed">Показатели скорости вращения коленвала</param>
        /// <param name="toverheat">Температура перегрева двигателя</param>
        /// <param name="hm">Коэффициент зависимости скорости нагрева от крутящего момента</param>
        /// <param name="hv">Коэффициент зависимости скорости нагрева от скорости вращения коленвала</param>
        /// <param name="c">Коэффициент зависимости скорости охлаждения от температуры двигателя и окружающей среды</param>
        public Engine(string name, double inertia, double[] mtorque, double[] vspeed,
                                   double toverheat, double hm, double hv, double c)
        {
            this.Name = name;
            this.Inertia = inertia;
            this.Mtorque = mtorque;
            this.Vspeed = vspeed;
            this.Toverheat = toverheat;
            this.Hm = hm;
            this.Hv = hv;
            this.C = c;            
            this.M = Mtorque[0];    // начальное значение крутящего момента 
            this.V = Vspeed[0];     // начальное значение скорости вращения коленвала
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;
using System.Net;
using System.Net.Http;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using MihaZupan;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TelegramBot tgBot;
         
        public MainWindow()
        {
            
            InitializeComponent();

            if (!File.Exists(@"songs.json")) // проверка существует ли файл, если нет, то создаётся с нулевыми счетчиками
            {
                File.Create(@"songs.json");
                List<Songs> songsListTmp = new List<Songs>();
                var song = JsonConvert.DeserializeObject<Songs>(File.ReadAllText(@"songs.json"));
                songsListTmp.Add(new Songs("flowers", "CQACAgIAAxkBAANyXmIMJQn9Q4cuC3Iv2PyVKL2CkgQAAh0FAAKcHhBL4-Qc2j4rqTIYBA", 0));
                songsListTmp.Add(new Songs("gameMenu", "CQACAgIAAxkBAAN1XmIMn2JwmhlbY4LVZzHKafDH6soAAq8GAAIuYRlLVC_sSrInL2wYBA", 0));
                songsListTmp.Add(new Songs("hunter", "CQACAgIAAxkBAAN4XmIM3Cth-EZCqerBbjpRjlQS3ScAAogFAALfgBFLKc1NGy_n6OcYBA", 0));
                songsListTmp.Add(new Songs("miami", "CQACAgIAAxkBAAN7XmINA2tgyU3xsNGBxMyw35xlwHYAAokFAALfgBFLBnUTjcGO9lsYBA", 0));
                songsListTmp.Add(new Songs("whenSheSmiles", "CQACAgIAAxkBAAN-XmINKuhPMjPZuNShvs880iY0htwAArQGAAIuYRlLhxiOqXIT_N4YBA", 0));

                string jsonTmp = JsonConvert.SerializeObject(songsListTmp);
                File.WriteAllText(@"songs.json", jsonTmp);
            }

            List<Songs> songsList = new List<Songs>();   // если файл существует, то в этот лист из songs.json подцепляются значения
            string json = File.ReadAllText("songs.json");
            songsList = JsonConvert.DeserializeObject<List<Songs>>(json);
            this.DataContext = songsList;   // для отображения счетчика песен на старте программы
            
            start.IsEnabled = false;  // недопступность кнопок запуска и остановки, если поля ввода данных пустые
            stop.IsEnabled = false;

            if (File.Exists(@"info.json"))   // если уже вводились данные, то подставить их при запуске
            {
                string[] separator = {",","\"","[","]"};
                string[] info = File.ReadAllText(@"info.json").Split(separator,StringSplitOptions.RemoveEmptyEntries);
                token.Password = info[0];
                ip.Text = info[1];
                port.Text = info[2];
                login.Text = info[3];
                password.Password = info[4];
                start.IsEnabled = true;
                stop.IsEnabled = true;               
            }

            if (File.Exists(@"songs.json"))   // Данные для счётчика 
            {               
                    _flowers.Text = songsList[0].SongCount.ToString();
                    _gameMenu.Text = songsList[1].SongCount.ToString();
                    _hunter.Text = songsList[2].SongCount.ToString();
                    _miami.Text = songsList[3].SongCount.ToString();
                    _wss.Text = songsList[4].SongCount.ToString();
               
            }
            
        }

        private void open_Click(object sender, RoutedEventArgs e) // кнопка открытия текстового файла
        {                                                         //  для взятия данных прокси и логина/пароля
            string[] info;
            string[] separators = { ";", ",", " ", "\n" };

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                info = File.ReadAllText(openFileDialog.FileName).Split(separators, StringSplitOptions.RemoveEmptyEntries);

                token.Password = info[0];     // забираем токен
                ip.Text = info[1];            // забираем IP прокси
                port.Text = info[2];          // порт прокси
                login.Text = info[3];         // имя пользователя
                password.Password = info[4];  // пароль

                start.IsEnabled = true;
                stop.IsEnabled = true;
                

                if (!File.Exists(@"info.json"))   // пишем данные в json файл, чтоб повторно не вводить
                {
                    JsonSerializer js = new JsonSerializer();
                    using (StreamWriter sw = new StreamWriter(@"info.json"))
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        js.Serialize(writer, info);
                    }
                }
            }
        }

        public bool botStarted = false;   // чтобы повторно не запускался код, когда он запущен))
        private void start_Click(object sender, RoutedEventArgs e)  // запускаем бота
        {
           
           
            if (!botStarted && token.Password!=null &&
                ip.Text!=null && port.Text!=null &&
                login.Text!=null && password.Password!=null)
            {

                tgBot = new TelegramBot(this);
                botStarted = true;   
            }
        }

        /// <summary>
        /// Метод "ручного" получения пользователем списка песен и песни
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void stop_Click(object sender, RoutedEventArgs e)  // Останавливаем бота
        {
            if (botStarted)
            {
                tgBot.BotStopReceiving();
                botStarted = false;
            }
        }
    } 
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FiveM_Teamspeak_Teamspeak_Launcher
{
    class Program
    {

        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        static void Main(string[] args)
        {

            Console.WriteLine("Drücke Y um das Spiel, Discord und Teamspeak zu starten und auf Loyal Roleyplay zu verbinden!");
            Console.WriteLine("Drücke N um dieses Fenster zu schließen!");
            Console.WriteLine();
            Console.WriteLine("Drücke C für einen kurzen Systemcheck - empfohlen wenn das Programm zum ersten mal gestartet wird.");

            char key = Console.ReadKey(true).KeyChar;

            if (key == 'y' || key == 'Y')
                startAll();
            else if (key == 'c' || key == 'C')
                checkFiles();
            else
                Environment.Exit(0);

        }


        public static void startAll()
        {
            Console.Clear();
            Console.WriteLine("Wird gestartet...");
            Console.WriteLine("Programm schließt sich von selbst sobald alles läuft!");
            System.Threading.Thread.Sleep(1000);
            startDiscord();
        }


        public static void checkFiles()
        {

            bool discordFound = false;

            discordFound = File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Discord\Update.exe");
            Console.Clear();
            if (discordFound)
                Console.WriteLine("Discord wurde gefunden!");
            else
                Console.WriteLine("Discord wurde nicht gefunden, bitte Peter Rameder#7548 kontaktieren!");

            Console.WriteLine("");
            Console.WriteLine("Taste drücken um fortzufahren");
            Console.ReadKey();

            Console.Clear();

            Process restart = System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
            SetForegroundWindow(GetConsoleWindow());

            Environment.Exit(0);

        }


        public static bool isActive(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }
            return false;
        }


        public static void startTeamspeak()
        {

            if(isActive("ts3client_win64.exe"))
            {
                Console.Write("");
                Console.Clear();
            }
            else
            {
                System.Diagnostics.Process.Start("ts3server://134.255.231.80:9046");
            }

            startFiveM();

        }
    

        public static void startFiveM()
        {

            System.Threading.Thread.Sleep(5000);

            if (isActive("FiveM_GTAProcess.exe"))
            {
                Console.Write("");
                Console.Clear();
            }
            else
            {
                System.Diagnostics.Process.Start("fivem:\\193.23.126.96:30120");
            }

        }

        public static void startDiscord()
        {

            if (isActive("Discord.exe"))
            {
                Console.Write("");
                Console.Clear();
            }
            else
            {
                System.Diagnostics.Process discord = new System.Diagnostics.Process();
                discord.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Discord\Update.exe";
                discord.StartInfo.Arguments = "--processStart Discord.exe";
                discord.Start();
            }

            System.Threading.Thread.Sleep(2500);

            startTeamspeak();
            
        }

    }
}

using Newtonsoft.Json;
using Simulator.Models;
using SimulatorEvent.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public class SportEvent
    {
        int eventNumber = 1;
        #region - Methods - 
        /// <summary>
        /// Get written timeout by user
        /// </summary>
        /// <returns></returns>
        public int GetTime()
        {
            Console.WriteLine("Napíšte náhodne pozastavenie v sekundách : ");
            int timeOut = 0;
            int.TryParse(Console.ReadLine(), out timeOut);
            return timeOut;
        }

        /// <summary>
        /// Get data from the file
        /// </summary>
        /// <returns></returns>
        public List<EventModel> ReadData()
        {
            bool fileExist = File.Exists(ConfigurationManager.AppSettings["Path"]);
            List<int> choseElements = new List<int>();
            if (fileExist)
            {
                using (StreamReader file = new StreamReader(ConfigurationManager.AppSettings["Path"]))
                {
                    string info = file.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<EventModel>>(info);

                }
            }
            Console.WriteLine($"Subor neixistuje na danej ceste: {ConfigurationManager.AppSettings["Path"]}");
            return null;
        }

        /// <summary>
        /// Parse and pick received data 
        /// </summary>
        /// <param name="eventList"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async void PreparingData(List<EventModel> eventList)
        {         
            
            Parallel.For(0, eventList.Count(), i =>
            {
                SendEvent(eventList[i]);
                eventNumber++;
                Console.WriteLine($"Udalosť číslo {eventNumber} bolo úspešne poslane");
            });
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"Počet načítaných události : {eventList.Count()}");
            Console.WriteLine($"Počet poslaných události : {eventNumber}");
            
        }

        
        /// <summary>
        /// Send request for data sending to WebApi
        /// </summary>
        /// <param name="choseEvent"></param>
        /// <returns></returns>
        public async void SendEvent(EventModel choseEvent)
        {
            Random random = new Random();
            using (HttpClient client = new HttpClient())
            {
                
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAdress"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response1 = new HttpResponseMessage();
               
                foreach (var item in choseEvent.OddsList)
                {
                    //int timeOut = random.Next(0, 10);
                    //Thread.Sleep(timeOut * 1000);
                    SendEventModel sendEvent = new SendEventModel();
                    sendEvent.ProviderEventID = choseEvent.ProviderEventID;
                    sendEvent.EventName = choseEvent.EventName;
                    sendEvent.EventDate = choseEvent.EventDate;
                    sendEvent.OddsName = item.OddsName;
                    sendEvent.OddsRate = item.OddsRate;
                    sendEvent.Status = item.Status;
                    sendEvent.ProviderOddsID = item.ProviderOddsID;

                    var json = JsonConvert.SerializeObject(sendEvent);
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/event", json);
                    Console.WriteLine("Name of current running " + "thread: {0}", Thread.CurrentThread.Name);
                    Console.WriteLine("Id of current running " + "thread: {0}", Thread.CurrentThread.ManagedThreadId);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Status " + response.StatusCode);

                    }
                    else
                    {
                        Console.WriteLine("Internal server Error");
                        Console.WriteLine("Status " + response.StatusCode);
                    }
                }
            }

        }

        /// <summary>
        /// Get command to continue or stop program working
        /// </summary>
        /// <returns></returns>
        public bool GetCommand()
        {
            string command = string.Empty;
            Console.WriteLine("Chcete pokračovat Y/N ?");
            command = Console.ReadLine();

            if (!string.IsNullOrEmpty(command) && command.ToLower() == "y")
            {
               
                return true;
                
            }

            return false;
        }
        #endregion
    }
}

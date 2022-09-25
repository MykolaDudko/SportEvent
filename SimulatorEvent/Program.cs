using Newtonsoft.Json;
using Simulator.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading;

namespace Simulator
{
    internal class Program
    {
        static  void Main(string[] args)
        {
            SportEvent sportEvent = new SportEvent();
            do
            {
                sportEvent.PreparingData(sportEvent.ReadData());
            }

            while (sportEvent.GetCommand());
        }
    }
}

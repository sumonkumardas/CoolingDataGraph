using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolingDataGraph.Models
{
    public class CoolingData
    {
        public DateTime CoolingTime { get; set; }
        public double Value { get; set; }
        public bool AnomanlyDetected { get; set; }
        public List<CoolingData> CoolingLoadDataList { get; set; }

        public List<CoolingData> ConvertCoolingDataFromCsv(string filePath)
        {
            try
            {
                var yourData = File.ReadAllLines(filePath)
                   .Skip(1)
                   .Select(x => x.Split(','))
                   .Select(x => new CoolingData
                   {
                       CoolingTime = Convert.ToDateTime(x[0]),
                       Value = Convert.ToDouble(x[1]),
                       AnomanlyDetected = GetData(Convert.ToDateTime(x[0]), Convert.ToDouble(x[1]))
                   });

                List < CoolingData > coolingDatas = new List<CoolingData>();
                foreach (var coolingData in yourData)
                {
                    coolingDatas.Add(coolingData);
                }
                return coolingDatas;
            }
            catch
            {
                return null;
            }
            
        }

        private bool GetData(DateTime toDateTime, double toDouble)
        {
            var firstTimeStartSpan = new TimeSpan(0, 0, 0);
            var firstTimeEndSpan = new TimeSpan(7, 0, 0);
            var secondTimeStartSpan = new TimeSpan(8, 0, 0);
            var secondTimeEndSpan = new TimeSpan(17, 0, 0);
            var thirdTimeStartSpan = new TimeSpan(18, 0, 0);
            var thirdTimeEndSpan = new TimeSpan(23, 59, 0);

            if (toDateTime.TimeOfDay >= firstTimeStartSpan && toDateTime.TimeOfDay <= firstTimeEndSpan)
            {
                return toDouble > 500;
            }

            else if (toDateTime.TimeOfDay >= secondTimeStartSpan && toDateTime.TimeOfDay <= secondTimeEndSpan)
            {
                return toDouble > 1500;
            }

            else if (toDateTime.TimeOfDay >= thirdTimeStartSpan && toDateTime.TimeOfDay <= thirdTimeEndSpan)
            {
                return toDouble > 500;
            }

            else
                return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolingDataGraph.Models
{
    public class GraphModel
    {
        public List<string> Categories { get; set; }
        public List<Series> SeriesDatas { get; set; }

        public void SetModelValue(List<CoolingData> coolingLoadDataList)
        {
            Categories = new List<string>();
            SeriesDatas = new List<Series>();
            MakeDataPointSeries(coolingLoadDataList);
        }

        private void MakeDataPointSeries(List<CoolingData> coolingLoadDataList)
        {
            
            Series tempSeries = new Series();
            bool state = coolingLoadDataList[0].AnomanlyDetected;
            for (int i = 0; i < coolingLoadDataList.Count; i++)
            {
                tempSeries.Annomaly = state;
                Categories.Add(coolingLoadDataList[i].CoolingTime.TimeOfDay.ToString());

                tempSeries.Data.Add(coolingLoadDataList[i].Value);
                if (!state)
                {
                    tempSeries.Color = "blue";
                    tempSeries.Name = "Data Point";
                }
                else
                {
                    tempSeries.Color = "red";
                    tempSeries.Name = "Anomaly Data Point";
                }
                if (i + 1 != coolingLoadDataList.Count && coolingLoadDataList[i + 1].AnomanlyDetected != state)
                {
                    if (state)
                    {
                        if (tempSeries.Data.Count > 2)
                        {
                            tempSeries.Data.Add(coolingLoadDataList[i + 1].Value);
                        }
                        tempSeries.Data.RemoveAt(0);
                        tempSeries.PointStart++;
                    }
                    else
                    {
                        tempSeries.Data.Add(coolingLoadDataList[i + 1].Value);
                    }

                    SeriesDatas.Add(tempSeries);

                    tempSeries = new Series();

                    if (SeriesDatas.Count > 0 && tempSeries.PointStart == null)
                    {
                        tempSeries.PointStart = i;
                        tempSeries.Data.Add(coolingLoadDataList[i].Value);
                    }

                    state = !state;
                }
            }
            if (state)
            {
                tempSeries.Data.RemoveAt(0);
                tempSeries.PointStart++;
            }

            SeriesDatas.Add(tempSeries);
        }
    }
}

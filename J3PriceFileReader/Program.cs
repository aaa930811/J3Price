using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J3Price.DAL;
using log4net;

namespace J3PriceFileReader
{
    class Program
    {
        private static DALQuiz DAL = new DALQuiz();
        static void Main(string[] args)
        {
            Console.WriteLine("请输入文件地址");
            string path = Console.ReadLine();
            log4net.Config.XmlConfigurator.Configure();
            Read(path);
            Console.WriteLine("按回车键退出");
            Console.ReadLine();
        }
        public static void Read(string path)
        {
            ILog log = LogManager.GetLogger("J3Price.Logging");//获取一个日志记录器
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                string name = line.Split('：')[0];
                string[] info = line.Split('：')[1].Split(' ');
                string serviceName = info[0];
                string price = info[1];
                string saletypeName = info[2];
                string product = info[3];
                DateTime time = DateTime.Parse(info[4]);
                ServiceMst mst = DAL.GetServiceMstByName(serviceName);
                int salecode = DAL.GetSaleCodeByName(saletypeName);
                try
                {
                    DAL.CreateQuote((int)mst.RegionID, mst.ServiceID, salecode, product, price, time, null, name, false);
                    log.Info(name+'：'+serviceName+' '+price+' '+saletypeName+' '+product+' '+time +" 录入成功");
                }
                catch (Exception ex)
                {
                    log.Info(name + '：' + serviceName + ' ' + price + ' ' + saletypeName + ' ' + product + ' ' + time + " 录入失败 "+ex);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCFS
{
    class Program
    {
        static int TIME=99999;//获取最小时间
        static int index = -1;//对应的进程
        static int ALLSTIME = 0;
        static int ALLETIME = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("请输入进程数:");
            int len = int.Parse(Console.ReadLine());
            Process[] ps = new Process[len];
            for (int i = 0; i < len; i++)
            {
                ps[i] = new Process();
                ps[i].P_ID = i + 1;
                Console.WriteLine("第" + ps[i].P_ID + "个进程的进入时刻（0-99999）");
                ps[i].P_enterTime = int.Parse(Console.ReadLine());
                Console.WriteLine("第" + ps[i].P_ID + "个进程的运行时间");
                ps[i].P_runTime = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("进程ID   进入时间    开始时间    运行时间    结束时间    周转时间");
            for (int i = 0; i < len; i++)
            {
               CHECK(ps);
            }
            Console.ReadKey();
        }
        public  static void CHECK(Process[] ps)
        {
            for (int i = 0; i < ps.Length; i++)
            {
                if(ps[i].visit)
                {
                    continue;
                }
                if (index == -1)
                {
                    index = i;
                    TIME = ps[i].P_enterTime;
                    continue;
                }
                if (ps[i].P_enterTime < TIME)
                {
                    index = i;
                    TIME = ps[i].P_enterTime;
                }
            }
            if (ALLSTIME == 0 & ps[index].P_startTime != 0)
            {
                ALLSTIME = ps[index].P_enterTime;
            }
            ps[index].P_startTime = ALLSTIME;
            ALLETIME=ALLSTIME + ps[index].P_runTime;
            ps[index].P_endTime = ALLETIME;
            ps[index].P_turnoverTime = ps[index].P_endTime - ps[index].P_enterTime;
            Console.WriteLine("     " + ps[index].P_ID + "          " + ps[index].P_enterTime + "           " + ps[index].P_startTime + "           " + ps[index].P_runTime + "        " + ps[index].P_endTime + "         " + ps[index].P_turnoverTime);
            ps[index].visit = true;
            ALLSTIME = ALLETIME;
            index = -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJF
{
    class Program
    {
        static int TIME = 99999;//获取最小时间
        static int index = -1;//对应的进程
        static int ALLSTIME = 0;//开始时间
        static int ALLETIME = 0;//结束时间
        static int shortruntime = 99999;//获取短作业
        static void Main(string[] args)
        {
            Console.WriteLine("SJF算法");
            Console.WriteLine("请输入进程数:");
            int len = int.Parse(Console.ReadLine());
            Process[] ps = new Process[len];
            for (int i = 0; i < len; i++)
            {
                ps[i] = new Process();
                ps[i].P_ID = i + 1;
                Console.Write("第" + ps[i].P_ID + "个进程的进入时刻（0-99999）:");
                ps[i].P_enterTime = int.Parse(Console.ReadLine());
                Console.Write("第" + ps[i].P_ID + "个进程的运行时间:");
                ps[i].P_runTime = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("进程ID   进入时间    开始时间    运行时间    结束时间    周转时间");
            for (int i = 0; i < len; i++)
            {
                CHECK(ps);
            }
            Console.ReadKey();
        }
        public static void CHECK(Process[] ps)
        {
            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i].visit)//判断进程是否访问
                {
                    continue;
                }
                if (index == -1)//刚开始赋值
                {
                    index = i;//给定进程下标
                    TIME = ps[i].P_enterTime;//进行初赋值
                    continue;
                }
                if (ps[i].P_runTime < shortruntime&&ps[i].P_enterTime<ALLSTIME)
                {
                    index = i;
                    TIME = ps[i].P_enterTime;
                }
            }
            if (ALLSTIME == 0 & ps[index].P_startTime != 0)//当执行的第一个进程不是第0秒开始时，进行初赋值
            {
                ALLSTIME = ps[index].P_enterTime;
            }
            ps[index].P_startTime = ALLSTIME;//每一个进程的开始时间为总开始时间
            ALLETIME = ALLSTIME + ps[index].P_runTime;//执行本次进程后的结束时间更新总最后时间
            ps[index].P_endTime = ALLETIME;//填入本进程的最后结束时间
            ps[index].P_turnoverTime = ps[index].P_endTime - ps[index].P_enterTime;//周转筛箕   
            Console.WriteLine("     "+ps[index].P_ID+"          "+ps[index].P_enterTime+"           "+ps[index].P_startTime+"           "+ ps[index].P_runTime + "        "+ps[index].P_endTime+"         "+ps[index].P_turnoverTime);
            ps[index].visit = true;
            ALLSTIME = ALLETIME;
            index = -1;
        }
    }
}

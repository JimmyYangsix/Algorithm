using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RR
{
    class Program
    {
        static int index = -1;//对应的进程
        static int ALLTIME = 0;//全局时间
        static int TimeLength;//时间片长度
        static int over = 0;//已完成的进程数
        static int minEntertime=-1;//获取最先进入的进程
        static void Main(string[] args)
        {
            Console.WriteLine("请输入进程数:");
            int len = int.Parse(Console.ReadLine());
            Process[] ps = new Process[len];
            Console.WriteLine("请输入时间片长度");
            TimeLength = int.Parse(Console.ReadLine());
            for (int i = 0; i < len; i++)
            {
                ps[i] = new Process();
                ps[i].P_ID = i + 1;
                Console.WriteLine("第" + ps[i].P_ID + "个进程的进入时刻（0-99999）");
                ps[i].P_enterTime = int.Parse(Console.ReadLine());
                Console.WriteLine("第" + ps[i].P_ID + "个进程的运行时间");
                ps[i].P_runTime = int.Parse(Console.ReadLine());
                ps[i].P_leaverunTime = ps[i].P_runTime;//剩余执行时间初始化               
            }
            Console.WriteLine("进程ID   进入时间    运行时间    结束时间    周转时间");
            CHECK(ps,len);           
            Console.ReadKey();
        }
        public static void CHECK(Process[] ps,int lenth)
        {
            for (int i = 0; i < ps.Length; i++)
            {
                if (minEntertime == -1)
                {
                    minEntertime = ps[i].P_enterTime;
                    index = i;
                    continue;
                }
                else
                {
                    if (ps[i].P_enterTime < minEntertime)
                    {
                        minEntertime = ps[i].P_enterTime;
                        index = i;
                    }
                }
           
            }
            if (ALLTIME == 0 & ps[index].P_enterTime != 0)//开始时间进行赋值
            {
                ALLTIME = ps[index].P_enterTime;
            }
            while (true)
            {

                if (ps[index].P_enterTime > ALLTIME || ps[index].visit)
                {
                    index = (index + 1) % ps.Length;
                    continue;
                }
                if (ps[index].P_leaverunTime >= TimeLength)
                {
                    ps[index].P_leaverunTime -= TimeLength;
                    ALLTIME += TimeLength;
                    if (ps[index].P_leaverunTime == 0)
                    {
                        ps[index].P_turnoverTime = ALLTIME - ps[index].P_enterTime;
                        ps[index].P_endTime = ALLTIME;
                        Console.WriteLine("     " + ps[index].P_ID + "          " + ps[index].P_enterTime + "           " + ps[index].P_runTime + "        " + ps[index].P_endTime + "         " + ps[index].P_turnoverTime);
                        ps[index].visit = true;//修改标志变量
                        over++;                       
                    }
                    index = (index + 1) % ps.Length;
                }
                else
                {
                    ALLTIME += ps[index].P_leaverunTime;
                    ps[index].P_turnoverTime = ALLTIME - ps[index].P_enterTime;
                    ps[index].P_endTime = ALLTIME;
                    Console.WriteLine("     " + ps[index].P_ID + "          " + ps[index].P_enterTime + "           " + ps[index].P_runTime + "        " + ps[index].P_endTime + "         " + ps[index].P_turnoverTime);
                    ps[index].visit = true;//修改标志变量
                    over++;
                }
                if (over == lenth)
                {
                    Console.WriteLine("所有进程已执行完毕");
                    break;
                }
            }
          
        }
    }
}


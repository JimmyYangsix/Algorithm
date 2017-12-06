using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPF
{
    class Program
    {
        static int TIME = 99999;//获取最小时间
        static int index = -1;//对应的进程
        static int ALLSTIME = 0;//开始时间
        static int ALLETIME = 0;//结束时间//进程权值
        static double maxquanzhi = 1;//最小权值
        static void Main(string[] args)
        {
            Console.WriteLine("FPF算法");
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
        //等待时间=上一个结束时间-进入时间
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
                if (ALLSTIME == 0)//当执行的第一个进程不是第0秒开始时，进行初赋值
                {
                    ps[index].P_quanzhi = 1;
                    if (ps[index].P_startTime != 0)
                    {
                        ALLSTIME = ps[index].P_enterTime;
                    }
                }
            }
            for (int k = 0; k < ps.Length; k++)
            {
                if (ps[k].P_quanzhi > maxquanzhi)
                {
                    index = k;
                    maxquanzhi = ps[k].P_quanzhi;
                }
            }
            ps[index].P_startTime = ALLSTIME;//每一个进程的开始时间为总开始时间
            ALLETIME = ALLSTIME + ps[index].P_runTime;//执行本次进程后的结束时间更新总最后时间
            ps[index].P_endTime = ALLETIME;//填入本进程的最后结束时间
            ps[index].P_turnoverTime = ps[index].P_endTime - ps[index].P_enterTime;//周转筛箕   
            Console.WriteLine("     " + ps[index].P_ID + "          " + ps[index].P_enterTime + "           " + ps[index].P_startTime + "           " + ps[index].P_runTime + "        " + ps[index].P_endTime + "         " + ps[index].P_turnoverTime);//结果输出
            ps[index].visit = true;//输出对应得进程后，修改状态
            ps[index].P_quanzhi = -2;//对应线程权值改为负值
            ALLSTIME = ALLETIME;
            for (int k = 0; k < ps.Length; k++)
            {
                if (k != index)//未执行得
                {
                    if(ps[k].P_enterTime<ALLETIME)
                    {
                    ps[k].P_waittime = ALLETIME - ps[k].P_enterTime;
                    }
                    else{
                        ps[k].P_waittime=0;
                    }
                }
                else//已经执行的
                {
                    ps[k].P_waittime = -2;
                }

            }
            for (int k = 0; k < ps.Length; k++)//权值计算
            {
                if (ps[k].P_quanzhi == -2)
                {
                    continue;
                }
                else
                {
                    ps[k].P_quanzhi = 1.0*(ps[k].P_waittime + ps[k].P_runTime) / ps[k].P_runTime;//权值计算
                    Console.WriteLine(ps[k].P_ID + "的权值为：" + ps[k].P_quanzhi);
                }
            }
            index = -1;

        }

    }
}


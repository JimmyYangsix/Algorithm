using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF
{
    class Program
    {
        public static int choice;//进行的操作
        public static int id;//记录要回收的id号
        public static int finalindex = 0;//记录当前要开始分配的地址起点
        static Memory loadmem = new Memory();//起始的内存
        static List<Memory> memlist = new List<Memory>();//声明Memory列表
        static List<Job> joblist = new List<Job>();//声明Job列表
         
        static void Main(string[] args)
        {
        Input1: Console.WriteLine("请输入内存最大空间，请输入整数");
            if (Int32.TryParse(Console.ReadLine(), out Memory.M_maxsize))
            {
                loadmem.M_size = Memory.M_maxsize;//起始内存大小初始化；
                loadmem.M_start = 0;//地址初始化
                memlist.Add(loadmem);
            Input2: Console.WriteLine("请选择操作");
                Console.WriteLine("1.增加作业");
                Console.WriteLine("2.回收作业");
                Console.WriteLine("3.展示内存使用情况");
                Console.WriteLine("4.退出");
                if (Int32.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1://添加作业至内存
                            {
                                Job addjob = new Job();
                                Give(addjob, loadmem);
                                goto Input2;
                            }
                        case 2://回收内存
                            {
                                Console.WriteLine("请输入要回收的作业号");
                                if (Int32.TryParse(Console.ReadLine(), out id))
                                {
                                    Return(id);
                                }
                                else
                                {
                                    Console.WriteLine("输入有误");
                                }
                                goto Input2;
                            }
                        case 3://展示内存使用情况
                            {
                                Show();
                                goto Input2;
                            }
                        case 4://退出程序
                            {
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("无效操作");
                                goto Input2;
                            }
                    }
                }
                else
                {
                    Console.WriteLine("操作输入有误，请重新输入");
                    goto Input2;
                }
            }
            else
            {
                Console.WriteLine("数值输入有误，请重新输入");
                goto Input1;
            }
        }
        public static void Give(Job job, Memory mem)//分配
        {
            Console.WriteLine("请输入作业号(整数)");
            if (Int32.TryParse(Console.ReadLine(), out job.J_id))
            {
                Console.WriteLine("请输入此作业的大小(整数)");
                if (Int32.TryParse(Console.ReadLine(), out job.J_size))
                {
                    if (job.J_size > Memory.M_maxsize)
                    {
                        Console.WriteLine("作业过大，分配失败");
                    }
                    else
                    {
                        int j = finalindex;//起点下标
                        for (int i = 0; i < memlist.Count; i++)
                        {
                            Console.WriteLine("this is index in:" + j + ":" + memlist.Count);
                            if (job.J_size <= memlist[j % memlist.Count].M_size)
                            {
                                job.J_start = memlist[j % memlist.Count].M_start;//进行作业起始地址赋值；
                                memlist[j % memlist.Count].M_start = job.J_start + job.J_size;//空闲起始重划分；
                                memlist[j % memlist.Count].M_size -= job.J_size;//空闲地址长度减小；
                                joblist.Add(job);//添加作业
                                Console.WriteLine("分配成功");
                                j++;
                                finalindex = j;
                                return;
                            }
                            j++;
                        }
                        Console.WriteLine("剩余空间不足，请进行回收或减小作业大小操作");
                        return;
                    }

                }
                else
                {
                    Console.WriteLine("分配失败");
                    return;
                }
            }
            else
            {
                Console.WriteLine("创建失败");
                return;
            }
        }
        public static void Return(int job_id)//回收
        {
            foreach (Job jb in joblist)
            {
                if (jb.J_id == job_id)
                {
                    Memory freemem = new Memory();//创建回收的内存
                    freemem.M_size = jb.J_size;//大小进行赋值
                    freemem.M_start = jb.J_start;//开始地址进行赋值
                    joblist.Remove(jb);//作业列表进行移除
                    memlist.Add(freemem);//进行回收的添加
                    memlist.Sort(delegate(Memory x, Memory y)//按照内存地址进行排序
                    {
                        int a = y.M_start.CompareTo(x.M_start);
                        return -a;
                    });
                    Console.WriteLine("回收成功");
                    return;
                }
            }
            Console.WriteLine("回收失败");
            return;
        }
        public static void Show()//展示剩余内存情况
        {
            int[] jobadds = new int[joblist.Count];
            int[] memadds = new int[memlist.Count];
            int up = 0;//起到C语言指针作用；对job数组有效
            int down = 0;//起到C语言指针作用；对mem数组有效
            for (int i = 0; i < joblist.Count; i++)//进行开始地址赋值数组
            {
                jobadds[i] = joblist[i].J_start;
            }
            for (int i = 0; i < memlist.Count; i++)
            {
                memadds[i] = memlist[i].M_start;
            }
            Console.WriteLine("\t\t\tNF内存分配算法结果如下：");
            Console.WriteLine("作业号\t\t大小\t\t\t地址范围\t\t状态");
            for (int j = 0; j < joblist.Count + memlist.Count; j++)
            {
                if (up < jobadds.Length && down < memadds.Length)
                {
                    if (jobadds[up] < memadds[down])
                    {
                        Console.WriteLine(joblist[up].J_id + "\t\t" + joblist[up].J_size + "\t\t\t" + joblist[up].J_start + "M~" + (joblist[up].J_start + joblist[up].J_size) + "M\t\t\t" + "busy");
                        up++;
                    }
                    else if (jobadds[up] > memadds[down])
                    {
                        Console.WriteLine("内存\t\t" + memlist[down].M_size + "\t\t\t" + memlist[down].M_start + "M~" + (memlist[down].M_start + memlist[down].M_size) + "M\t\t" + "free");
                        down++;
                    }
                }
                else if (up > jobadds.Length)
                {
                    Console.WriteLine(joblist[up].J_id + "\t\t" + joblist[up].J_size + "\t\t\t" + joblist[up].J_start + "M~" + (joblist[up].J_start + joblist[up].J_size) + "M\t\t\t " + "busy");
                    up++;
                }
                else
                {
                    Console.WriteLine("内存\t\t" + memlist[down].M_size + "\t\t\t" + memlist[down].M_start + "M~" + (memlist[down].M_start + memlist[down].M_size) + "M\t\t" + "free");
                    down++;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RR
{
    class Process
    {
        public int P_ID;//进程ID
        public int P_enterTime;//进入时间
        public int P_endTime = -1;//结束时间
        public int P_runTime;//运行时间
        public int P_turnoverTime = -1;//周转时间
        public int P_avgturnoverTime = -1;//带权周转时间
        public bool visit = false;//判断是否运行
        public int P_leaverunTime;//剩余执行时间
    }
}

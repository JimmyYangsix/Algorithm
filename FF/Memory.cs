using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF
{
    public class Memory
    {
        public bool M_visit;//标志是否回收
        public static int M_maxsize;//最大上限
        public int M_size;//现有内存大小
        public int M_start;//开始地址
    }
}

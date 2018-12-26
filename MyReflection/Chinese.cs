using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyReflection
{
    public class Chinese : BaseModel, IInterface
    {
        public delegate void ChineseEventHandle();
        public event ChineseEventHandle ChineseEvent;

        public Chinese()
        {
            Console.WriteLine($"调用了{typeof(Chinese).Name}的无参数构造函数。");
        }
        public Chinese(string name, string qq)
        {
            base.Name = name;
            this.QQ = qq;
            Console.WriteLine($"调用了{typeof(Chinese).Name}的两个参数构造函数。");
        }

        public string QQ { get; set; }
        public string WX { get; set; }

        public int Age;

        public void Eat()
        {
            Console.WriteLine("吃饭ing...");
        }

        public void SayHello()
        {
            Console.WriteLine("你好。");
        }

        public void SayHello(string name)
        {
            Console.WriteLine($"{name}你好。");
        }

        public void SayAny(string str)
        {
            Console.WriteLine(str);
        }

        public void ShowObject<T>(T t)
        {
            Console.WriteLine($"T的类型为：{t.GetType()},值为{t}");
        }

        public void Sleep()
        {
            Console.WriteLine("睡觉ing...");
        }

        public void PlayDouDou()
        {
            Console.WriteLine("打豆豆ing...");
        }

        public void InvokeEvent()
        {
            if (ChineseEvent != null)
                ChineseEvent.Invoke();
        }


    }
}

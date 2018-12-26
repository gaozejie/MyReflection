using MyReflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {

            // 长格式名称加载 注意此处不需要后缀
            Assembly assembly = Assembly.Load("MyReflection");

            // 绝对路径加载 
            Assembly assembly1 = Assembly.LoadFrom(@"E:\project\Reflection\MyReflection\bin\Debug\MyReflection.dll");
            // 如果要加载的程序集跟当前执行程序集处于同一路径，则可省略前面的路径
            Assembly assembly2 = Assembly.LoadFrom(@"MyReflection.dll");
            // 通过URL地址加载
            Assembly assembly3 = Assembly.LoadFrom(@"http://www.test.com/MyReflection.dll");

            // 必须传入绝对路径
            Assembly assembly4 = Assembly.LoadFile(@"MyReflection.dll");


            // 获取所有类型
            //Type[] types = assembly.GetTypes();
            Type type = assembly.GetType("MyReflection.Chinese");

            //foreach (var type in assembly.GetTypes())
            //{
            Console.WriteLine($"父类：{type.BaseType}");

            // 获取实现或集成的所有接口
            foreach (Type inter in type.GetInterfaces())
            {
                Console.WriteLine(inter.Name);
            }

            // 获取所有构造函数
            foreach (ConstructorInfo ctor in type.GetConstructors())
            {
                Console.WriteLine(ctor.Name);
                foreach (ParameterInfo parameter in ctor.GetParameters())
                {
                    Console.WriteLine($"参数类型：{ parameter.ParameterType},名称：{parameter.Name},位置：{parameter.Position}");
                }
            }

            var obj = Activator.CreateInstance(type);

            // 获取所有字段
            foreach (FieldInfo field in type.GetFields())
            {
                Console.WriteLine($"字段类型：{ field.FieldType},名称：{field.Name}");
            }

            // 通过名称获取字段
            FieldInfo fieldAge = type.GetField("Age");
            fieldAge.SetValue(obj, 23);
            var fieldVal = fieldAge.GetValue(obj);

            // 获取所有属性
            foreach (PropertyInfo prop in type.GetProperties())
            {
                Console.WriteLine($"属性类型：{ prop.PropertyType},名称：{prop.Name}");
            }

            // 通过名称获取属性
            PropertyInfo propWX = type.GetProperty("WX");
            propWX.SetValue(obj, "wx111223");
            var propVal = propWX.GetValue(obj);

            // 获取所有方法
            foreach (MethodInfo method in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public))
            {
                Console.WriteLine(method.Name);
                // method.IsGenericMethod // 是否是泛型方法
                // method.ContainsGenericParameters // 是否包含有未分配的泛型参数

                foreach (ParameterInfo parameter in method.GetParameters())
                {
                    //parameter.GetType().GetGenericParameterConstraints // 获取泛型参数约束
                    Console.WriteLine($"参数类型：{ parameter.ParameterType},名称：{parameter.Name},位置：{parameter.Position}");
                }
            }

            // 普通方法
            MethodInfo methodEat = type.GetMethod("Eat");
            methodEat.Invoke(obj, null);

            // 有参数方法
            MethodInfo methodSayAny = type.GetMethod("SayAny");
            methodSayAny.Invoke(obj, new object[] { "啥都可以说。" });

            // 重载方法-无参数
            MethodInfo methodSayHello = type.GetMethod("SayHello", new Type[] { });
            methodSayHello.Invoke(obj, null);

            // 重载方法-有参数
            MethodInfo methodSayHello1 = type.GetMethod("SayHello", new Type[] { typeof(string) });
            methodSayHello1.Invoke(obj, new object[] { "张三" });

            // 泛型方法
            MethodInfo methodShowObject = type.GetMethod("ShowObject");
            bool isGenericMethod = methodShowObject.IsGenericMethod;
            bool containsGenericParameters = methodShowObject.ContainsGenericParameters;
            MethodInfo genericMethod = methodShowObject.MakeGenericMethod(new Type[] { typeof(int) });
            genericMethod.Invoke(obj, new object[] { 23 });


            //foreach (EventInfo eventInfo in type.GetEvents())
            //{
            //    eventInfo.AddEventHandler(null, new Action(new Chinese().SayHello));
            //    Console.WriteLine(eventInfo.Name);
            //}



            // 获取事件
            EventInfo eventInfo = type.GetEvent("ChineseEvent");
            // 获取处理事件的委托类型
            Type delegateType = eventInfo.EventHandlerType;
            //Console.WriteLine(delegateType.GetMethod("Invoke").ToString());  // 查看委托签名

            // 获取要添加的方法
            MethodInfo methodInfo = type.GetMethod("Sleep", BindingFlags.Public | BindingFlags.Instance);     //, BindingFlags.Public | BindingFlags.Instance
            //Console.WriteLine(methodInfo.ToString());

            // 创建委托
            Delegate d = Delegate.CreateDelegate(delegateType, obj, methodInfo);
            // 将委托实例添加到事件     方法1
            eventInfo.AddEventHandler(obj, d);


            // 获取调用事件方法
            MethodInfo invokeMethod = type.GetMethod("InvokeEvent");
            invokeMethod.Invoke(obj, null);

            //}


            Console.Read();
        }
    }
}

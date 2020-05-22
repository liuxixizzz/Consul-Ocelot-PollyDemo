using Polly;
using System;
using System.Reflection;
using System.Threading;

namespace PollyTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //FallbackDemo();
            //var str = FallbackStringDemo();
            //Console.WriteLine(str);
            //RetryForeverDemo();
            //CircuitBreakerDemo();
            Console.ReadKey();
        }

        /// <summary>
        /// 不带返回值的降级
        /// </summary>
        static void FallbackDemo()
        {
            Console.WriteLine("Hello World!");
            Policy policy = Policy.Handle<ArgumentException>() //故障
                .Fallback(() => Console.WriteLine("降级了！！！"), ex => { Console.WriteLine(ex); });//动作

            policy.Execute(() =>//在策略中执行业务代码
            {
                Console.WriteLine("开始任务");
                throw new ArgumentException("Hello world!");
                Console.WriteLine("完成任务");
            });
        }

        /// <summary>
        /// 带返回值的降级
        /// </summary>
        /// <returns></returns>
        static string FallbackStringDemo()
        {
            Console.WriteLine("Hello World!");
            Policy<string> policy = Policy<string>.Handle<ArgumentException>() //故障
                .Fallback(() => //动作
                {
                    Console.WriteLine("降级了！！！");
                    return "降级返回值";
                }, ex =>
                {
                    Console.WriteLine(ex);
                });

            string value = policy.Execute(() =>//在策略中执行业务代码
            {
                Console.WriteLine("开始任务");
                throw new ArgumentException("Hello world!");
                Console.WriteLine("完成任务");
                return "完成任务";
            });
            return value;
        }

        /// <summary>
        /// 重试
        /// </summary>
        static void RetryForeverDemo()
        {
            //Retry()是重试最多一次；
            //Retry(n) 是重试最多n次；
            //WaitAndRetry()可以实现“如果出错等待100ms再试还不行再等150ms秒。。。。”
            Policy policy = Policy.Handle<ArgumentException>()
                    //.RetryForever();
                    .WaitAndRetry(10, i => TimeSpan.FromMilliseconds(100));

            try
            {
                policy.Execute(() =>
                {
                    if (DateTime.Now.Second % 10 == 0)
                    {
                        Console.WriteLine("完成任务");
                    }
                    else
                    {
                        Console.WriteLine("出错了");
                        throw new ArgumentException("出错了！");
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("重试结束");
            }
        }

        /// <summary>
        /// 熔断
        /// </summary>
        static void CircuitBreakerDemo()
        {
            //Retry()是重试最多一次；
            //Retry(n) 是重试最多n次；
            //WaitAndRetry()可以实现“如果出错等待100ms再试还不行再等150ms秒。。。。”
            Policy policy = Policy.Handle<ArgumentException>()
                    //.RetryForever();
                    .CircuitBreaker(6, TimeSpan.FromSeconds(5));
            while (true)
            {
                try
                {
                    policy.Execute(() =>
                    {
                        if (DateTime.Now.Second % 10 == 0)
                        {
                            Console.WriteLine("完成任务");
                        }
                        else
                        {
                            Console.WriteLine("出错了");
                            throw new ArgumentException("出错了！");
                        }
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("熔断5秒");
                }
                Thread.Sleep(500);
            }
        }
    }
}

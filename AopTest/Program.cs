using AspectCore.DynamicProxy;
using System;

namespace AopTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
            using (IProxyGenerator proxyGenerator = proxyGeneratorBuilder.Build())
            {
                Person person = proxyGenerator.CreateClassProxy<Person>();
                person.Say("say!");
            }
        }
    }
}

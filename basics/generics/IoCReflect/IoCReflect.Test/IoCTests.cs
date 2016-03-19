using System.Runtime.Remoting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IoCReflect.Test
{
    [TestClass]
    public class IocTests
    {
        [TestMethod]
        public void Can_Resolve_Types()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();

            var logger = ioc.Resolve<ILogger>();

            Assert.AreEqual(typeof(SqlServerLogger), logger.GetType());
        }

        [TestMethod]
        public void Can_Resolve_Types_Without_Default_Constructor()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();
            ioc.For<IRepository<Employee>>().Use<SqlRepository<Employee>>();

            var repository = ioc.Resolve<IRepository<Employee>>();

            Assert.AreEqual(typeof(SqlRepository<Employee>), repository.GetType());
        }

        [TestMethod]
        public void Can_Resolve_Types_Concrete_Type()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();
            //ioc.For<IRepository<Employee>>().Use<SqlRepository<Employee>>();
            ioc.For(typeof (IRepository<>)).Use(typeof (SqlRepository<>));

            var service = ioc.Resolve<ProductionService>();

            Assert.IsNotNull(service);
        }
    }
}

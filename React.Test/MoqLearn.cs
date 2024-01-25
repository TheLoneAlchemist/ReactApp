using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
namespace React.Test
{

    public interface ICalculator
    {
        int Add(int a, int b);
        int Div(int a, int b);
    }

    public delegate int AddDel(int a, int b);
    public class Calculator : ICalculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Div(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException();
            }
            return a / b;
        }
    }

    public class Test
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 20)]
        public void Test25(int l, int m)
        {
            var calmock = new Mock<ICalculator>();

            calmock.Setup(z => z.Add(It.IsAny<int>(), It.IsAny<int>())).Returns((int a, int b) => a + b);

            var calservice = calmock.Object;

            int result = calservice.Add(l, m);

            Assert.Equal(l + m, result);

        }
        [Theory]
        [InlineData(0)]
        [InlineData(0.1)]
        public void Test26(int l)
        {
            var calmock = new Mock<ICalculator>();
            calmock.Setup(x=>x.Div(It.IsAny<int>(),0)).Returns((int a, int b)=> a / b);

            var calservice = calmock.Object;

            Assert.Throws<DivideByZeroException>(()=>calservice.Div(10,l));
        }
    }

    


}
*/
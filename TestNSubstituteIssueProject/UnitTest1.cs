using System;
using NSubstitute;
using Xunit;

namespace TestNSubstituteIssueProject
{
    public class UnitTest1
    {
        private IFoo getMockFoo()
        {
            int instanceIDPool = 1;
            IFoo foo = Substitute.For<IFoo>();
            foo.AddBar(Arg.Is<BarInfoObj>(x => x.ServerName.Contains("SuccessAddNewBar"))).Returns(ci =>
            {
                BarInfoObj barInfoObjToReturn = ci.ArgAt<BarInfoObj>(0).ShallowCopy();
                barInfoObjToReturn.BarInstanceId = instanceIDPool++;
                barInfoObjToReturn.Status = BarStatus.Idle;
                barInfoObjToReturn.StatusMessage = "Newly registered Bar";
                barInfoObjToReturn.StatusReportDateTimeUTC = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
                string msg = $"Added Bar at {barInfoObjToReturn.BarUrl} with instance ID {barInfoObjToReturn.BarInstanceId.ToString()} to the Foo's pool of Bars.";
                return Response<BarInfoObj>.Success(barInfoObjToReturn, msg);
            });
            //Why is this throwing an object null exception when used with the one above but when it's used on its own it's not?  We do this sort of thing in another test.
            foo.AddBar(Arg.Is<BarInfoObj>(x => x.ServerName.Contains("SuccessAddExistingBar"))).Returns(ci =>
            {
                BarInfoObj barInfoObjToReturn = ci.ArgAt<BarInfoObj>(0).ShallowCopy();
                barInfoObjToReturn.BarInstanceId = instanceIDPool++;
                barInfoObjToReturn.Status = BarStatus.Idle;
                barInfoObjToReturn.StatusMessage = "Re-registered Bar";
                barInfoObjToReturn.StatusReportDateTimeUTC = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
                string msg = $"Re-registered Bar at {barInfoObjToReturn.BarUrl} with instance ID {barInfoObjToReturn.BarInstanceId.ToString()}.";
                return Response<BarInfoObj>.Success(barInfoObjToReturn, msg);
            });

            return foo;
        }

        [Fact]
        public void Test1()
        {
            //Arrange
            IFoo testFoo = getMockFoo();
            BarInfoObj testInputBar = new BarInfoObj()
            {
                ServerName = "SuccessAddNewBar",
                BarUrl = "http://some.test.url",
                BarInstanceId = 1,
                Status = BarStatus.Idle,
                StatusMessage = "TestInput",
                StatusReportDateTimeUTC = DateTime.Now
            };

            //Act
            Response<BarInfoObj> testResponse = testFoo.AddBar(testInputBar);

            //Assert
            testFoo.ReceivedCalls();
        }
    }
}

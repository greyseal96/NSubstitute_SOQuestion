using System;

namespace TestNSubstituteIssueProject
{
    public interface IFoo
    {
        Response<BarInfoObj> AddBar(BarInfoObj objToAdd);
    }

    public class BarInfoObj
    {
        public string ServerName { get; set; }
        public string BarUrl { get; set; }
        public int BarInstanceId { get; set; }
        public BarStatus Status { get; set; }
        public string StatusMessage { get; set; }
        public DateTime StatusReportDateTimeUTC { get; set; }

        public BarInfoObj ShallowCopy()
        {
            return (BarInfoObj)this.MemberwiseClone();
        }
    }

    public enum BarStatus
    {
        Idle,
        Active,
        Unavailable
    }

    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public static Response<T> Success(T dataObj, string message)
        {
            return new Response<T>() { Data = dataObj, IsSuccess = true, Message = message };
        }
    }
}
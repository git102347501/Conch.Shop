using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace Conch.Shared
{
    public class BaseResult
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }

        public BaseResult()
        {

        }


        public BaseResult IsSuccess(string message = "获取成功")
        {
            this.Code = 200;
            this.Success = true;
            this.Message = message;
            return this;
        }

        public BaseResult IsError(string message = "操作失败")
        {
            this.Code = 500;
            this.Success = false;
            this.Message = message;
            return this;
        }
    }

    public class BaseResult<T> : BaseResult
    {
        public T Data { get; set; }

        public BaseResult()
        {
                
        }

        public BaseResult(string message, bool success = true, int code = 200)
        {
            this.Code = code;
            this.Message = message;
            this.Success = success;
        }

        public BaseResult(T data)
        {
            this.Code = 200;
            this.Message = "获取成功";
            this.Success = true;
            this.Data = data;
        }

        public BaseResult<T> IsSuccess(string message, T data)
        {
            this.Data = data;
            this.Code = 200;
            this.Success = true;
            this.Message = message;
            return this;
        }

        public new BaseResult<T> IsError(string message = "操作失败")
        {
            this.Code = 500;
            this.Success = false;
            this.Message = message;
            return this;
        }
    }
}

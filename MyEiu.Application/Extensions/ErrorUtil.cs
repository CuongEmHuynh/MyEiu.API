using MyEiu.Application.Const;
using MyEiu.Utilities.Dtos;


namespace MyEiu.Application.Extensions
{
    public static class ErrorUtil
    {
        public static  OperationResult GetMessageError(this Exception ex)
        {
            string message = ex.Message;
            if (ex.InnerException != null)
            {
                message += " \n " + ex.InnerException.Message;
            }

            return new OperationResult { StatusCode = StatusCode.InternalServerError, Message = message, Success = false };
            

        }
    }
}

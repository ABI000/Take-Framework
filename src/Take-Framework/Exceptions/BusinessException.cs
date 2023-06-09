﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeFramework.Exceptions
{
    /// <summary>
    /// 业务异常
    /// 用于业务处理发生的异常处理，通常为计算错误，业务逻辑验证不通过使用
    /// </summary>
    public class BusinessException : Exception
    {
        public string Code { get; set; }

        public override string Message { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        public BusinessException(string message, string code = "BusinessError")
        {
            this.Message = message;
            this.Code = code;
        }
    }
}

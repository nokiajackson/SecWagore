using SecWagore.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecWagore.Helpers
{
    public class Result<T> : IResult<T> where T : class
    {

        /// <summary> Constructor Success=false </summary>
        public Result() : this(false) { }


        /// <summary> Constructor </summary>
        /// <param name="success"> success </param>
        public Result(bool success)
        {
            //ID = Guid.NewGuid();
            ID = Guid.NewGuid(); // Changed to Guid
            Success = success;
            Data = null;
            Total = 0;
            ErrorCode = 0;
            VaildationData = new List<VaildationResult>();
            Message = string.Empty;
            ExceptionMessage = string.Empty; // Changed from ExMessage
        }

        /// <summary> Constructor with error message. </summary>
        /// <param name="message"> Error Message </param>
        public Result(string message)
        {
            Message = message;
        }



        /// <summary> 操作的資料 </summary>
        public T Data { get; set; }
        public int Total { get; set; }

        /// <summary> 驗證的資料 </summary>
        public List<VaildationResult> VaildationData { get; set; }

        /// <summary> Error code </summary>
        public int ErrorCode { get; set; }

        #region IResult Members

        /// <summary> Unique identifier </summary>
        public Guid ID { get; set; } // Changed from dynamic

        /// <summary> Operation message </summary>
        public string Message { get; set; }

        /// <summary> Exception message </summary>
        public string ExceptionMessage { get; set; } // Changed from ExMessage

        /// <summary> Operation success status </summary>
        public bool Success { get; set; }

        dynamic IResult<T>.ID => throw new NotImplementedException();

        #endregion

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
    /// <summary> 驗證資訊 </summary>
    public class VaildationResult
    {
        /// <summary> 欄位名稱 </summary>
        public string FieldName { get; set; }

        /// <summary> 驗證失敗原因 </summary>
        public string ErrorMessage { get; set; }
    }
}


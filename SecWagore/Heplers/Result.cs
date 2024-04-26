using SecWagore.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecWagore.Heplers
{

    public class Result<T> : IResult<T> where T : class
    {

        /// <summary> Constructor Success=false </summary>
        public Result()
            : this(false)
        {
        }

        /// <summary> Constructor </summary>
        /// <param name="success"> success </param>
        public Result(bool success)
        {
            //ID = Guid.NewGuid();
            ID = 0;
            Success = success;
            Data = null;
            Total = 0;
            ErrorCode = 0;
        }

        /// <summary> Constructor with error message. </summary>
        /// <param name="message"> Error Message </param>
        public Result(string message)
        {
            //ID = Guid.NewGuid();
            ID = 0;
            Success = false;
            Message = message;
        }



        /// <summary> 操作的資料 </summary>
        public T Data
        {
            get;
            set;
        }

        public int Total { get; set; }

        /// <summary> 驗證的資料 </summary>
        public List<VaildationResult> VaildationData
        {
            get;
            set;
        }

        /// <summary> 錯誤代碼 </summary>
        public int ErrorCode { get; set; }

        #region IResult 成員

        /// <summary> 唯一編碼 </summary>
        public dynamic ID
        {
            get;
            set;
            //private set;
        }

        /// <summary> 操作失敗訊息 </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary> 操作例外訊息 </summary>
        public string ExMessage
        {
            get;
            set;
        }

        /// <summary> 操作狀態 </summary>
        public bool Success
        {
            get;
            set;
        }

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

        #endregion IResult 成員
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


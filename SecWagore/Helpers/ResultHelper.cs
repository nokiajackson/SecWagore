using SecWagore.Helpers;
using System.Collections.Generic;

namespace SecWagore.Helpers
{
    public static class ResultHelper
    {
        /// <summary> 取得成功的 Result </summary>
        /// <typeparam name="T"> 回傳資料類別 </typeparam>
        /// <param name="data"> 回傳的資料 </param>
        /// <param name="code"> 操作動作 </param>
        /// <returns> </returns>
        public static Result<T> Success<T>(T data, StatusCode code, string message = "Success", int total = 0) where T : class
        {
            return new Result<T>(true)
            {
                Data = data,
                Total = total,
                Message = GetMessage(true, code),
                ErrorCode = 0 // 默认成功时没有错误码
            };
        }

        /// <summary> 取得失敗的 Result </summary>
        /// <typeparam name="T"> 回傳資料類別 </typeparam>
        /// <param name="exMessage"> 例外訊息 </param>
        /// <param name="code"> 操作動作 </param>
        /// <returns> </returns>
        public static Result<T> Failure<T>(string message, StatusCode code, int errorCode = 500) where T : class
        {
            return new Result<T>(false)
            {
                Message = GetMessage(false, code),
                ErrorCode = errorCode
                //VaildationData = new List<VaildationResult>()
            };
        }

        #region 操作動作回傳訊息控制

        /// <summary> 操作動作代碼 </summary>
        public enum StatusCode
        {
            /// <summary> Save </summary>
            Save = 0,

            /// <summary> Delete </summary>
            Delete = 1,

            /// <summary> Validation </summary>
            Validation = 2,

            /// <summary> Import </summary>
            Import = 3,

            /// <summary> Export </summary>
            Export = 4,

            /// <summary> Send </summary>
            Send = 5,

            /// <summary> BackUp </summary>
            BackUp = 6,

            /// <summary> Compare </summary>
            Compare = 7,

            /// <summary> Get </summary>
            Get = 8,

            /// <summary> Apply </summary>
            Apply = 9
        }

        /// <summary> 取得訊息 </summary>
        /// <param name="isSuccess"> 是否成功 </param>
        /// <param name="code"> 操作動作 </param>
        /// <returns> 訊息 </returns>
        private static string GetMessage(bool isSuccess, StatusCode code)
        {
            return code switch
            {
                StatusCode.Save => isSuccess ? "儲存成功" : "儲存失敗",
                StatusCode.Delete => isSuccess ? "刪除成功" : "刪除失敗",
                StatusCode.Validation => isSuccess ? "驗證成功" : "驗證失敗",
                StatusCode.Import => isSuccess ? "匯入成功" : "匯入失敗",
                StatusCode.Export => isSuccess ? "匯出成功" : "匯出失敗",
                StatusCode.Send => isSuccess ? "發送成功" : "發送失敗",
                StatusCode.BackUp => isSuccess ? "備份成功" : "備份失敗",
                StatusCode.Compare => isSuccess ? "比對成功" : "比對失敗",
                StatusCode.Get => isSuccess ? "資料取得成功" : "資料取得失敗",
                StatusCode.Apply => isSuccess ? "申請成功" : "申請失敗",
                _ => string.Empty,
            };
        }

        #endregion 操作動作回傳訊息控制
    }
}
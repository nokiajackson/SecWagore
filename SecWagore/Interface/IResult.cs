namespace SecWagore.Interface
{
    /// <summary> 所有的資料異動，包括新增修改刪除等，都必須回傳此類別 </summary>
    /// <typeparam name="T"> </typeparam>
    public interface IResult<T> where T : class
    {
        /// <summary> 操作的資料 </summary>
        T Data { get; set; }

        /// <summary> 唯一編碼 </summary>
        dynamic ID { get; }

        /// <summary> 操作失敗訊息 </summary>
        string Message { get; set; }

        /// <summary> 操作狀態 </summary>
        bool Success { get; set; }
    }
}
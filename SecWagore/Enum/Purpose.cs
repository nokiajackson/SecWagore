using System.ComponentModel;

namespace SecWagore
{
    public enum Purpose
    {
        [Description("洽公")]
        洽公 = 1,
        [Description("送貨")]
        送貨 = 2,
        [Description("活動")]
        活動 = 3,
        [Description("維護")]
        維護 = 4,
        [Description("志工")]
        志工 = 5,
        [Description("面試")]
        面試 = 6,
        [Description("其他")]
        其他 = 99
    }
}
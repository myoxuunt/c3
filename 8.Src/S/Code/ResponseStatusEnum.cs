using Xdgk.Common;

namespace S
{
    public enum ResponseStatusEnum : byte
    {
        [EnumText("成功")]
        Success = 0,

        [EnumText("名称不存在")]
        NotExistName = 1,

        [EnumText("没有新数据")]
        NotNewDatas = 2,
    }
}

using Xdgk.Common;

namespace S
{
    public enum ResponseStatusEnum : byte
    {
        [EnumText("�ɹ�")]
        Success = 0,

        [EnumText("���Ʋ�����")]
        NotExistName = 1,

        [EnumText("û��������")]
        NotNewDatas = 2,
    }
}

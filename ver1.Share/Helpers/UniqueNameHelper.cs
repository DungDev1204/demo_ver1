using System.Text;


namespace demo_ver1.Share.Helpers;
public static class UniqueNameHelper
{
    private static readonly Dictionary<char, string> MapAsciiUnicode = new()
    {
        { 'a', "aàáãạảăắằẳẵặâấầẩẫậAÀÁÃẠẢĂẮẰẲẴẶÂẤẦẨẪẬ" },
        { 'o', "oòóõọỏôốồổỗộơớờởỡợUÒÓÕỌỎÔỐỒỔỖỘƠỚỜỞỠỢ" },
        { 'u', "uùúũụủưứừửữựUÙÚŨỤỦƯỨỪỬỮỰ" },
        { 'e', "eèéẹẻẽêềếểễệEÈÉẸẺẼÊỀẾỂỄỆ" },
        { 'i', "iìíĩỉịÌÍĨỈỊ" },
        { 'y', "yỳỵỷỹýYỲỴỶỸÝ" },
        { 'd', "đĐ" }
    };

    public static string ConvertToAsciiName(string name)
    {
        if (string.IsNullOrEmpty(name)) return "";
        var lowName = name.Replace(' ', '-');

        StringBuilder sb = new();
        for (int i = 0; i < lowName.Length; i++)
        {
            if (lowName[i] == '-' || (name[i] >= '0' && name[i] <= '9') || (name[i] >= 'a' && name[i] <= 'z'))
            {
                sb.Append(lowName[i]);
            }
            else if (name[i] >= 'A' && name[i] <= 'Z')
            {
                sb.Append((char)(lowName[i] + 32));
            }
            else
            {
                foreach (var map in MapAsciiUnicode)
                {
                    if (map.Value.Contains(lowName[i]))
                    {
                        sb.Append(map.Key);
                    }
                }
            }
        }
        while (sb.ToString().Contains("--"))
        {
            sb.Replace("--", "-");
        }
        return sb.ToString();
    }
}

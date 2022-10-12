using System.Collections.Generic;

namespace Xas.Otp.Console
{
    public class OtpSettings
    {
        public IEnumerable<OtpSetting> Settings { get; set; }
    }

    public class OtpSetting
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public int? Length { get; set; }
        public int? Duration { get; set; }
    }
}

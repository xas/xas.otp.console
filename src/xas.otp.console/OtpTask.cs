using OtpNet;
using Spectre.Console;

namespace Xas.Otp.Console
{
    public class OtpTask
    {
        private Totp _totp;
        private ProgressTask _task;
        private string _name;
        private int _duration;

        public OtpTask(OtpSetting setting)
        {
            _duration = setting.Duration ?? 30;
            _totp = new Totp(secretKey: Base32Encoding.ToBytes(setting.Key),
                            step: _duration,
                            totpSize: setting.Length ?? 6);
            _name = setting.Name;
        }

        public void Initialize(ProgressContext ctx)
        {
            _task = ctx.AddTask($"[green]{this}[/]", true, _duration);
            _task.Value = _duration - _totp.RemainingSeconds();
        }

        public string GetCurrentCode()
        {
            return _totp?.ComputeTotp();
        }

        public int GetRemainingTime()
        {
            return _totp?.RemainingSeconds() ?? 0;
        }

        public void Increment()
        {
            if (_task == null)
            {
                return;
            }
            _task.Increment(1);
            if (_task.IsFinished)
            {
                Reset();
            }
        }

        public void Reset()
        {
            if (_task != null)
            {
                _task.Value = _duration - _totp.RemainingSeconds();
                _task.Description = ToString();
            }
        }

        public override string ToString()
        {
            return $"[green]{_name,15} - {GetCurrentCode(),8}[/]";
        }
    }
}

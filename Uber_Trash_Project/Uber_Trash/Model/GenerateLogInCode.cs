namespace Uber_Trash.Model
{
    public static class GenerateLogInCode
    {
        public static string Code { get; set; } = "123456789";
        public static string GenerateCode()
        {
            Random random = new Random();
            Code = new string(Enumerable.Range(0, 4).Select(_ => Code[random.Next(Code.Length)]).ToArray());
            return Code;
        }
    }
}

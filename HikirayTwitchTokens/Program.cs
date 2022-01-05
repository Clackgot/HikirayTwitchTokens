using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HikirayTwitchTokens
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetToken().GetAwaiter().GetResult());
        }

        private static async Task<string> GetToken()
        {
            var text = await File.ReadAllLinesAsync("2.txt");//Содержимое файла cookies
            var tokenString = text.FirstOrDefault(x => x.Contains(".twitch.tv") && x.Contains("auth-token"));//Поиск подходящей строки
            if(!(tokenString is null))
            {
                //string token = new string(tokenString.TakeLast(30).ToArray());//Получаем последние 30 символов строки
                string token = tokenString.Split().Last();//Получение токена
                return token;
            }
            else
            {
                return null;
            }
        }
    }
}

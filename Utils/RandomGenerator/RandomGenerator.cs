using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.RandomGenerator
{
    public class RandomGenerator : IRandomGenerator
    {
        private static readonly Random _random = new();
        private static readonly string _idCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly int _idLenght = 10;
        private static readonly string _codeCharacters = "0123456789";
        private static readonly int _codeLength = 6;

        public string GenerateId()
        {
            StringBuilder sb = new();
            for (int i = 0; i < _idLenght; i++)
            {
                int index = _random.Next(_idCharacters.Length);
                sb.Append(_idCharacters[index]);
            }
            return sb.ToString();
        }
        public string GenerateCode()
        {
            StringBuilder sb = new();
            for (int i = 0; i < _codeLength; i++)
            {
                int index = _random.Next(_codeCharacters.Length);
                sb.Append(_codeCharacters[index]);
            }
            return sb.ToString();
        }
    }
}

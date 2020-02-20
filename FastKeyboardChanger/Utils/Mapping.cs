using System.Collections.Generic;
using System.Text;

namespace FastKeyboardChanger.Utils
{
    public static class Mapping
    {
        private static Dictionary<char, char> EnRuVocabulary = new Dictionary<char, char>()
        {
            { 'Q', 'Й' },
            { 'W', 'Ц' },
            { 'E', 'У' },
            { 'R', 'К' },
            { 'T', 'Е' },
            { 'Y', 'Н' },
            { 'U', 'Г' },
            { 'I', 'Ш' },
            { 'O', 'Щ' },
            { 'P', 'З' },
            { '{', 'Х' },
            { '}', 'Ъ' },
            { 'A', 'Ф' },
            { 'S', 'Ы' },
            { 'D', 'В' },
            { 'F', 'А' },
            { 'G', 'П' },
            { 'H', 'Р' },
            { 'J', 'О' },
            { 'K', 'Л' },
            { 'L', 'Д' },
            { ':', 'Ж' },
            { '"', 'Э' },
            { '~', 'Ё' },
            { 'Z', 'Я' },
            { 'X', 'Ч' },
            { 'C', 'С' },
            { 'V', 'М' },
            { 'B', 'И' },
            { 'N', 'Т' },
            { 'M', 'Ь' },
            { '<', 'Б' },
            { '>', 'Ю' },
            { '?', ',' },
            { 'q', 'й' },
            { 'w', 'ц' },
            { 'e', 'у' },
            { 'r', 'к' },
            { 't', 'е' },
            { 'y', 'н' },
            { 'u', 'г' },
            { 'i', 'ш' },
            { 'o', 'щ' },
            { 'p', 'з' },
            { '[', 'х' },
            { ']', 'ъ' },
            { 'a', 'ф' },
            { 's', 'ы' },
            { 'd', 'в' },
            { 'f', 'а' },
            { 'g', 'п' },
            { 'h', 'р' },
            { 'j', 'о' },
            { 'k', 'л' },
            { 'l', 'д' },
            { ';', 'ж' },
            { '\'', 'э' },
            { '`', 'ё' },
            { 'z', 'я' },
            { 'x', 'ч' },
            { 'c', 'с' },
            { 'v', 'м' },
            { 'b', 'и' },
            { 'n', 'т' },
            { 'm', 'ь' },
            { ',', 'б' },
            { '.', 'ю' },
            { '/', '.' },
            { '@', '"' },
            { '#', '№' },
            { '$', ';' },
            { '^', ':' },
            { '&', '?' }
        };

        public static string EnToRu(string text)
        {
            return Mapper(text, EnRuVocabulary);
        }

        private static string Mapper(string text, Dictionary<char, char> vocabulary)
        {
            StringBuilder stringBuilder = new StringBuilder(text.Length);
            foreach (char character in text)
            {
                if (vocabulary.TryGetValue(character, out char value))
                {
                    stringBuilder.Append(value);
                }
                else
                {
                    stringBuilder.Append(character);
                }
            }
            return stringBuilder.ToString();
        }
    }
}

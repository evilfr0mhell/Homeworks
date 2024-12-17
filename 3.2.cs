using System;
using System.Collections.Generic;

class MorseCode
{
    // Словарь для хранения азбуки Морзе
    static Dictionary<char, string> morseAlphabet = new Dictionary<char, string>()
    {
        { 'A', ".-" },    { 'B', "-..." },  { 'C', "-.-." },  { 'D', "-.." },
        { 'E', "." },     { 'F', "..-." },  { 'G', "--." },   { 'H', "...." },
        { 'I', ".." },    { 'J', ".---" },  { 'K', "-.-" },   { 'L', ".-.." },
        { 'M', "--" },    { 'N', "-." },    { 'O', "---" },   { 'P', ".--." },
        { 'Q', "--.-" },  { 'R', ".-." },   { 'S', "..." },   { 'T', "-" },
        { 'U', "..-" },   { 'V', "...-" },  { 'W', ".--" },   { 'X', "-..-" },
        { 'Y', "-.--" },  { 'Z', "--.." },  { '1', ".----" }, { '2', "..---" },
        { '3', "...--" }, { '4', "....-" }, { '5', "....." }, { '6', "-...." },
        { '7', "--..." }, { '8', "---.." }, { '9', "----." }, { '0', "-----" },
        { ' ', "/" } // Пробел
    };

    // Метод для шифрования текста в азбуку Морзе
    static string EncryptToMorse(string input)
    {
        input = input.ToUpper();
        string morseCode = "";

        foreach (char letter in input)
        {
            if (morseAlphabet.ContainsKey(letter))
            {
                morseCode += morseAlphabet[letter] + " ";
            }
            else
            {
                morseCode += "? "; // Для неизвестных символов
            }
        }

        return morseCode.Trim();
    }

    // Метод для дешифрования из азбуки Морзе в текст
    static string DecryptFromMorse(string morseCode)
    {
        string[] morseWords = morseCode.Split('/');
        string decodedText = "";

        foreach (string morseWord in morseWords)
        {
            string[] morseChars = morseWord.Trim().Split(' ');

            foreach (string morseChar in morseChars)
            {
                foreach (var pair in morseAlphabet)
                {
                    if (pair.Value == morseChar)
                    {
                        decodedText += pair.Key;
                        break;
                    }
                }
            }
            decodedText += " ";
        }

        return decodedText.Trim();
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Введите текст для шифрования:");
        string input = Console.ReadLine();

        // Шифрование текста
        string morse = EncryptToMorse(input);
        Console.WriteLine("Закодировано в азбуку Морзе: " + morse);

        // Дешифрование текста
        string decrypted = DecryptFromMorse(morse);
        Console.WriteLine("Расшифровано из азбуки Морзе: " + decrypted);
    }
}
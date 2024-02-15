using System.Collections.Generic;
using System.Text;
using System;

namespace UntitledCube.Maze.Generation
{
    public static class SeedCodec
    {
        private static readonly Dictionary<string, string> _premutationTable = new()
        {
            {"000", "a"}, {"001", "b"}, {"002", "c"}, {"010", "d"}, {"011", "e"},
            {"012", "f"}, {"020", "g"}, {"021", "h"}, {"022", "i"}, {"100", "j"},
            {"101", "k"}, {"102", "l"}, {"110", "m"}, {"111", "n"}, {"112", "o"}, 
            {"120", "p"}, {"121", "q"}, {"122", "r"}, {"200", "s"}, {"201", "t"},
            {"202", "u"}, {"210", "v"}, {"211", "w"}, {"212", "x"}, {"220", "y"},
            {"221", "z"},
        };

        /// <summary>
        /// Encodes a list of integers into a single string representation.
        /// </summary>
        /// <param name="intList">The list of integers to encode.</param>
        /// <returns>A string where each character represents a digit from the input list.</returns>
        public static string Encode(List<int> intList)
        {
            StringBuilder builder = new();

            foreach (int value in intList)
                builder.Append(value.ToString());

            return builder.ToString();
        }

        /// <summary>
        /// Decodes a string representation of encoded integers back into a list of integers.
        /// </summary>
        /// <param name="encodedString">The string containing the encoded integers.</param>
        /// <returns>A list of integers, or null if the input string contains non-digit characters.</returns>
        public static List<int> Decode(string encodedString)
        {
            List<int> intList = new();

            foreach (char digitChar in encodedString)
            {
                if (!char.IsDigit(digitChar))
                    return null;

                int value = digitChar - '0';
                intList.Add(value);
            }

            return intList;
        }

        /// <summary>
        /// Encrypts a string using a simple character permutation scheme.
        /// </summary>
        /// <param name="data">The string to be encrypted.</param>
        /// <returns>The encrypted string.</returns>
        public static string Encrypt(string data)
        {
            string encodedData = "";
            for (int i = 0; i < data.Length; i += 3)
            {
                string triplet = data.Substring(i, Math.Min(3, data.Length - i));
                encodedData += _premutationTable.ContainsKey(triplet) ? _premutationTable[triplet] : triplet;
            }

            return encodedData;
        }

        /// <summary>
        /// Decrypts a string that was encrypted using the Encrypt method.
        /// </summary>
        /// <param name="data">The string to be decrypted.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string data)
        {
            string decodedData = "";
            foreach (char charToDecode in data)
            {
                string key = charToDecode.ToString();
                decodedData += _premutationTable.ContainsKey(key) ? _premutationTable[key] : key;
            }

            return decodedData;
        }
    }
}
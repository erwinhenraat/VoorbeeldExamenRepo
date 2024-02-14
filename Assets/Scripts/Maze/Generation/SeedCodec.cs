using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UntitledCube.Maze.Generation
{
    public static class SeedCodec
    {
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
                {
                    Debug.LogError("Invalid character in encoded string. Only digits are allowed.");
                    return null;
                }

                int value = digitChar - '0';
                intList.Add(value);
            }

            return intList;
        }
    }
}
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;

namespace UntitledCube.Maze.Generation
{
    public static class SeedCodec
    {
        private static readonly Dictionary<string, string> _encryptionPremutationTable = new()
        {
            {"000", "a"}, {"001", "b"}, {"002", "c"}, {"010", "d"}, {"011", "e"},
            {"012", "f"}, {"020", "g"}, {"021", "h"}, {"022", "i"}, {"100", "j"},
            {"101", "k"}, {"102", "l"}, {"110", "m"}, {"111", "n"}, {"112", "o"},
            {"120", "p"}, {"121", "q"}, {"122", "r"}, {"200", "s"}, {"201", "t"},
            {"202", "u"}, {"210", "v"}, {"211", "w"}, {"212", "x"}, {"220", "y"},
            {"221", "z"}, {"016", "A"}, {"156", "B"}, {"460", "C"}, {"146", "D"},
            {"714", "E"}, {"571", "F"}, {"506", "G"}, {"430", "H"}, {"310", "I"},
            {"750", "J"}, {"504", "K"}, {"361", "L"}, {"140", "M"}, {"417", "N"},
            {"715", "O"}, {"540", "P"}, {"075", "Q"}, {"371", "R"}, {"015", "S"},
            {"601", "T"}, {"615", "U"}, {"170", "V"}, {"541", "W"}, {"174", "X"},
            {"173", "Y"}, {"501", "Z"}, {"036", "!"}, {"641", "?"}, {"605", "#"}, 
        };

        private static readonly Dictionary<string, string> _decryptionPremutationTable = new();

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
            List<int> decodedSeed = new();

            foreach (char digitChar in encodedString)
            {
                if (!char.IsDigit(digitChar))
                    return null;

                int value = digitChar - '0';
                decodedSeed.Add(value);
            }

            return decodedSeed;
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
                encodedData += _encryptionPremutationTable.ContainsKey(triplet) 
                    ? _encryptionPremutationTable[triplet] 
                    : triplet;
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
            if (_decryptionPremutationTable.Count <= 0)
                GenerateDecryptionTable();

            string decodedData = "";
            foreach (char charToDecode in data)
            {
                string key = charToDecode.ToString();
                decodedData += _decryptionPremutationTable.ContainsKey(key) 
                    ? _decryptionPremutationTable[key] 
                    : key;
            }

            return decodedData;
        }

        /// <summary>
        /// Assembles a list of strings into a single string, separated by hyphens.
        /// </summary>
        /// <param name="seeds">A list of strings to be assembled.</param>
        /// <returns>The assembled string.</returns>
        public static string Assemble(List<string> seeds, int endPoint)
        {
            string assembledSeed = string.Join("-", seeds);
            assembledSeed += $";{endPoint}";
            return assembledSeed;
        }

        /// <summary>
        /// Disassembles a seed string with a word-number format (e.g., "hello-world-12") into
        /// a list of the component words and the trailing number.
        /// </summary>
        /// <param name="seed">The string to be disassembled.</param>
        /// <returns>A tuple containing: (1) a list of the disassembled words and (2) the extracted number.</returns>
        public static (List<string>, int) Disassemble(string seed)
        {
            if (string.IsNullOrEmpty(seed))
                return (null, 0);

            string modifiedSeed = seed[..^2];
            int endPoint = int.Parse(seed[^1..]);

            List<string> disassembledSeed = modifiedSeed.Split('-').ToList();

            return (disassembledSeed, endPoint);
        }

        /// <summary>
        /// Extracts the starting cell index for maze generation from a decoded and decrypted seed.
        /// </summary>
        /// <param name="seed">The encoded and encrypted maze seed.</param>
        /// <param name="cell">The index of the desired starting cell within the decoded seed.</param>
        /// <returns>The integer value representing the starting cell.</returns>
        public static int StartCell(string seed, int cell)
        {
            string decyptedSeed = Decrypt(seed);
            List<int> decodedSeed = Decode(decyptedSeed);
            int startCell = decodedSeed[cell];
            return startCell;
        }

        /// <summary>
        /// Processes a seed string by disassembling, decrypting, and decoding its components, 
        /// returning a nested list of integer results.
        /// </summary>
        /// <param name="seed">The input seed string.</param>
        /// <returns>A list of lists, where each inner list represents the decoded integers.</returns>
        public static (List<List<int>>, int) ProcessSeed(string seed)
        {
            if (string.IsNullOrEmpty(seed))
                return (null, 0);

            (List<string> mazeSeeds, int endPoint) = Disassemble(seed);

            List<List<int>> processedSeed = new();

            foreach(string mazeSeed in mazeSeeds)
                processedSeed.Add(Decode(Decrypt(mazeSeed)));

            return (processedSeed, endPoint);
        }

        /// <summary>
        /// Validates a seed string to ensure it conforms to a specific format.  
        /// </summary>
        /// <param name="seed">The seed string to validate.</param>
        /// <returns>True if the seed is valid, false otherwise.</returns>
        public static bool Validate(string seed)
        {
            if (string.IsNullOrEmpty(seed))
                return false;

            string[] seedParts = seed.Split(";");
            if (seedParts.Length != 2)
                return false;

            string[] mazeParts = seedParts[0].Split("-");
            if (mazeParts.Length != 6)
                return false;

            if (!int.TryParse(seedParts[1], out _))
                return false;

            if (seed.Length > 241 || seed.Length < 85)
                return false;

            return true;
        }

        private static void GenerateDecryptionTable()
        {
            foreach (KeyValuePair<string, string> pair in _encryptionPremutationTable)
                _decryptionPremutationTable[pair.Value] = pair.Key;
        }
    }
}
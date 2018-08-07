using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpChess.Model.Tests
{
    [TestClass]
    public class GameModeTests
    {
        [TestMethod]
        public void StartClassicMode()
        {
            Assert.AreEqual(Fen.GameStartPosition, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        }

        [TestMethod]
        public void Start960Mode()
        {

        }

        [TestMethod]
        public void Null960Start()
        {
            Assert.IsFalse(Fen.Validate960StartPosition(null));
        }

        [TestMethod]
        public void Invalid960Start()
        {
            Assert.IsFalse(Fen.Validate960StartPosition("rnbqbknr"));
            Assert.IsFalse(Fen.Validate960StartPosition("rnbqbnrk"));
            Assert.IsFalse(Fen.Validate960StartPosition("rbbkr"));
        }

        [TestMethod]
        public void AllValid960Positions()
        {
            int validStartCount = 0;
            string classicStart = "rnbqkbnr";
            List<string> solutions = new List<string>();
            IEnumerable<IEnumerable<char>> allPermutations = GetPermutations(classicStart, 8);
            foreach (IEnumerable<char> permutation in allPermutations)
            {
                StringBuilder sb = new StringBuilder();
                foreach (char c in permutation)
                {
                    sb.Append(c);
                }
                if (Fen.Validate960StartPosition(sb.ToString()))
                {
                    string validPositions = sb.ToString();
                    if (!solutions.Contains(validPositions))
                    {
                        solutions.Add(validPositions);
                        validStartCount++;
                    }
                }
            }
            Assert.AreEqual(960, validStartCount);
        }

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list,
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}

using CurrencyConverterLibrary;
using CurrencyConverterLibrary.Utils;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;

namespace CurrencyConverterlibrary.NUnitTests
{
    public class FindShortestPathUnitTest
    {
        [Test]
        public void GetShortestPathFunction_ConnectedGraph_ReturnShortestPath()
        {
            //Arrenge
            List<Tuple<string, string>> edges = new List<Tuple<string, string>>();
            edges.Add(Tuple.Create("a", "b"));
            edges.Add(Tuple.Create("a", "d"));
            edges.Add(Tuple.Create("b", "c"));
            edges.Add(Tuple.Create("d", "e"));
            edges.Add(Tuple.Create("d", "h"));
            edges.Add(Tuple.Create("e", "f"));
            edges.Add(Tuple.Create("e", "g"));
            edges.Add(Tuple.Create("e", "h"));
            edges.Add(Tuple.Create("f", "g"));
            edges.Add(Tuple.Create("g", "h"));
            Graph<string> graph = Graph<string>.CreateGraph(edges);

            //Action
            var shortestPathFunc = graph.GetShortestPathFunction("a");

            //Assert
            Assert.AreEqual(new List<string> { "a" }, shortestPathFunc("a"));
            Assert.AreEqual(new List<string> { "a", "b" }, shortestPathFunc("b"));
            Assert.AreEqual(new List<string> { "a", "b", "c" }, shortestPathFunc("c"));
            Assert.AreEqual(new List<string> { "a", "d" }, shortestPathFunc("d"));
            Assert.AreEqual(new List<string> { "a", "d", "e" }, shortestPathFunc("e"));
            Assert.AreEqual(new List<string> { "a", "d", "e", "f" }, shortestPathFunc("f"));
            Assert.That(shortestPathFunc("g"), Is.EqualTo(new List<string> { "a", "d", "e", "g" }) | Is.EqualTo(new List<string> { "a", "d", "h", "g" }));
            Assert.AreEqual(new List<string> { "a", "d", "h" }, shortestPathFunc("h"));
        }

        [Test]
        public void GetShortestPathFunction_UnconnectedGraph_ReturnShortestPath()
        {
            //Arrenge
            List<Tuple<string, string>> edges = new List<Tuple<string, string>>();
            edges.Add(Tuple.Create("a", "b"));
            edges.Add(Tuple.Create("a", "d"));
            edges.Add(Tuple.Create("b", "c"));
            edges.Add(Tuple.Create("c", "d"));
            edges.Add(Tuple.Create("e", "f"));
            edges.Add(Tuple.Create("e", "g"));
            edges.Add(Tuple.Create("e", "h"));
            edges.Add(Tuple.Create("f", "g"));
            edges.Add(Tuple.Create("g", "h"));
            Graph<string> graph = Graph<string>.CreateGraph(edges);

            //Action
            var shortestPathFunc = graph.GetShortestPathFunction("a");

            //Assert
            Assert.AreEqual(new List<string> { "a" }, shortestPathFunc("a"));
            Assert.AreEqual(new List<string> { "a", "b" }, shortestPathFunc("b"));
            Assert.AreEqual(new List<string> { "a", "b", "c" }, shortestPathFunc("c"));
            Assert.AreEqual(new List<string> { "a", "d" }, shortestPathFunc("d"));
            Assert.AreEqual(new List<string> { }, shortestPathFunc("e"));
            Assert.AreEqual(new List<string> { }, shortestPathFunc("f"));
            Assert.AreEqual(new List<string> { }, shortestPathFunc("h"));
            Assert.AreEqual(new List<string> { }, shortestPathFunc("h"));
        }
    }
}
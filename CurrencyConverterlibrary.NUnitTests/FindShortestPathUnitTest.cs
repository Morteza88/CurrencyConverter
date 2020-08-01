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
            List<Edge<string>> edges = new List<Edge<string>>();
            edges.Add(new Edge<string>("a", "b"));
            edges.Add(new Edge<string>("a", "d"));
            edges.Add(new Edge<string>("b", "c"));
            edges.Add(new Edge<string>("d", "e"));
            edges.Add(new Edge<string>("d", "h"));
            edges.Add(new Edge<string>("e", "f"));
            edges.Add(new Edge<string>("e", "g"));
            edges.Add(new Edge<string>("e", "h"));
            edges.Add(new Edge<string>("f", "g"));
            edges.Add(new Edge<string>("g", "h"));
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
            List<Edge<string>> edges = new List<Edge<string>>();
            edges.Add(new Edge<string>("a", "b"));
            edges.Add(new Edge<string>("a", "d"));
            edges.Add(new Edge<string>("b", "c"));
            edges.Add(new Edge<string>("c", "d"));
            edges.Add(new Edge<string>("e", "f"));
            edges.Add(new Edge<string>("e", "g"));
            edges.Add(new Edge<string>("e", "h"));
            edges.Add(new Edge<string>("f", "g"));
            edges.Add(new Edge<string>("g", "h"));
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
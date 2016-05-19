﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmAdvisor;
using System.IO;

namespace ParserTests {
    [TestFixture]
    public class TestClass {
        public string ReadFile(string filename) {
            using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + filename, Encoding.GetEncoding("windows-1251"))) {
                // Read the stream to a string, and write the string to the console.
                return sr.ReadToEnd();
            }
        }
        [Test]
        public void TestMethod() {
            // TODO: Add your test code here
            Assert.Pass("Your first passing test");
        }
        [Test]
        public void IsFilmPageTest() {
            var parser = new Parser();

            var filmPageUrl = new Uri("http://www.kinopoisk.ru/film/464963/");
            var notFilmPageUrl = new Uri("http://www.kinopoisk.ru/index.php?level=7&from=forma&result=adv&m_act%5Bfrom%5D=forma&m_act%5Bwhat%5D=content&m_act%5Bfind%5D=pulpy");
            Assert.True(parser.IsFilmPage(filmPageUrl));
            Assert.False(parser.IsFilmPage(notFilmPageUrl));
        }
        [Test]
        public void TestFilmPageParsing() {
            var parser = new Parser();

            var text1 = ReadFile("Игра престолов.html");
            var text2 = ReadFile("Криминальное чтиво.html");

            var name1 = parser.GetMatch(parser.filmPageRegexes["name"], text1);
            var year1 = parser.GetMatch(parser.filmPageRegexes["year"], text1);

            var name2 = parser.GetMatch(parser.filmPageRegexes["name"], text2);
            var year2 = parser.GetMatch(parser.filmPageRegexes["year"], text2);

            Assert.AreEqual("Игра престолов", name1);
            Assert.AreEqual("2011", year1);

            Assert.AreEqual("Криминальное чтиво", name2);
            Assert.AreEqual("1994", year2);
        }
        [Test]
        public void TestSearchResultsParsing1() {
            var parser = new Parser();

            var text1 = ReadFile("Результаты поиска (22).html");

            var entries = parser.GetEntriesFromSearchResults(text1);

            Assert.AreEqual(11, entries.Count());
        }
        [Test]
        public void TestSearchResultsParsing2() {
            var parser = new Parser();

            var text1 = ReadFile("Результаты поиска (22).html");
            //var text2 = ReadFile("Криминальное чтиво.html");

            var films = parser.ParseSearchResults(text1);

            var expectedFilms = new List<Film>();
            expectedFilms.Add(new Film("Дешевое чтиво", "1972"));
            expectedFilms.Add(new Film("Pulp", "2013"));
            expectedFilms.Add(new Film("Pulp: A Film About Life, Death and Supermarkets", "2014"));
            expectedFilms.Add(new Film("Криминальное чтиво", "1994"));
            expectedFilms.Add(new Film("«Криминальное чтиво» в мгновение ока: Ретроспектива к 10-летию (ТВ)", "2004"));
            expectedFilms.Add(new Film("Документальный фильм к 75-летию Marvel (ТВ)", "2014"));
            expectedFilms.Add(new Film("Pulp Fiction: The Facts (видео)", "2002"));
            expectedFilms.Add(new Film("Pulp Fiction Art: Cheap Thrills &amp; Painted Nightmares", "2005"));
            expectedFilms.Add(new Film("Pulp Comedy (сериал)", "1997"));
            expectedFilms.Add(new Film("Pulp Comics: Louis C.K.'s Filthy Stupid Talent Show (ТВ)", "1999"));
            expectedFilms.Add(new Film("Pulp Ration (Raci&#243;n de pulpo)", "1996"));

            Assert.That(films.Count() == expectedFilms.Count());

            var names = films.Select(x => x.Name);
            var expectedNames = expectedFilms.Select(x => x.Name);
            var years = films.Select(x => x.Year);
            var expectedYears = expectedFilms.Select(x => x.Year);

            CollectionAssert.AreEqual(expectedNames, names);
            CollectionAssert.AreEqual(expectedYears, years);
        }
    }
}

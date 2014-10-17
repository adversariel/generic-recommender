using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace RecEngModel
{
    [TestClass]
    public class UnitTestModel
    {
        [TestMethod]
        public void TestGetBook()
        {
            Model m = new Model();
            m.addBook("The Music of the Primes", "Marcus du Sautoy");
            Book actual = m.getBook("The Music of the Primes", "Marcus du Sautoy");
            Book expected = new Book("The Music of the Primes", "Marcus du Sautoy");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetBookByTitle()
        {
            Model m = new Model();
            m.addBook("Godel, Escher, Bach: An Eternal Gold Braid", "Douglas Hofstadter");
            Book actual = (m.getBookByTitle("Godel, Escher, Bach: An Eternal Gold Braid"))[0];
            Book expected = new Book("Godel, Escher, Bach: An Eternal Gold Braid", "Douglas Hofstadter");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetBookByAuthor()
        {
            Model m = new Model();
            m.addBook("Information Arts", "Stephen Wilson");
            Book actual = (m.getBookByAuthor("Stephen Wilson"))[0];
            Book expected = new Book("Information Arts", "Stephen Wilson");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetRater()
        {
            Model m = new Model();
            m.addRater("Morgan Freeman");
            Rater actual = m.getRater("Morgan Freeman");
        }

        [TestMethod]
        [ExpectedException(typeof(Model.AlreadyExistsException))]
        public void TestBookException()
        {
            Model m = new Model();
            m.addBook("Visual Perception from a Computer Graphics Perspective", "William Thompson");
            m.addBook("Visual Perception from a Computer Graphics Perspective", "William Thompson");
        }

        [TestMethod]
        [ExpectedException(typeof(Model.AlreadyExistsException))]
        public void TestRaterException()
        {
            Model m = new Model();
            m.addRater("Roger Ebert");
            m.addRater("Roger Ebert");
        }

        [TestMethod]
        public void TestBookAddRating()
        {
            Book b = new Book("Theoretical Neuroscience", "Peter Dayan");
            Rater r = new Rater("Santiago Ramón y Cajal");
            b.addRating(r, (float)5.0);
            Assert.AreEqual(r.ratedBooks[b], (float)5.0);
        }

        [TestMethod]
        public void TestRaterAddRating()
        {
            Book b = new Book("The Visual Display of Quantitative Information", "Edward Tufte");
            Rater r = new Rater("John Tukey");
            r.addRating(b, (float)5.0);
            Assert.AreEqual(b.bookRating[r], (float)5.0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Web.Models
{
    public class Production
    {
        public Production(Film film, Person person, Post post)
        {
            Film = film;
            Person = person;
            Post = post;
        }

        public Film Film { get; private set; }

        public Person Person { get; private set; }

        public Post Post { get; private set; }
    }

    public struct Person
    {
        public Person(int id, string name, string information)
        {
            Id = id;
            Name = name;
            Information = information;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Information { get; private set; }
    }

    public struct Post
    {
        public Post(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}

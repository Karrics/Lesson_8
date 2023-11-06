using System;
using System.Collections.Generic;

namespace Tymakov_9
{
    internal class Song
    {
        string name; // название песни
        string author; // автор песни
        Song prev; // связь с предыдущей песней в списке

        public Song(string name, string author, Song prev = null)
        {
            this.name = name;
            this.author = author;
            this.prev = prev;
        }

        public Song(string name, string author, string prevName, string prevAuthor)
                : this(name, author)
        {
            prev = new Song(prevName, prevAuthor);
        }

        // Метод для печати названия песни и ее исполнителя
        public string Title()
        {
            return $"{name} - {author}";
        }

        // Метод, который сравнивает между собой два объекта-песни
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Song otherSong = (Song)obj;
            return name == otherSong.name && author == otherSong.author;
        }
    }
}

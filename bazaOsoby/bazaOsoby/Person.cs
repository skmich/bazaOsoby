using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace bazaOsoby
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail {  get; set; }
        public string Adress { get; set; }


    }
}

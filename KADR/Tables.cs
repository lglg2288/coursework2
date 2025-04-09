using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace KADR
{
    public interface IDbEntity
    {
        [Key]
        int ID { get; set; }
    }
    public class Users : IDbEntity
    {
        private string _name;
        private string _login;
        private string _pass;
        [Key]
        public int ID { get; set; }
        public string Name
        {
            get => _name;
            set => _name = value ?? "NULL";
        }
        public string Login
        {
            get => _login;
            set => _login = value ?? "NULL";
        }
        public string PASS
        {
            get => _pass;
            set => _pass = value ?? "NULL";
        }
        public bool Adm { get; set; }
    }

    public class TypeDosc : IDbEntity
    {
        private string _name;
        private string _fullname;
        [Key]
        public int ID { get; set; }
        public string Name
        {
            get => _name;
            set => _name = value ?? "NULL";
        }
        public string FullName
        {
            get => _fullname;
            set => _fullname = value ?? "NULL";
        }
        public int Kadr { get; set; }
        public int Tabel { get; set; }
    }
    public class Tree
    {
        public int ID { get; set; }
        public int OwnerId { get; set; }
        public string Tree_Object { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string ClassObject { get; set; }
    }
    public class SaveDocs
    {
        public int ID { get; set; }
        public string BementName { get; set; }
        public int BementId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string FileName { get; set; }
        public byte[] Doc { get; set; }
    }
    public class PropValue
    {
        public int ID { get; set; }
        public int ClassId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
    public class Prop
    {
        public int ID { get; set; }
        public int PropValueId { get; set; }
        public string ElementName { get; set; }
        public int ElementId { get; set; }
        public string Name { get; set; }
        public string FullNames { get; set; }
    }
    public class Post
    {
        public int ID { get; set; }
        public int Departmentid { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public int Quantity { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateBeg { get; set; }
        public DateTime DateEnd { get; set; }
    }
    public class Peoples
    {
        public int ID { get; set; }
        public int Treeid { get; set; }
        public string F { get; set; }
        public string I { get; set; }
        public string O { get; set; }
        public string Sex { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string INN { get; set; }
        public DateTime DR { get; set; }
        public string Telephon { get; set; }
        public string AddressFact { get; set; }
        public string AddressUr { get; set; }
    }
    public class JornalTabel
    {
        public int ID { get; set; }
        public int PostId { get; set; }
        public int PeoplesId { get; set; }
        public int ClassId { get; set; }
        public int TypeDocsId { get; set; }
        public string NumDoc { get; set; }
        public DateTime DateDoc { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public int TimeFond { get; set; }
        public int TimeFact { get; set; }
        public int Day1 { get; set; }
        public int Day2 { get; set; }
        public int Day3 { get; set; }
        public int Day4 { get; set; }
        public int Day5 { get; set; }
        public int Day6 { get; set; }
        public int Day7 { get; set; }
        public int Day8 { get; set; }
        public int Day9 { get; set; }
        public int Day10 { get; set; }
        public int Day11 { get; set; }
        public int Day12 { get; set; }
        public int Day13 { get; set; }
        public int Day14 { get; set; }
        public int Day15 { get; set; }
        public int Day16 { get; set; }
        public int Day17 { get; set; }
        public int Day18 { get; set; }
        public int Day19 { get; set; }
        public int Day20 { get; set; }
        public int Day21 { get; set; }
        public int Day22 { get; set; }
        public int Day23 { get; set; }
        public int Day24 { get; set; }
        public int Day25 { get; set; }
        public int Day26 { get; set; }
        public int Day27 { get; set; }
        public int Day28 { get; set; }
        public int Day29 { get; set; }
        public int Day30 { get; set; }
        public int Day31 { get; set; }
    }
    public class JornalKard
    {
        public int ID { get; set; }
        public int PostId { get; set; }
        public int PeoplesId { get; set; }
        public int ClassId { get; set; }
        public int TypeDoscId { get; set; }
        public string NumDoc { get; set; }
        public DateTime DateDoc { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
    public class FieldsJornal
    {
        public int ID { get; set; }
        public int TypeDocsid { get; set; }
        public string Name { get; set; }
        public string FieldName { get; set; }
    }
    public class Department
    {
        public int ID { get; set; }
        public int TreeId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public DateTime DateBeg { get; set; }
        public DateTime DateEnd { get; set; }
    }
    public class ClassName
    {
        public int ID { get; set; }
        public string Object { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public bool Tree { get; set; }
        public bool NotDel { get; set; }
    }
    public class ClassArr
    {
        public int ID { get; set; }
        public int ClassId { get; set; }
        public string BementName { get; set; }
        public int BementId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
    public class Class
    {
        public int ID { get; set; }
        public int TreeId { get; set; }
        public string Object { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
}
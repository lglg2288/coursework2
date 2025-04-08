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
}

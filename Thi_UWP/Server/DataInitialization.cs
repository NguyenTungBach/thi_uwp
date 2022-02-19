using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thi_UWP.Entity;

namespace Thi_UWP.Server
{
    class DataInitialization
    {
        public static void CreatedContact()
        {
            SQLiteConnection cnn = new SQLiteConnection("contactlite.db");
            string sqlPersonalContact = @"CREATE TABLE IF NOT EXISTS
            Contact (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
            Name VARCHAR( 140 ),
            Phone VARCHAR( 140 )
            );";
            using (var statement = cnn.Prepare(sqlPersonalContact))
            {
                Debug.WriteLine("Tao bang");
                statement.Step();
            }
        }

        public static bool IsInsertContact(Contact contact)
        {
            try
            {
                SQLiteConnection cnn = new SQLiteConnection("contactlite.db");
                using (var personalTransaction = cnn.Prepare("INSERT INTO Contact(Name, Phone) VALUES(?, ?)"))
                {
                    personalTransaction.Bind(1, contact.Name);
                    personalTransaction.Bind(2, contact.Phone);
                    personalTransaction.Step();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
        public static List<Contact> ListContact()
        {
            var list = new List<Contact>();
            try
            {
                SQLiteConnection cnn = new SQLiteConnection("contactlite.db");
                using (var stt = cnn.Prepare("select * from Contact"))
                {
                    while (stt.Step() == SQLiteResult.ROW)
                    {
                        var contactThis = new Contact()
                        {
                            Name = (string)stt["Name"],
                            Phone = (string)stt["Phone"],
                        };
                        list.Add(contactThis);
                    }
                }
                //Debug.WriteLine(list[0]);
                return list;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Co loi list" + ex);
                return null;
            }
        }

        public static List<Contact> ListTransactionByName(string Name)
        {
            var list = new List<Contact>();
            try
            {
                Debug.WriteLine("Name la" + Name);
                SQLiteConnection cnn = new SQLiteConnection("contactlite.db");
                using (var stt = cnn.Prepare($"select * from Contact where Name = '{Name}'"))
                {
                    while (stt.Step() == SQLiteResult.ROW)
                    {
                        var contact = new Contact()
                        {
                            Name = (string)stt["Name"],
                            Phone = (string)stt["Phone"],
                        };
                        list.Add(contact);
                    }
                }
                Debug.WriteLine(list[0]);
                return list;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Co loi list" + ex);
                return null;
            }
        }
    }
}

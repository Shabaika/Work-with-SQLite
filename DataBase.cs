using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace Lab4
{
    public class DataBase
    {
        static public string path = Path.GetDirectoryName(Application.ExecutablePath);

        public static SQLiteConnection connection = new SQLiteConnection(@"Data Source=" + path + "\\galactics.db;version=3;new=False;datetimeformat=CurrentCulture");

        public static void SqlConnect()
        {
            string q = path;
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand(connection);
            connection.Close();
        }

        public static void AddRow(string name, string type, string size, string weight)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand(connection);
            DataTable dt = new DataTable();
            cmd.CommandText = "INSERT INTO Space(Name,Type,Size,Weight) values(@name,@type,@size,@weight)";
            cmd.Parameters.Add(new SQLiteParameter("@name", name));
            cmd.Parameters.Add(new SQLiteParameter("@type", type));
            cmd.Parameters.Add(new SQLiteParameter("@size", size));
            cmd.Parameters.Add(new SQLiteParameter("@weight", weight));

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public static void DeleteRow(int id)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand(connection);

            cmd.CommandText = "delete from  Space where ID=@ID";
            cmd.Parameters.AddWithValue("@ID", id);

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public static void EditRow(int id, string name, string type, string size, string weight)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand(connection);

            cmd.CommandText = "update Space set Name =@name,Type=@type,Size=@size,Weight=@weight where ID=@id";
            cmd.Parameters.AddWithValue("@Name", name);

            cmd.Parameters.AddWithValue("@Type", type);

            cmd.Parameters.AddWithValue("@Size", size);

            cmd.Parameters.AddWithValue("@Weight", weight);

            cmd.Parameters.AddWithValue("@ID", id);

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        
    }
}

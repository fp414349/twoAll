using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Clients.Pages.ClientsData
{
    public class EditModel : PageModel
       
    {

     
        public  ClientInfo info=new ClientInfo();
        public   string errorMessage = "";
        public   string sucssesMessage = "";
        
        public void OnGet()
        {
            string id = Request.Query["ID"];    
            try
            {
                string connectionString = "Data Source=COMP;Initial Catalog=server;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select * from clients where id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                              
                                info.id = "" + reader.GetInt32(0);
                                info.name = reader.GetString(1);
                                info.email = reader.GetString(2);
                                info.phon = reader.GetString(3);
                                info.adress = reader.GetString(4);

                              


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            info.id = Request.Form["ID"];
            info.name = Request.Form["Name"];   
            info.email = Request.Form["email"];
            info.phon = Request.Form["phon"];
            info.adress = Request.Form["adress"];

            if (info.name.Length == 0 || info.email.Length == 0 ||
           info.phon.Length == 0 || info.adress.Length == 0)
            {
                errorMessage = "All fields are requierd";
                return;
            }
            try
            {
                string connectionString = "Data Source=COMP;Initial Catalog=server;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients " +
                     "SET name=@name,email=@email,phone=@phon,address=@adress" +
                        " where id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", info.name);
                        command.Parameters.AddWithValue("@email", info.email);
                        command.Parameters.AddWithValue("@phon", info.phon);
                        command.Parameters.AddWithValue("@adress", info.adress);
                        command.Parameters.AddWithValue("@id", info.id);
                        Console.WriteLine(sql);
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/ClientsData/Index");

        }



    }
}

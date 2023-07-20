using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Clients.Pages.ClientsData
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo=new ClientInfo();
        public string errorMessage = "";
        public string sucssesMessage = "";
        public void OnGet()
        {
        }
        
        public void OnPost() 
        {
         
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phon = Request.Form["phon"];
            clientInfo.adress = Request.Form["adress"];
           
            if(clientInfo.name.Length==0||clientInfo.email.Length==0||
                clientInfo.phon.Length == 0 || clientInfo.adress.Length == 0)
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
                    String sql = "insert into clients "+
                        "(name,email,phone,address) values"+
                        "(@name,@email,@phon,@adress);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue ("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phon",clientInfo.phon);
                        command.Parameters.AddWithValue("@adress", clientInfo.adress);
                        
                        command.ExecuteNonQuery();
                      
                    }
                }
            }
            catch(Exception ex) { 
                errorMessage = ex.Message;  
            }
            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phon = "";
            clientInfo.adress = "";
            sucssesMessage = "new client added corectly";
            Response.Redirect("/ClientsData/Index");
        }
    }
}

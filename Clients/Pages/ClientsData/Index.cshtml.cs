using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Clients.Pages.ClientsData
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> liscClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=COMP;Initial Catalog=server;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select * from clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo info = new ClientInfo();
                              info.id= "" + reader.GetInt32(0);
                               info.name = reader.GetString(1);
                                info.email = reader.GetString(2);
                                info.phon= reader.GetString(3);
                                info.adress= reader.GetString(4);

                                liscClients.Add(info);


                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

    }
    public class ClientInfo
    {
        public string id;
        public string name;
        public string email;
        public string phon;
        public string adress;


    }
}
